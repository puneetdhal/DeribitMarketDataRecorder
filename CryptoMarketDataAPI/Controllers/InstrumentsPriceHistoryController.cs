using CryptoMarketDataAPI.Models;
using CryptoMarketDataAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeribitMarketDataRecorder.Controllers
{
    [ApiController]
    [Route("market-data")]
    public class InstrumentsPriceHistoryController : ControllerBase
    {
        private readonly ILogger<InstrumentsPriceHistoryController> _logger;
        private readonly IDeribitInstrumentPriceService _deribitInstrumentPriceService;

        public InstrumentsPriceHistoryController(IDeribitInstrumentPriceService deribitInstrumentPriceService, ILogger<InstrumentsPriceHistoryController> logger)
        {
            _logger = logger;
            _deribitInstrumentPriceService = deribitInstrumentPriceService;
        }

        [HttpPost("price-history/deribit/search")]
        public async Task<IEnumerable<DeribitInstrumentPriceItem>> Search([FromBody] DeribitPriceHistorySearchRequest deribitPriceHistorySearchRequest)
        {
            return await _deribitInstrumentPriceService.GetInstrumentPriceHistorFor(
                deribitPriceHistorySearchRequest.InstrumentNames,
                deribitPriceHistorySearchRequest.FromDate,
                deribitPriceHistorySearchRequest.ToDate);
        }
    }
}
