using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DeribitService.BackgroundWorkers
{
    /// <summary>
    /// Class responsible for subscribing to and receving raw notifications from the Deribit server
    /// As a background service it can be hosted in an independent application. For simplicity, it is hosted along with the API application
    /// For simplicity in implementation it inherits from <see cref="BackgroundService"/> and only overrides the <see cref="BackgroundService.ExecuteAsync(CancellationToken)"/> method.
    /// The raw notifications from the Deribit server are added to a global queue.
    /// Which channels to subscribe to is delegated to <see cref="IDeribitSubscriptionChannelsProvider"/>
    /// </summary>
    public class DeribitInstrumentPriceDataSubscriber : BackgroundService
    {
        private readonly DeribitServerOptions _deribitServerOptions;
        private readonly ILogger<DeribitInstrumentPriceDataSubscriber> _logger;

        private IDeribitSubscriptionChannelsProvider _deribitSubscriptionChannelsProvider;
        private IDeribitInstrumentDataQueue _deribitInstrumentDataQueue;

        public DeribitInstrumentPriceDataSubscriber(
            IDeribitSubscriptionChannelsProvider deribitSubscriptionChannelsProvider,
            IDeribitInstrumentDataQueue deribitInstrumentDataQueue,
            IOptions<DeribitServerOptions> deribitServerOptions,
            ILogger<DeribitInstrumentPriceDataSubscriber> logger)
        {
            _deribitSubscriptionChannelsProvider = deribitSubscriptionChannelsProvider;
            _deribitInstrumentDataQueue = deribitInstrumentDataQueue;
            _deribitServerOptions = deribitServerOptions.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Worker started at: {time}", DateTimeOffset.Now);

            using (var deribitWs = new ClientWebSocket())
                try
                {
                    await deribitWs.ConnectAsync(new Uri(_deribitServerOptions.WsServerUrl), stoppingToken);

                    using (var deribitRpc = new JsonRpc(new WebSocketMessageHandler(deribitWs)))
                    {
                        deribitRpc.AddLocalRpcMethod(
                            _deribitServerOptions.WsNotificationMethod,
                            (Action<JToken, CancellationToken>)DeribitNotificationHandler);

                        deribitRpc.StartListening();

                        var subscriptionResult = await deribitRpc.InvokeWithParameterObjectAsync<object>(
                            _deribitServerOptions.WsPublicSubscriptionMethod,
                            new { channels = _deribitSubscriptionChannelsProvider.GetChannels() });

                        // To keep the WS Connection running till the stoppingToken indicates otherwise
                        // This can be improved by directly implementing the Start/Stop methods of the IHostedService interface
                        // and explicitly handling the disposing of ClientWebSocket and JsonRpc objects
                        await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                    }

                }
                catch (Exception ex)
                {
                    // To make the service more robust we should add appropriate retry mechanisms, in case connection fails
                    Console.WriteLine($"ERROR - {ex.Message}");
                }
        }

        private void DeribitNotificationHandler(JToken token, CancellationToken cancellationToken)
        {
            _deribitInstrumentDataQueue.Enqueue(token);
        }
    }
}
