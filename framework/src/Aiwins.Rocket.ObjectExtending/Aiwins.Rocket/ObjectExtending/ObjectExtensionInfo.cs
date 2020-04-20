﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.ObjectExtending
{
    public class ObjectExtensionInfo
    {
        [NotNull]
        public Type Type { get; }

        [NotNull]
        protected Dictionary<string, ObjectExtensionPropertyInfo> Properties { get; }

        [NotNull]
        public Dictionary<object, object> Configuration { get; }

        public ObjectExtensionInfo([NotNull] Type type)
        {
            Type = Check.AssignableTo<IHasExtraProperties>(type, nameof(type));
            Properties = new Dictionary<string, ObjectExtensionPropertyInfo>();
            Configuration = new Dictionary<object, object>();
        }

        public virtual bool HasProperty(string propertyName)
        {
            return Properties.ContainsKey(propertyName);
        }

        [NotNull]
        public virtual ObjectExtensionInfo AddOrUpdateProperty<TProperty>(
            [NotNull] string propertyName,
            [CanBeNull] Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            return AddOrUpdateProperty(
                typeof(TProperty),
                propertyName,
                configureAction
            );
        }

        [NotNull]
        public virtual ObjectExtensionInfo AddOrUpdateProperty(
            [NotNull] Type propertyType,
            [NotNull] string propertyName,
            [CanBeNull] Action<ObjectExtensionPropertyInfo> configureAction = null)
        {
            Check.NotNull(propertyType, nameof(propertyType));
            Check.NotNull(propertyName, nameof(propertyName));

            var propertyInfo = Properties.GetOrAdd(
                propertyName,
                () => new ObjectExtensionPropertyInfo(this, propertyType, propertyName)
            );

            configureAction?.Invoke(propertyInfo);

            return this;
        }

        [NotNull]
        public virtual ImmutableList<ObjectExtensionPropertyInfo> GetProperties()
        {
            return Properties.Values.ToImmutableList();
        }

        [CanBeNull]
        public virtual ObjectExtensionPropertyInfo GetPropertyOrNull(
            [NotNull] string propertyName)
        {
            Check.NotNullOrEmpty(propertyName, nameof(propertyName));

            return Properties.GetOrDefault(propertyName);
        }
    }
}