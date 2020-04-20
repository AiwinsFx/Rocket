using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.EventBus.Local {
    public class RocketLocalEventBusOptions {
        public ITypeList<IEventHandler> Handlers { get; }

        public RocketLocalEventBusOptions () {
            Handlers = new TypeList<IEventHandler> ();
        }
    }
}