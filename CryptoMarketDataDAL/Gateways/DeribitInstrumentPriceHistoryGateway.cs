using DeribitDAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeribitDAL.Gateways
{
    internal class DeribitInstrumentPriceHistoryGateway : IDeribitInstrumentPriceHistoryGateway
    {
        private ILogger<DeribitInstrumentPriceHistoryGateway> _logger;

        public DeribitInstrumentPriceHistoryGateway(ILogger<DeribitInstrumentPriceHistoryGateway> logger)
        {
            _logger = logger;
        }

        public async Task AddHistoryItem(DeribitInstrumentPriceHistoryItem item)
        {
            _logger.LogInformation($"Adding new item to {nameof(DeribitInstrumentPriceHistoryItem)} for: {item.InstrumentName}");

            using (var context = new DeribitDbContext())
            {
                context.DeribitInstrumentPriceHistory.Add(item);
                await context.SaveChangesAsync();
            }
        }

        public Task<IReadOnlyCollection<DeribitInstrumentPriceHistoryItem>> GetHistoryFor(IEnumerable<string> instrumentNames, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var results = new List<DeribitInstrumentPriceHistoryItem>();

            var instrumentNameFilters = (instrumentNames ?? Enumerable.Empty<string>()).ToList().AsQueryable();

            if (instrumentNameFilters.Any())
                using (var context = new DeribitDbContext())
                {
                    results =
                        context.DeribitInstrumentPriceHistory
                        .AsEnumerable()
                        .Where(item =>
                            instrumentNameFilters.Contains(item.InstrumentName, StringComparer.InvariantCultureIgnoreCase)
                            && item.Timestamp <= toDate
                            && item.Timestamp >= fromDate).ToList();
                }

            return Task.FromResult((IReadOnlyCollection<DeribitInstrumentPriceHistoryItem>)results);
        }
    }
}
