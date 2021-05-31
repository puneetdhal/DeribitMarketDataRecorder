using System.Collections.Generic;

namespace DeribitService
{
    public interface IDeribitSubscriptionChannelsProvider
    {
        IReadOnlyCollection<string> GetChannels();
    }
}
