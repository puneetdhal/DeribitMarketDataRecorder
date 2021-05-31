using System.Collections.Generic;

namespace DeribitService
{
    /// <summary>
    /// Class responsible for providing valid Deribit channels to which a service can subscribe.
    /// For simplicity only two channels have been hard-coded here.
    /// More elaborate implementation would make use of the Deribit API to get active instruments and then construct a dynamic list of channels to be given to the subscriber.
    /// The subscription could then even be distributed over multiple instances of the Background service.
    /// </summary>
    public class DeribitSubscriptionChannelsProvider : IDeribitSubscriptionChannelsProvider
    {
        public IReadOnlyCollection<string> GetChannels()
        {
            return new List<string>
            {
                "ticker.BTC-PERPETUAL.100ms",
                "ticker.ETH-PERPETUAL.100ms",
            };
        }
    }
}
