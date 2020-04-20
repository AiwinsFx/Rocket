using System.Linq;
using Aiwins.Rocket.Domain.Entities;

namespace Aiwins.Rocket.Auditing
{
    public static class EntityHistorySelectorListExtensions
    {
        public const string AllEntitiesSelectorName = "Rocket.Entities.All";

        public static void AddAllEntities(this IEntityHistorySelectorList selectors)
        {
            if (selectors.Any(s => s.Name == AllEntitiesSelectorName))
            {
                return;
            }

            selectors.Add(new NamedTypeSelector(AllEntitiesSelectorName, t => typeof(IEntity).IsAssignableFrom(t)));
        }
    }
}
