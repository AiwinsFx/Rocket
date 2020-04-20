using System;

namespace Aiwins.Rocket.EventBus {
    public interface IEventHandlerDisposeWrapper : IDisposable {
        IEventHandler EventHandler { get; }
    }
}