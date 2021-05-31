using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeribitService.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeribitRecorderService(this IServiceCollection services)
        {
            services.AddSingleton<IDeribitInstrumentDataQueue, DeribitInstrumentDataQueue>();
            services.AddSingleton<IDeribitSubscriptionChannelsProvider, DeribitSubscriptionChannelsProvider>();

            return services;
        }
    }
}
