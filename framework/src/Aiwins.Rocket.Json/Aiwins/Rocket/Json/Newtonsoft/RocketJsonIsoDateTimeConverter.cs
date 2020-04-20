using System;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Timing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aiwins.Rocket.Json.Newtonsoft {
    public class RocketJsonIsoDateTimeConverter : IsoDateTimeConverter, ITransientDependency {
        private readonly IClock _clock;

        public RocketJsonIsoDateTimeConverter (IClock clock, IOptions<RocketJsonOptions> abpJsonOptions) {
            _clock = clock;

            if (abpJsonOptions.Value.DefaultDateTimeFormat != null) {
                DateTimeFormat = abpJsonOptions.Value.DefaultDateTimeFormat;
            }
        }

        public override bool CanConvert (Type objectType) {
            if (objectType == typeof (DateTimeOffset) || objectType == typeof (DateTimeOffset?)) {
                return true;
            }

            return false;
        }

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var date = base.ReadJson (reader, objectType, existingValue, serializer) as DateTimeOffset?;

            if (date.HasValue) {
                return _clock.Normalize (date.Value);
            }

            return null;
        }

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            var date = value as DateTimeOffset?;
            base.WriteJson (writer, date.HasValue ? _clock.Normalize (date.Value) : value, serializer);
        }
    }
}