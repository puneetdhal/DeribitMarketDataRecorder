namespace DeribitService
{
    public class DeribitServerOptions
    {
        public const string SectionName = "DeribitServerOptions";

        public string WsServerUrl { get; set; }
        public string WsPublicSubscriptionMethod { get; set; }
        public string WsNotificationMethod { get; set; }
    }
}