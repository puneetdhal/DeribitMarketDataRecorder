using DeribitDAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeribitDAL.Gateways
{
    public interface IDeribitInstrumentPriceHistoryGateway
    {
        Task AddHistoryItem(DeribitInstrumentPriceHistoryItem item);

        Task<IReadOnlyCollection<DeribitInstrumentPriceHistoryItem>> GetHistoryFor(IEnumerable<string> instrumentNames, DateTimeOffset fromDate, DateTimeOffset toDate);
    }
}
