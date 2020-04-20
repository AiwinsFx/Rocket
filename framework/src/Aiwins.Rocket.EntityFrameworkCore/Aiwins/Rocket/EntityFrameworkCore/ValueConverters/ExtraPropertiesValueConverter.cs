using System;
using System.Collections.Generic;
using Aiwins.Rocket.ObjectExtending;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Aiwins.Rocket.EntityFrameworkCore.ValueConverters {
    public class ExtraPropertiesValueConverter : ValueConverter<Dictionary<string, object>, string> {
        public ExtraPropertiesValueConverter (Type entityType) : base (
            d => SerializeObject (d, entityType),
            s => DeserializeObject (s)) {

        }

        private static string SerializeObject (Dictionary<string, object> extraProperties, Type entityType) {
            var copyDictionary = new Dictionary<string, object> (extraProperties);

            if (entityType != null) {
                var objectExtension = ObjectExtensionManager.Instance.GetOrNull (entityType);
                if (objectExtension != null) {
                    foreach (var property in objectExtension.GetProperties ()) {
                        if (property.IsMappedToFieldForEfCore ()) {
                            copyDictionary.Remove (property.Name);
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject (copyDictionary, Formatting.None);
        }

        private static Dictionary<string, object> DeserializeObject (string extraPropertiesAsJson) {
            return JsonConvert.DeserializeObject<Dictionary<string, object>> (extraPropertiesAsJson);
        }
    }
}