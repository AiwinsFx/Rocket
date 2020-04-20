using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.EntityFrameworkCore {
    internal static class DbContextHelper {
        public static IEnumerable<Type> GetEntityTypes (Type dbContextType) {
            return
            from property in dbContextType.GetTypeInfo ().GetProperties (BindingFlags.Public | BindingFlags.Instance)
            where
            ReflectionHelper.IsAssignableToGenericType (property.PropertyType, typeof (DbSet<>)) &&
                typeof (IEntity).IsAssignableFrom (property.PropertyType.GenericTypeArguments[0])
            select property.PropertyType.GenericTypeArguments[0];
        }
    }
}