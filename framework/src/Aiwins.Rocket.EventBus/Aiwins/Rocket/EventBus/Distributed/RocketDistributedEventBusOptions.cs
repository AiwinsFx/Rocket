using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.EventBus.Distributed {
    public class RocketDistributedEventBusOptions {
        public ITypeList<IEventHandler> Handlers { get; }
        public EtoMappingDictionary EtoMappings { get; set; }

        public RocketDistributedEventBusOptions () {
            Handlers = new TypeList<IEventHandler> ();
            EtoMappings = new EtoMappingDictionary ();
        }
    }
}