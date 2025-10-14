using Microsoft.Extensions.Options;

namespace OLC.Web.UI.Helper
{
    public class TokenAuthorizationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OLCConfig _olcConfig;


        public TokenAuthorizationHttpClientHandler(IHttpContextAccessor httpContextAccessor, IOptions<OLCConfig> olcConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _olcConfig = olcConfig.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Add("Authorization", accessToken);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
