using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PricePulse.Data;
using PricePulse.Services;
using Microsoft.EntityFrameworkCore;
using PricePulse.ConsoleApp;
using PricePulse.Core.Interfaces;
using PricePulse.Data.Repositories;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<PricePulseDbContext>();

            var app = services.GetRequiredService<App>();
            app.Run();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<PricePulseDbContext>(options =>
                    options.UseSqlServer(connectionString));

                // Register other services
                services.AddScoped<IConsumerPriceIndexEntryRepository, ConsumerPriceIndexEntryRepository>();
                services.AddScoped<IConsumerPriceIndexSeriesRepository, ConsumerPriceIndexSeriesRepository>();
                services.AddScoped<IBlsApiService, BlsApiService>();

                // Register the App class
                services.AddTransient<App>();
            });
}