using System;
using Aiwins.Rocket.Timing;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aiwins.Rocket.EntityFrameworkCore.ValueConverters {
    public class RocketDateTimeValueConverter : ValueConverter<DateTime?, DateTime?> {
        public RocketDateTimeValueConverter (IClock clock, [CanBeNull] ConverterMappingHints mappingHints = null) : base (
            x => x.HasValue ? clock.Normalize (x.Value) : x,
            x => x.HasValue ? clock.Normalize (x.Value) : x, mappingHints) { }
    }
}