using CryptoMarketDataAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoMarketDataAPI.Services
{
    public interface IDeribitInstrumentPriceService
    {
        Task<IEnumerable<DeribitInstrumentPriceItem>> GetInstrumentPriceHistorFor(IEnumerable<string> instrumentNames, DateTimeOffset fromDate, DateTimeOffset toDate);
    }
}