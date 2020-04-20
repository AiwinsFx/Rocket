using System;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.ObjectMapping;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AutoMapper {
    [DependsOn (
        typeof (RocketObjectMappingModule),
        typeof (RocketObjectExtendingModule))]
    public class RocketAutoMapperModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddAutoMapperObjectMapper ();

            var mapperAccessor = new MapperAccessor ();
            context.Services.AddSingleton<IMapperAccessor> (_ => mapperAccessor);
            context.Services.AddSingleton<MapperAccessor> (_ => mapperAccessor);
        }

        public override void OnPreApplicationInitialization (ApplicationInitializationContext context) {
            CreateMappings (context.ServiceProvider);
        }

        private void CreateMappings (IServiceProvider serviceProvider) {
            using (var scope = serviceProvider.CreateScope ()) {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<RocketAutoMapperOptions>> ().Value;

                void ConfigureAll (IRocketAutoMapperConfigurationContext ctx) {
                    foreach (var configurator in options.Configurators) {
                        configurator (ctx);
                    }
                }

                void ValidateAll (IConfigurationProvider config) {
                    foreach (var profileType in options.ValidatingProfiles) {
                        config.AssertConfigurationIsValid (((Profile) Activator.CreateInstance (profileType)).ProfileName);
                    }
                }

                var mapperConfiguration = new MapperConfiguration (mapperConfigurationExpression => {
                    ConfigureAll (new RocketAutoMapperConfigurationContext (mapperConfigurationExpression, scope.ServiceProvider));
                });

                ValidateAll (mapperConfiguration);

                scope.ServiceProvider.GetRequiredService<MapperAccessor> ().Mapper = mapperConfiguration.CreateMapper ();
            }
        }
    }
}