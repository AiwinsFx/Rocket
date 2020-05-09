using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities.Events;
using Aiwins.Rocket.EventBus;

namespace Aiwins.Docs.Documents.FullSearch.Elastic
{
    public class DocumentChangedEventHandler : ILocalEventHandler<EntityCreatedEventData<Document>>, 
        ILocalEventHandler<EntityUpdatedEventData<Document>>,
        ILocalEventHandler<EntityDeletedEventData<Document>>,
        ITransientDependency
    {
        private readonly DocsElasticSearchOptions _options;
        private readonly IDocumentFullSearch _documentFullSearch;

        public DocumentChangedEventHandler(IDocumentFullSearch documentFullSearch, IOptions<DocsElasticSearchOptions> options)
        {
            _documentFullSearch = documentFullSearch;
            _options = options.Value;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<Document> eventData)
        {
            await AddOrUpdate(eventData.Entity);
        }

        public async Task HandleEventAsync(EntityUpdatedEventData<Document> eventData)
        {
            await AddOrUpdate(eventData.Entity);
        }

        private async Task AddOrUpdate(Document document)
        {
            if (_options.Enable)
            {
                await _documentFullSearch.AddOrUpdateAsync(document);
            }
        }

        public async Task HandleEventAsync(EntityDeletedEventData<Document> eventData)
        {
            if (_options.Enable)
            {
                await _documentFullSearch.DeleteAsync(eventData.Entity.Id);
            }
        }
    }
}