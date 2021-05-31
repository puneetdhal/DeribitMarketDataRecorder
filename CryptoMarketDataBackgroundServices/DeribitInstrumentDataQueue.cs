using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace DeribitService
{
    /// <summary>
    /// Class representing the global queue to register all the raw notifications coming from Deribit.
    /// The current architecture requires the Queue to be thread-safe.
    /// More elaborate implementations would include distributed and persistent queues
    /// </summary>
    internal class DeribitInstrumentDataQueue : IDeribitInstrumentDataQueue
    {
        private static ConcurrentQueue<JToken> _queue = new ConcurrentQueue<JToken>();

        public void Enqueue(JToken data)
        {
            _queue.Enqueue(data);
        }

        public bool TryDequeue(out JToken data)
        {
            return _queue.TryDequeue(out data);
        }
    }
}
