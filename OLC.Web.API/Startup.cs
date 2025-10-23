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
            services.AddScoped<IPaymentReasonManager, PaymentReasonManager>();
            services.AddScoped<ICreditCardManager, CreditCardManager>();
            services.AddScoped<ITransactionTypeManager, TransactionTypeManager>();
            services.AddScoped<IStatusManager, StatusManager>();
            services.AddScoped<IAccountTypeManager, AccountTypeManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ICardTypeManager, CardTypeManager>();  
            services.AddScoped<IAddressTypeManager, AddressTypeManager>();
            services.AddScoped<ICardTypeManager, CardTypeManager>();    
            services.AddScoped<IBankManager, BankManager>();    
            services.AddScoped<ICountryManager, CountryManager>();
            services.AddScoped<ICityManager, CityManager>();

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:5227") // Specific allowed origin
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials() // If you need credentials/cookies
                );
            });

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

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

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
