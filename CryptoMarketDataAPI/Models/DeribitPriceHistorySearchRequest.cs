using System;

namespace CryptoMarketDataAPI.Models
{
    public class DeribitPriceHistorySearchRequest
    {
        public string[] InstrumentNames { get; set; }

        public DateTimeOffset FromDate { get; set; }

        public DateTimeOffset ToDate { get; set; }
    }
}
