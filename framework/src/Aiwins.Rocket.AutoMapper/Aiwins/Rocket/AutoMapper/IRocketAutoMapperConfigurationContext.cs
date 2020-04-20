using System;
using AutoMapper;

namespace Aiwins.Rocket.AutoMapper {
    public interface IRocketAutoMapperConfigurationContext {
        IMapperConfigurationExpression MapperConfiguration { get; }

        IServiceProvider ServiceProvider { get; }
    }
}