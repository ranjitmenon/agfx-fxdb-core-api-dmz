using Argentex.ClientSite.Service.Http;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.ClientCompanies;
using Argentex.Core.Service.Country;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Order;
using Argentex.Core.Service.Payments;
using Argentex.Core.Service.Settlements;
using Argentex.Core.Service.Statements;
using Argentex.Core.Service.Trade;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.AppSettings;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.Countries;
using Argentex.Core.UnitsOfWork.Currencies;
using Argentex.Core.UnitsOfWork.Payments;
using Argentex.Core.UnitsOfWork.Statements;
using Argentex.Core.UnitsOfWork.Trades;
using Argentex.Core.UnitsOfWork.Users;
using EQService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SynetecLogger;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Argentex.Recurrent.Services
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            // config section
            // getting service collections with dependencies
            var serviceCollections = StartupDependencyInjections();
            // building services
            var serviceProvider = serviceCollections.BuildServiceProvider();

            // recurrent
            var orderService = serviceProvider.GetService<IOrderService>();
            var orders = orderService.GetExpiredValidityOrders();

            Console.WriteLine($"Current orders ={orders.Count}");
            foreach (var order in orders)
            {
                Console.WriteLine($"Current orders ={order.TradeRef}");
                await orderService.CancelOrderAsync(order);
            }
            Console.WriteLine($"Done");

            Console.ReadLine();
        }

        /// <summary>
        /// Get Program Dependency Injections
        /// </summary>
        /// <returns>ServiceCollection</returns>
        public static ServiceCollection StartupDependencyInjections()
        {
            var serviceCollections = new ServiceCollection();

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // DB Context configs
            serviceCollections.AddDbContext<SecurityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDB")));
            serviceCollections.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<SecurityDbContext>();
            serviceCollections.AddEntityFrameworkSqlServer()
                .AddDbContext<FXDB1Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FXDB1")));

            // adding dependencies
            serviceCollections.AddSingleton(provider => Configuration);
            serviceCollections.AddScoped<ILogWrapper, NLogWrapper>((ctx) =>
                new NLogWrapper(Configuration.GetConnectionString("NLogWrapperDB")));
            serviceCollections.AddScoped<HttpClient, HttpClient>();
            serviceCollections.AddScoped<IHttpService, HttpService>();
            serviceCollections.AddScoped<IBarxFxService, BarxFxService>();
            serviceCollections.AddScoped<ITradeService, TradeService>();
            serviceCollections.AddScoped<ITradeUow, TradeUow>();
            serviceCollections.AddScoped<IClientCompanyService, ClientCompanyService>();
            serviceCollections.AddScoped<IClientCompanyUow, ClientCompanyUow>();
            serviceCollections.AddScoped<ICountryService, CountryService>();
            serviceCollections.AddScoped<ICountryUow, CountryUow>();
            serviceCollections.AddScoped<IIdentityService, IdentityService>();
            serviceCollections.AddScoped<IUserService, UserService>();
            serviceCollections.AddScoped<IUserUow, UserUow>();
            serviceCollections.AddScoped<IConfigWrapper, ConfigWrapper>();
            serviceCollections.AddScoped<IStatementUoW, StatementUoW>();
            serviceCollections.AddScoped<IStatementService, StatementService>();
            serviceCollections.AddScoped<ICurrencyUoW, CurrencyUoW>();
            serviceCollections.AddScoped<ICurrencyService, CurrencyService>();
            serviceCollections.AddScoped<IClientCompanyAccountsUoW, ClientCompanyAccountsUoW>();
            serviceCollections.AddScoped<IClientCompanyAccountsService, ClientCompanyAccountsService>();
            serviceCollections.AddScoped<IPaymentUoW, PaymentUoW>();
            serviceCollections.AddScoped<ISettlementService, SettlementService>();
            serviceCollections.AddScoped<IOrderService, OrderService>();
            serviceCollections.AddScoped<IAppSettingService, AppSettingService>();
            serviceCollections.AddScoped<IAppSettingUow, AppSettingUow>();
            serviceCollections.AddScoped<IServiceEmail, ServiceEmailClient>();
            serviceCollections.AddScoped<IEmailSender, EmailSender>();

            return serviceCollections;
        }
    }
}
