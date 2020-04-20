using AutoMapper;

namespace Aiwins.Rocket.AutoMapper {
    public interface IMapperAccessor {
        IMapper Mapper { get; }
    }
}