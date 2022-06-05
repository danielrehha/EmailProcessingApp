using EmailProcessingApp.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EmailProcessingApp.Tests.Shared.Core
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        //private readonly StreamWriter _logStream = new StreamWriter("integration_test_logs.txt", append: true);
        public IConfiguration Configuration { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var directory = Directory.GetCurrentDirectory();
            var settingsFile = "test_appsettings.json";
            var developmentSettingsFile = "test_appsettings.Development.json";

            builder.ConfigureAppConfiguration(conf =>
            {
                if(!File.Exists(developmentSettingsFile))
                {
                    Configuration = conf.AddJsonFile(Path.Combine(directory, settingsFile)).Build();
                    return;
                }

                Configuration = conf.AddJsonFile(Path.Combine(directory, developmentSettingsFile)).Build();
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                var configurationDescriptorList = new List<ServiceDescriptor>();

                configurationDescriptorList.AddRange(services.Where(
                    d => d.ServiceType == typeof(IConfiguration)).ToList());

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                foreach(var configurationDescriptor in configurationDescriptorList)
                {
                    services.Remove(configurationDescriptor);
                }

                services.AddSingleton(Configuration);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    var connectionString = Configuration.GetConnectionString("SqlConnection");
                    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
                    options.LogTo(message => Debug.WriteLine(message));
                    options.EnableSensitiveDataLogging();
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var scopedServices = scope.ServiceProvider;
            });
        }
    }
}
