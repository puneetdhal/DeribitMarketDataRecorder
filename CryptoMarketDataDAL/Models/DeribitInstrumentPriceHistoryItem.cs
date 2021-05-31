using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeribitDAL.Models
{
    [Table("DeribitInstrumentPriceHistory")]
    public class DeribitInstrumentPriceHistoryItem
    {
        [Key]
        internal int Id { get; set; }

        [Required]
        public string InstrumentName { get; set; }

        [Required]
        public decimal MinPrice { get; set; }

        [Required]
        public decimal MaxPrice { get; set; }

        [Required]
        public decimal MarkPrice { get; set; }

        [Required]
        public decimal LastPrice { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; }
    }
}
