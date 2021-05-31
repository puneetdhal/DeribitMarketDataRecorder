using CryptoMarketDataAPI.Models;
using DeribitDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace CryptoMarketDataAPI.Mappers
{
    public static class DeribitInstrumentPriceHistoryItemExtensions
    {
        public static IEnumerable<DeribitInstrumentPriceItem> ToExternalModel(this IEnumerable<DeribitInstrumentPriceHistoryItem> deribitInstrumentPriceHistory)
        {
            return deribitInstrumentPriceHistory.Select(item => new DeribitInstrumentPriceItem
            {
                InstrumentName = item.InstrumentName,
                MinPrice = item.MinPrice,
                MaxPrice = item.MaxPrice,
                MarkPrice = item.MarkPrice,
                LastPrice = item.LastPrice,
                Timestamp = item.Timestamp,
            });
        }
    }
}
