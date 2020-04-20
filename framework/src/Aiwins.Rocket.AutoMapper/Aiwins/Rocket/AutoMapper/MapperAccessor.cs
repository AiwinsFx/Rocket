using AutoMapper;

namespace Aiwins.Rocket.AutoMapper {
    internal class MapperAccessor : IMapperAccessor {
        public IMapper Mapper { get; set; }
    }
}