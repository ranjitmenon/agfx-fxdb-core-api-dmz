using System.Collections.Generic;
using Argentex.ClientSite.Service.Http;
using Argentex.Core.Api.Automapper;
using Argentex.Core.Api.Config;
using Argentex.Core.Api.Filters;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.ClientCompanies;
using Argentex.Core.Service.ClientSiteAction;
using Argentex.Core.Service.Country;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Monitoring;
using Argentex.Core.Service.Order;
using Argentex.Core.Service.Payments;
using Argentex.Core.Service.Settlements;
using Argentex.Core.Service.Sms.SmsSender;
using Argentex.Core.Service.Statements;
using Argentex.Core.Service.Trade;
using Argentex.Core.Service.User;
using Argentex.Core.SignalRService;
using Argentex.Core.UnitsOfWork.AppSettings;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.ClientSiteAction;
using Argentex.Core.UnitsOfWork.Countries;
using Argentex.Core.UnitsOfWork.Currencies;
using Argentex.Core.UnitsOfWork.Notifications;
using Argentex.Core.UnitsOfWork.Payments;
using Argentex.Core.UnitsOfWork.Settlements;
using Argentex.Core.UnitsOfWork.Statements;
using Argentex.Core.UnitsOfWork.Trades;
using Argentex.Core.UnitsOfWork.Users;
using AutoMapper;
using EQService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SynetecLogger;
using System.Net.Http;
using Argentex.Core.Api.ClientAuthentication;
using Microsoft.EntityFrameworkCore.Internal;
using OpenIddict.Abstractions;

namespace Argentex.Core.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile(@"ClientAuthentication/ClientCredentials.json", optional:true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Configuration.Bind("ClientCredentials", _clientCredentials);
        }

        public IConfiguration Configuration { get; }
        private readonly IList<ClientConfig> _clientCredentials = new List<ClientConfig>();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDB"));
                options.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<SecurityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<FXDB1Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FXDB1")));

            IdentityConfig.ConfigureIdentityOptions(services);

            // Register the OpenIddict services.
            services.AddOpenIddict()
                .AddCore(o =>
                    o.UseEntityFrameworkCore()
                        .UseDbContext<SecurityDbContext>())
                .AddServer(o =>
                {
                    o.UseMvc();
                    o.EnableTokenEndpoint("/api/security/token");
                    o.AllowPasswordFlow();
                    o.AllowRefreshTokenFlow();
                    o.AllowClientCredentialsFlow();
                    o.DisableHttpsRequirement();
                    o.AcceptAnonymousClients();
                    o.RegisterScopes(OpenIddictConstants.Scopes.Email,
                        OpenIddictConstants.Scopes.OpenId,
                        OpenIddictConstants.Scopes.Phone,
                        OpenIddictConstants.Scopes.Profile,
                        OpenIddictConstants.Scopes.OfflineAccess,
                        OpenIddictConstants.Scopes.Roles);
                });

            JwtConfig.ConfigureJwt(services, Configuration);

            //CORS
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                //URL slashes must be consistent, no double slashes
                builder.AllowAnyMethod().AllowAnyHeader()
                       .WithOrigins(Configuration["Urls:FXDBTraderUrl"], Configuration["Urls:ClientSiteUrl"])
                       .AllowCredentials();
            }));

            // Add application services.
            //transiet         
            //scoped
            services.AddScoped<HttpClient, HttpClient>();
            services.AddScoped<IHttpService, HttpService>();

            services.AddScoped<IBarxFxService, BarxFxService>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<ITradeUow, TradeUow>();
            services.AddScoped<IClientCompanyService, ClientCompanyService>();
            services.AddScoped<IClientCompanyUow, ClientCompanyUow>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryUow, CountryUow>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserUow, UserUow>();
            services.AddScoped<IConfigWrapper, ConfigWrapper>();
            services.AddScoped<ILogWrapper, NLogWrapper>((ctx) => 
                new NLogWrapper(Configuration.GetConnectionString("NLogWrapperDB")));
            services.AddScoped<IStatementUoW, StatementUoW>();
            services.AddScoped<IStatementService, StatementService>();
            services.AddScoped<ICurrencyUoW, CurrencyUoW>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IClientCompanyAccountsUoW, ClientCompanyAccountsUoW>();
            services.AddScoped<IClientCompanyAccountsService, ClientCompanyAccountsService>();
            services.AddScoped<IPaymentUoW, PaymentUoW>();
            services.AddScoped<ISettlementService, SettlementService>();
            services.AddScoped<ISettlementUow, SettlementUow>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAppSettingService, AppSettingService>();
            services.AddScoped<IAppSettingUow, AppSettingUow>();
            services.AddScoped<IMonitoringService, MonitoringService>();
            services.AddScoped<IServiceEmail, ServiceEmailClient>((ctx) =>
                new ServiceEmailClient(ServiceEmailClient.EndpointConfiguration.Basic, Configuration["EQS:EQSEndPointUrl"]));
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMonitoringHub, MonitoringHub>();
            services.AddScoped<ITraderActionsHub, TraderActionsHub>();
            services.AddScoped<IClientSiteActionService, ClientSiteActionService>();
            services.AddScoped<IClientSiteActionUow, ClientSiteActionUow>();
            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<ISmsSender, SmsSender>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<ITextMagicService, TextMagicService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationUow, NotificationUow>();
            services.AddScoped<IClientApplicationUow, ClientApplicationUow>();
            services.AddAutoMapper(x => 
            {
                x.AddProfile<MappingProfiles>();
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            
            app.Use((context, next) =>
            {
                context.Response.Headers.Remove("x-powered-by");
                return next();
            });

            app.UseAuthentication();

            //Configure Cors
            app.UseCors("CorsPolicy");

            app.UseMvc();

            app.UseWebSockets();
            app.UseSignalR(routes =>
            {
                //slashes must be consistent, no double slashes
                //and hub URLs should always start with a slash
                routes.MapHub<MonitoringHub>("/hubs/monitoring");
                routes.MapHub<TraderActionsHub>("/hubs/trader-actions");
            });

            if (_clientCredentials.Any())
            {
                using (var updater = new ClientUpdater(app.ApplicationServices))
                {
                    updater.SynchroniseClients(_clientCredentials);
                }
            }
        }
    }
}
