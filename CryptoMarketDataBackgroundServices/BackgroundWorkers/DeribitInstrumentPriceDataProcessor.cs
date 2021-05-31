using DeribitDAL.Gateways;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeribitService.BackgroundWorkers
{
    /// <summary>
    /// Class responsible for processing raw Deribit notifications from the global Queue.
    /// As a Background Service this can be hosted in an independent application.
    /// For processing a lot of notifications, we could deploy multiple instances of this service.
    /// </summary>
    public class DeribitInstrumentPriceDataProcessor : BackgroundService
    {
        private readonly ILogger<DeribitInstrumentPriceDataProcessor> _logger;
        private readonly IDeribitInstrumentDataQueue _deribitInstrumentDataQueue;
        private readonly IDeribitInstrumentPriceHistoryGateway _deribitInstrumentPriceHistoryGateway;        

        public DeribitInstrumentPriceDataProcessor(
            ILogger<DeribitInstrumentPriceDataProcessor> logger,
            IDeribitInstrumentDataQueue deribitInstrumentDataQueue,
            IDeribitInstrumentPriceHistoryGateway deribitInstrumentPriceHistoryGateway)
        {
            _logger = logger;
            _deribitInstrumentDataQueue = deribitInstrumentDataQueue;
            _deribitInstrumentPriceHistoryGateway = deribitInstrumentPriceHistoryGateway;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Worker started at: {time}", DateTimeOffset.Now);
            await Task.Delay(100);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_deribitInstrumentDataQueue.TryDequeue(out var token))
                {
                    var notificationData = token.ToObject<DeribitInstrumentTickerNotification>();

                    await _deribitInstrumentPriceHistoryGateway.AddHistoryItem(notificationData.ToDataModel());
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
    }
}
