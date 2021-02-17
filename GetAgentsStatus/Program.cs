
using GetAgentsStatus.Connectors;
using GetAgentsStatus.Repositories;
using GetAgentsStatus.Services;
using GetAgentStatus.Connectors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Configuration;
using System.IO;

namespace GetAgentsStatus.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddNLog("NLog.config");

            });

            var config = LoadConfiguration();
            services.AddSingleton(config);

            var connection = config.GetValue<string>("SQLConnectionString:DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(connection));

            services.AddSingleton(con => new Connector(config));
            services.AddScoped<ITCXRepository, TCXRepository>();
            services.AddScoped<IAgentStatusRepository, AgentStatusRepository>();
            services.AddScoped<IAgentAvailbilityRepository, AgentAvailbilityRepository>();
            services.AddScoped<IAgentAvailbilityService, AgentAvailbilityService>();


            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            return builder.Build();
        }
    }
}
