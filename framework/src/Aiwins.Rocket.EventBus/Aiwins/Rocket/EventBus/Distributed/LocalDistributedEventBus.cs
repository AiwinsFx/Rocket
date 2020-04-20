﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket.Collections;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.EventBus.Local;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.EventBus.Distributed {
    [Dependency (TryRegister = true)]
    [ExposeServices (typeof (IDistributedEventBus), typeof (LocalDistributedEventBus))]
    public class LocalDistributedEventBus : IDistributedEventBus, ISingletonDependency {
        private readonly ILocalEventBus _localEventBus;

        protected IServiceScopeFactory ServiceScopeFactory { get; }

        protected RocketDistributedEventBusOptions RocketDistributedEventBusOptions { get; }

        public LocalDistributedEventBus (
            ILocalEventBus localEventBus,
            IServiceScopeFactory serviceScopeFactory,
            IOptions<RocketDistributedEventBusOptions> distributedEventBusOptions) {
            _localEventBus = localEventBus;
            ServiceScopeFactory = serviceScopeFactory;
            RocketDistributedEventBusOptions = distributedEventBusOptions.Value;
            Subscribe (distributedEventBusOptions.Value.Handlers);
        }

        public virtual void Subscribe (ITypeList<IEventHandler> handlers) {
            foreach (var handler in handlers) {
                var interfaces = handler.GetInterfaces ();
                foreach (var @interface in interfaces) {
                    if (!typeof (IEventHandler).GetTypeInfo ().IsAssignableFrom (@interface)) {
                        continue;
                    }

                    var genericArgs = @interface.GetGenericArguments ();
                    if (genericArgs.Length == 1) {
                        Subscribe (genericArgs[0], new IocEventHandlerFactory (ServiceScopeFactory, handler));
                    }
                }
            }
        }

        /// <inheritdoc/>
        public virtual IDisposable Subscribe<TEvent> (IDistributedEventHandler<TEvent> handler) where TEvent : class {
            return Subscribe (typeof (TEvent), handler);
        }

        public IDisposable Subscribe<TEvent> (Func<TEvent, Task> action) where TEvent : class {
            return _localEventBus.Subscribe (action);
        }

        public IDisposable Subscribe<TEvent> (ILocalEventHandler<TEvent> handler) where TEvent : class {
            return _localEventBus.Subscribe (handler);
        }

        public IDisposable Subscribe<TEvent, THandler> () where TEvent : class where THandler : IEventHandler, new () {
            return _localEventBus.Subscribe<TEvent, THandler> ();
        }

        public IDisposable Subscribe (Type eventType, IEventHandler handler) {
            return _localEventBus.Subscribe (eventType, handler);
        }

        public IDisposable Subscribe<TEvent> (IEventHandlerFactory factory) where TEvent : class {
            return _localEventBus.Subscribe<TEvent> (factory);
        }

        public IDisposable Subscribe (Type eventType, IEventHandlerFactory factory) {
            return _localEventBus.Subscribe (eventType, factory);
        }

        public void Unsubscribe<TEvent> (Func<TEvent, Task> action) where TEvent : class {
            _localEventBus.Unsubscribe (action);
        }

        public void Unsubscribe<TEvent> (ILocalEventHandler<TEvent> handler) where TEvent : class {
            _localEventBus.Unsubscribe (handler);
        }

        public void Unsubscribe (Type eventType, IEventHandler handler) {
            _localEventBus.Unsubscribe (eventType, handler);
        }

        public void Unsubscribe<TEvent> (IEventHandlerFactory factory) where TEvent : class {
            _localEventBus.Unsubscribe<TEvent> (factory);
        }

        public void Unsubscribe (Type eventType, IEventHandlerFactory factory) {
            _localEventBus.Unsubscribe (eventType, factory);
        }

        public void UnsubscribeAll<TEvent> () where TEvent : class {
            _localEventBus.UnsubscribeAll<TEvent> ();
        }

        public void UnsubscribeAll (Type eventType) {
            _localEventBus.UnsubscribeAll (eventType);
        }

        public Task PublishAsync<TEvent> (TEvent eventData)
        where TEvent : class {
            return _localEventBus.PublishAsync (eventData);
        }

        public Task PublishAsync (Type eventType, object eventData) {
            return _localEventBus.PublishAsync (eventType, eventData);
        }
    }
}