using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities.Events.Distributed;
using Aiwins.Rocket.EventBus.Distributed;
using Aiwins.Rocket.Users;

namespace Aiwins.Blogging.Users
{
    public class BlogUserSynchronizer :
        IDistributedEventHandler<EntityUpdatedEto<UserEto>>,
        ITransientDependency
    {
        protected IBlogUserRepository UserRepository { get; }
        protected IBlogUserLookupService UserLookupService { get; }

        public BlogUserSynchronizer(
            IBlogUserRepository userRepository, 
            IBlogUserLookupService userLookupService)
        {
            UserRepository = userRepository;
            UserLookupService = userLookupService;
        }

        public async Task HandleEventAsync(EntityUpdatedEto<UserEto> eventData)
        {
            var user = await UserRepository.FindAsync(eventData.Entity.Id);
            if (user == null)
            {
                user = await UserLookupService.FindByIdAsync(eventData.Entity.Id);
                if (user == null)
                {
                    return;
                }
            }

            if (user.Update(eventData.Entity))
            {
                await UserRepository.UpdateAsync(user);
            }
        }
    }
}
