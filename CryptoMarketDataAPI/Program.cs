using DeribitDAL;
using DeribitService.BackgroundWorkers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeribitMarketDataRecorder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using(var context = new DeribitDbContext())
            {
                context.Database.EnsureCreated();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices(services =>
            {
                services.AddHostedService<DeribitInstrumentPriceDataSubscriber>();
                services.AddHostedService<DeribitInstrumentPriceDataProcessor>();
            });
    }
}
