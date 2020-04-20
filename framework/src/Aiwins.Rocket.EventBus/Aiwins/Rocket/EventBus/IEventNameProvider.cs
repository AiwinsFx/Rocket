using System;

namespace Aiwins.Rocket.EventBus {
    public interface IEventNameProvider {
        string GetName (Type eventType);
    }
}