using CryptoMarketDataAPI.Mappers;
using CryptoMarketDataAPI.Models;
using DeribitDAL.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMarketDataAPI.Services
{
    public class DeribitInstrumentPriceService : IDeribitInstrumentPriceService
    {
        private readonly IDeribitInstrumentPriceHistoryGateway _deribitInstrumentPriceHistoryGateway;

        public DeribitInstrumentPriceService(IDeribitInstrumentPriceHistoryGateway deribitInstrumentPriceHistoryGateway)
        {
            _deribitInstrumentPriceHistoryGateway = deribitInstrumentPriceHistoryGateway;
        }

        public async Task<IEnumerable<DeribitInstrumentPriceItem>> GetInstrumentPriceHistorFor(IEnumerable<string> instrumentNames, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            //var allSearchTasks = (instrumentNames ?? Enumerable.Empty<string>()).Select(instrumentNames => _deribitInstrumentPriceHistoryGateway.GetHistoryBy(instrumentNames, fromDate, toDate));

            //var searchResults = new List<DeribitInstrumentPriceItem>();
            //var taskResults = (await Task.WhenAll(allSearchTasks));

            //foreach (var item in taskResults)
            //{
            //    searchResults.AddRange(item.ToExternalModel());
            //}

            //return searchResults;

            return (await _deribitInstrumentPriceHistoryGateway.GetHistoryFor(instrumentNames, fromDate, toDate)).ToExternalModel();
        }
    }
}
