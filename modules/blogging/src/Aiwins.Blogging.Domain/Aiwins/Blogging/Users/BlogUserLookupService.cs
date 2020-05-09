using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Users;

namespace Aiwins.Blogging.Users
{
    public class BlogUserLookupService : UserLookupService<BlogUser, IBlogUserRepository>, IBlogUserLookupService
    {
        public BlogUserLookupService(
            IBlogUserRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {
            
        }

        protected override BlogUser CreateUser(IUserData externalUser)
        {
            return new BlogUser(externalUser);
        }
    }
}