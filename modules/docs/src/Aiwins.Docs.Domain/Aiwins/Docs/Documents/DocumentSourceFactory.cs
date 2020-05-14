using System;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Docs.Documents {
    public class DocumentSourceFactory : IDocumentSourceFactory, ITransientDependency {
        protected DocumentSourceOptions Options { get; }
        protected IServiceProvider ServiceProvider { get; }

        public DocumentSourceFactory (
            IServiceProvider serviceProvider,
            IOptions<DocumentSourceOptions> options) {
            Options = options.Value;
            ServiceProvider = serviceProvider;
        }

        public virtual IDocumentSource Create (string sourceType) {
            var serviceType = Options.Sources.GetOrDefault (sourceType);
            if (serviceType == null) {
                throw new ApplicationException ($"Unknown document store: {sourceType}");
            }

            return (IDocumentSource) ServiceProvider.GetRequiredService (serviceType);
        }
    }
}