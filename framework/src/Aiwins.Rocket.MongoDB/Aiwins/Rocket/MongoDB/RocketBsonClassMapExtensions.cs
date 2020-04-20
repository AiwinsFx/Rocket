using System;
using System.Reflection;
using Aiwins.Rocket.Data;
using MongoDB.Bson.Serialization;

namespace Aiwins.Rocket.MongoDB {
    public static class RocketBsonClassMapExtensions {
        public static void ConfigureRocketConventions (this BsonClassMap map) {
            map.AutoMap ();
            map.TryConfigureExtraProperties ();
        }

        public static bool TryConfigureExtraProperties<TEntity> (this BsonClassMap<TEntity> map)
        where TEntity : class, IHasExtraProperties {
            var property = map.ClassType.GetProperty (
                nameof (IHasExtraProperties.ExtraProperties),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty
            );

            if (property?.DeclaringType != map.ClassType) {
                return false;
            }

            map.SetExtraElementsMember (new BsonMemberMap (
                map,
                typeof (TEntity).GetMember (nameof (IHasExtraProperties.ExtraProperties)) [0]));

            return true;
        }

        public static bool TryConfigureExtraProperties (this BsonClassMap map) {
            if (!map.ClassType.IsAssignableTo<IHasExtraProperties> ()) {
                return false;
            }

            var property = map.ClassType.GetProperty (
                nameof (IHasExtraProperties.ExtraProperties),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty
            );

            if (property?.DeclaringType != map.ClassType) {
                return false;
            }

            map.MapExtraElementsMember (property);

            return true;
        }
    }
}