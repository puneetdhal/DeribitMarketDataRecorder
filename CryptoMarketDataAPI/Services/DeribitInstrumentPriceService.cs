using CryptoMarketDataAPI.Mappers;
using CryptoMarketDataAPI.Models;
using DeribitDAL.Gateways;
using System;
using System.Collections.Generic;
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
            return (await _deribitInstrumentPriceHistoryGateway.GetHistoryFor(instrumentNames, fromDate, toDate)).ToExternalModel();
        }
    }
}
