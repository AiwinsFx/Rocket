using Nest;

namespace Aiwins.Docs.Documents.FullSearch.Elastic
{
    public interface IElasticClientProvider
    {
        IElasticClient GetClient();
    }
}