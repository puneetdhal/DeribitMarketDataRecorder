using DeribitDAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeribitService
{
    public class DeribitInstrumentTickerNotification
    {
        public string Channel { get; set; }

        public DeribitInstrumentTickerNotificationData Data { get; set; }
    }

    public class DeribitInstrumentTickerNotificationData
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("instrument_name")]
        public string InstrumentName { get; set; }

        [JsonProperty("min_price")]
        public decimal MinPrice { get; set; }

        [JsonProperty("max_price")]
        public decimal MaxPrice { get; set; }

        [JsonProperty("mark_price")]
        public decimal MarkPrice { get; set; }

        [JsonProperty("last_price")]
        public decimal LastPrice { get; set; }
    }

    public static class DeribitInstrumentTickerNotificationExtensions
    {
        internal static DeribitInstrumentPriceHistoryItem ToDataModel(this DeribitInstrumentTickerNotification notification)
        {
            return new DeribitInstrumentPriceHistoryItem
            {
                InstrumentName = notification.Data.InstrumentName,
                MinPrice = notification.Data.MinPrice,
                MaxPrice = notification.Data.MaxPrice,
                MarkPrice = notification.Data.MarkPrice,
                LastPrice = notification.Data.LastPrice,
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(notification.Data.Timestamp),// new DateTimeOffset(notification.Data.Timestamp, TimeSpan.Zero)
            };
        }
    }
}
