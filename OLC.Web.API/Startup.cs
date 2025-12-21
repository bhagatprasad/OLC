using Hangfire;
using Hangfire.SqlServer;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OLC.Web.API.Manager;
using OLC.Web.Email.Service;
using OLC.Web.Job;
using OLC.Web.Sms.Service;
using Stripe;

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
            services.AddScoped<IStateManager, StateManager>();
            services.AddScoped<ICityManager, CityManager>();
            services.AddScoped<ITransactionFeeManager, TransactionFeeManager>();
            services.AddScoped<IBillingAddressManager, BillingAddressManager>();
            services.AddScoped<IUserBankAccountManager, UserBankAccountManager>();
            services.AddScoped<IPaymentOrderManager, PaymentOrderManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IServiceRequestManager, ServiceRequestManager>();
            services.AddScoped<IUserKycManager, UserKycManager>();
            services.AddScoped<IUserKycDocumentManager, UserKycDocumentManager>();
            services.AddScoped<IPriorityManager, PriorityManager>();
            services.AddScoped<IRewardConfigurationManager, RewardConfigurationManager>();
            services.AddScoped<IWalletTypeManager, WalletTypeManager>();
            //init email service
            services.AddScoped<IEmailSubScriber, EmailSubScriber>();
            //init sms service
            services.AddScoped<ISmsSubscriber, SmsSubscriber>();
            services.AddScoped<IUserWalletManager, UserWalletManager>();
            services.AddScoped<ITransactionRewardManager, TransactionRewardManager>();
            services.AddScoped<IDepositOrderManager, DepositOrderManager>();
            services.AddScoped<ICryptocurrencyManager, CryptocurrencyManager>();
            services.AddScoped<IUserLoginHistoryManager, UserLoginHistoryManager>();
            services.AddScoped<INewsLetterManager, NewsLetterManager>();
            services.AddScoped<IUserLoginHistoryManager, UserLoginHistoryManager>();
            services.AddScoped<IEmailTemplateManager, EmailTemplateManager>();
            services.AddScoped<IBlockChainManager, BlockChainManager>();
            services.AddScoped<IQueueConfigurationManager, QueueConfigurationManager>();

            services.AddScoped<IExecutivesManager, ExecutivesManager>();
            services.AddScoped<IEmailCategoryManager, EmailCategoryManager>();
            services.AddScoped<IExecutiveAssignmentsManager, ExecutiveAssignmentsManager>();
            services.AddScoped<IMailBoxManager, MailBoxManager>();

            services.AddScoped<IQueueProcessingHistoryManager, QueueProcessingHistoryManager>();
            services.AddScoped<IOrderQueueManager, OrderQueueManager>();
            services.AddScoped<IEmailCampaignManager, EmailCampaignManager>();
            services.AddScoped<IEmailRuleTypeManager, EmailRuleTypeManager>();
            services.AddScoped<ICampaignTypeManager, CampaignTypeManager>();




            services.AddMvc().AddXmlSerializerFormatters();

            var emailConfig = _configuration.GetSection("EmailConfig");

            services.Configure<EmailConfig>(emailConfig);

            var smsConfig = _configuration.GetSection("SmsConfig");

            services.Configure<SmsConfig>(smsConfig);

            services.AddHangfire(config =>
            {
                config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        _configuration.GetConnectionString("DefaultConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.FromSeconds(15),
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        });
            });

            services.AddHangfireServer();

            services.AddScoped<AutoApprovePaymentOrderJob>();
            services.AddScoped<StripeDepositPaymentJob>();
            services.AddScoped<PaymentFlowSchedulerJob>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:5227")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
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

            app.UseHangfireDashboard("/hangfire");
          
            RecurringJob.AddOrUpdate<PaymentFlowSchedulerJob>(
                "payment-flow-scheduler",
                x => x.RunAsync(),
                Cron.Minutely);

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
