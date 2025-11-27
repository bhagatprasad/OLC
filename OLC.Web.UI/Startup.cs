using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Newtonsoft.Json.Serialization;
using OLC.Web.UI.Helper;
using OLC.Web.UI.Services;

namespace OLC.Web.UI
{
    public class Startup
    {
        public readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddMvc();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddDirectoryBrowser();

            var olcConfig = configuration.GetSection("OLCConfig");

            services.Configure<OLCConfig>(olcConfig);

            services.AddHttpClient();

            services.AddScoped<HttpClientService>();

            services.AddTransient<TokenAuthorizationHttpClientHandler>();

            services.AddHttpClient("AuthorizedClient").AddHttpMessageHandler<TokenAuthorizationHttpClientHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICreditCardService, CreditCardService>();

            services.AddScoped<IAccountTypeService, AccountTypeService>();

            services.AddScoped<IAddressTypeService, AddressTypeService>();

            services.AddScoped<IBankService, BankService>();

            services.AddScoped<ICardTypeService, CardTypeService>();

            services.AddScoped<IStatusService, StatusService>();

            services.AddScoped<ITransactionTypeService, TransactionTypeService>();

            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IStateService, StateService>();

            services.AddScoped<IBillingAddressService, BillingAddressService>();

            services.AddScoped<IBankAccountService, OLC.Web.UI.Services.BankAccountService>();

            services.AddScoped<ICityService, CityService>();

            services.AddScoped<IPaymentOrderService, PaymentOrderService>();

            services.AddScoped<IPaymentReasonService, PaymentReasonService>();

            services.AddScoped<ITransactionFeeService, TransactionFeeService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IServiceRequestService, ServiceRequestService>();

            services.AddScoped<IUserKycService, UserKycService>();

            services.AddScoped<IUserKycDocumentService, UserKycDocumentService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "allowCookies";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.SlidingExpiration = false;
                    options.AccessDeniedPath = "/Error/NotAccessable";
                    options.LoginPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                })
                .AddGoogle(options =>
                {
                    // Remove SignInScheme - let it use default
                    options.ClientId = configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                    options.SaveTokens = true;

                    // Add these for better debugging
                    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
                    {
                        OnCreatingTicket = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnTicketReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnRemoteFailure = context =>
                        {

                            context.Response.Redirect("/Account/Login");
                            context.HandleResponse();
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
                options.AddPolicy("Executive", policy => policy.RequireRole("Executive"));
            });

            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopCenter;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    ctx.Context.Response.Headers["Pragma"] = "no-cache";
                    ctx.Context.Response.Headers["Expires"] = "-1";
                }
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseNotyf();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
