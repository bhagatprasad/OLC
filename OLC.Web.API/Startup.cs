using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OLC.Web.API.Manager;

namespace OLC.Web.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddScoped<IAccountManager, AccountManager>();

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OLC.Web.API",
                    Description = "OLC.Web.API",
                    Contact = new OpenApiContact
                    {
                        Name = "OLC.Web.API Service",
                        Email = "contact@bdprasad.in",
                        Url = new Uri("https://bdprasad.in")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "contact@bdprasad.in",
                        Url = new Uri("https://bdprasad.in")
                    }

                });
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OLC.Web.API");
            });

        }
    }
}
