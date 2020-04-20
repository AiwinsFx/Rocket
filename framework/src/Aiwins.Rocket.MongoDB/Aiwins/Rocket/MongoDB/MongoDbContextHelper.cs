using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Reflection;
using MongoDB.Driver;

namespace Aiwins.Rocket.MongoDB {
    internal static class MongoDbContextHelper {
        public static IEnumerable<Type> GetEntityTypes (Type dbContextType) {
            return
            from property in dbContextType.GetTypeInfo ().GetProperties (BindingFlags.Public | BindingFlags.Instance)
            where
            ReflectionHelper.IsAssignableToGenericType (property.PropertyType, typeof (IMongoCollection<>)) &&
                typeof (IEntity).IsAssignableFrom (property.PropertyType.GenericTypeArguments[0])
            select property.PropertyType.GenericTypeArguments[0];
        }
    }
}