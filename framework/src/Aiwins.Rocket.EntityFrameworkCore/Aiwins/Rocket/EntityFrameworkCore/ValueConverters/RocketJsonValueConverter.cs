using Aiwins.Rocket.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Aiwins.Rocket.EntityFrameworkCore.ValueConverters {
    public class RocketJsonValueConverter<TPropertyType> : ValueConverter<TPropertyType, string> {
        public RocketJsonValueConverter () : base (
            d => SerializeObject (d),
            s => DeserializeObject (s)) {

        }

        private static string SerializeObject (TPropertyType d) {
            return JsonConvert.SerializeObject (d, Formatting.None);
        }

        private static TPropertyType DeserializeObject (string s) {
            return JsonConvert.DeserializeObject<TPropertyType> (s);
        }
    }
}