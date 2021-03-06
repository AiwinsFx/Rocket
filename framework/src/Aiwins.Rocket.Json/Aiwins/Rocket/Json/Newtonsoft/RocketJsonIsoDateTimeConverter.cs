using System;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Timing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aiwins.Rocket.Json.Newtonsoft {
    public class RocketJsonIsoDateTimeConverter : IsoDateTimeConverter, ITransientDependency {
        private readonly IClock _clock;

        public RocketJsonIsoDateTimeConverter (IClock clock, IOptions<RocketJsonOptions> rocketJsonOptions) {
            _clock = clock;

            if (rocketJsonOptions.Value.DefaultDateTimeFormat != null) {
                DateTimeFormat = rocketJsonOptions.Value.DefaultDateTimeFormat;
            }
        }

        public override bool CanConvert (Type objectType) {
            if (objectType == typeof (DateTime) || objectType == typeof (DateTime?)) {
                return true;
            }

            return false;
        }

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var date = base.ReadJson (reader, objectType, existingValue, serializer) as DateTime?;

            if (date.HasValue) {
                return _clock.Normalize (date.Value);
            }

            return null;
        }

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            var date = value as DateTime?;
            base.WriteJson (writer, date.HasValue ? _clock.Normalize (date.Value) : value, serializer);
        }
    }
}