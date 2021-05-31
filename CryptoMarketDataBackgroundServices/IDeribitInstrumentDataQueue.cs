using Newtonsoft.Json.Linq;

namespace DeribitService
{
    public interface IDeribitInstrumentDataQueue
    {
        void Enqueue(JToken data);
        bool TryDequeue(out JToken data);
    }
}