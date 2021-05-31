using System;

namespace CryptoMarketDataAPI.Models
{
    public class DeribitInstrumentPriceItem
    {
        public string InstrumentName { get; set; }
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal MarkPrice { get; set; }

        public decimal LastPrice { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
