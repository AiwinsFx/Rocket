using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.ObjectExtending;

namespace Aiwins.Rocket.Domain.Entities {
    [Serializable]
    public abstract class AggregateRoot : Entity,
        IAggregateRoot,
        IGeneratesDomainEvents,
        IHasExtraProperties,
        IHasConcurrencyStamp {
            public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

            [DisableAuditing]
            public virtual string ConcurrencyStamp { get; set; }

            private readonly ICollection<object> _localEvents = new Collection<object> ();
            private readonly ICollection<object> _distributedEvents = new Collection<object> ();

            protected AggregateRoot () {
                ExtraProperties = new Dictionary<string, object> ();
                ConcurrencyStamp = Guid.NewGuid ().ToString ("N");
            }

            protected virtual void AddLocalEvent (object eventData) {
                _localEvents.Add (eventData);
            }

            protected virtual void AddDistributedEvent (object eventData) {
                _distributedEvents.Add (eventData);
            }

            public virtual IEnumerable<object> GetLocalEvents () {
                return _localEvents;
            }

            public virtual IEnumerable<object> GetDistributedEvents () {
                return _distributedEvents;
            }

            public virtual void ClearLocalEvents () {
                _localEvents.Clear ();
            }

            public virtual void ClearDistributedEvents () {
                _distributedEvents.Clear ();
            }

            public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
                return ExtensibleObjectValidator.GetValidationErrors (
                    this,
                    validationContext
                );
            }
        }

    [Serializable]
    public abstract class AggregateRoot<TKey> : Entity<TKey>,
        IAggregateRoot<TKey>,
        IGeneratesDomainEvents,
        IHasExtraProperties,
        IHasConcurrencyStamp {
            public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

            [DisableAuditing]
            public virtual string ConcurrencyStamp { get; set; }

            private readonly ICollection<object> _localEvents = new Collection<object> ();
            private readonly ICollection<object> _distributedEvents = new Collection<object> ();

            protected AggregateRoot () {
                ExtraProperties = new Dictionary<string, object> ();
                ConcurrencyStamp = Guid.NewGuid ().ToString ("N");
            }

            protected AggregateRoot (TKey id) : base (id) {
                ExtraProperties = new Dictionary<string, object> ();
                ConcurrencyStamp = Guid.NewGuid ().ToString ("N");
            }

            protected virtual void AddLocalEvent (object eventData) {
                _localEvents.Add (eventData);
            }

            protected virtual void AddDistributedEvent (object eventData) {
                _distributedEvents.Add (eventData);
            }

            public virtual IEnumerable<object> GetLocalEvents () {
                return _localEvents;
            }

            public virtual IEnumerable<object> GetDistributedEvents () {
                return _distributedEvents;
            }

            public virtual void ClearLocalEvents () {
                _localEvents.Clear ();
            }

            public virtual void ClearDistributedEvents () {
                _distributedEvents.Clear ();
            }

            public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext) {
                return ExtensibleObjectValidator.GetValidationErrors (
                    this,
                    validationContext
                );
            }
        }
}