using System;
using System.Reflection;
using Aiwins.Rocket.Json.Newtonsoft;
using Aiwins.Rocket.Reflection;
using Aiwins.Rocket.Timing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aiwins.Rocket.AspNetCore.Mvc.Json {
    public class RocketMvcJsonContractResolver : DefaultContractResolver {
        private readonly Lazy<RocketJsonIsoDateTimeConverter> _dateTimeConverter;

        public RocketMvcJsonContractResolver (IServiceCollection services) {
            _dateTimeConverter = services.GetServiceLazy<RocketJsonIsoDateTimeConverter> ();

            NamingStrategy = new CamelCaseNamingStrategy ();
        }

        protected override JsonProperty CreateProperty (MemberInfo member, MemberSerialization memberSerialization) {
            JsonProperty property = base.CreateProperty (member, memberSerialization);

            ModifyProperty (member, property);

            return property;
        }

        protected virtual void ModifyProperty (MemberInfo member, JsonProperty property) {
            if (property.PropertyType != typeof (DateTime) && property.PropertyType != typeof (DateTime?)) {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute> (member) == null) {
                property.Converter = _dateTimeConverter.Value;
            }

        }
    }
}