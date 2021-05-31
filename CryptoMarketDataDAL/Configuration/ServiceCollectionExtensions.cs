using DeribitDAL.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace DeribitDAL.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IDeribitInstrumentPriceHistoryGateway, DeribitInstrumentPriceHistoryGateway>();

            return services;
        }
    }
}
