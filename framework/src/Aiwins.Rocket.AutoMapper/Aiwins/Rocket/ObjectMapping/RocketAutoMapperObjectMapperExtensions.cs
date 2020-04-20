using Aiwins.Rocket.AutoMapper;
using AutoMapper;

namespace Aiwins.Rocket.ObjectMapping {
    public static class RocketAutoMapperObjectMapperExtensions {
        public static IMapper GetMapper (this IObjectMapper objectMapper) {
            return objectMapper.AutoObjectMappingProvider.GetMapper ();
        }

        public static IMapper GetMapper (this IAutoObjectMappingProvider autoObjectMappingProvider) {
            if (autoObjectMappingProvider is AutoMapperAutoObjectMappingProvider autoMapperAutoObjectMappingProvider) {
                return autoMapperAutoObjectMappingProvider.MapperAccessor.Mapper;
            }

            throw new RocketException ($"Given object is not an instance of {typeof(AutoMapperAutoObjectMappingProvider).AssemblyQualifiedName}. The type of the given object it {autoObjectMappingProvider.GetType().AssemblyQualifiedName}");
        }
    }
}