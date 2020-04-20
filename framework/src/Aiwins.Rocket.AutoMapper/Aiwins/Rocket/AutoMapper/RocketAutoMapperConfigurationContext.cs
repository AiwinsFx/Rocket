using System;
using AutoMapper;

namespace Aiwins.Rocket.AutoMapper {
    public class RocketAutoMapperConfigurationContext : IRocketAutoMapperConfigurationContext {
        public IMapperConfigurationExpression MapperConfiguration { get; }
        public IServiceProvider ServiceProvider { get; }

        public RocketAutoMapperConfigurationContext (
            IMapperConfigurationExpression mapperConfigurationExpression,
            IServiceProvider serviceProvider) {
            MapperConfiguration = mapperConfigurationExpression;
            ServiceProvider = serviceProvider;
        }
    }
}