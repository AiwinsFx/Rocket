using System;
using System.Threading.Tasks;
using MyCompanyName.MyProjectName.Users;
using MongoDB.Driver.Linq;
using Shouldly;
using Aiwins.Rocket.Domain.Repositories;
using Xunit;

namespace MyCompanyName.MyProjectName.MongoDB.Samples
{
    /* This is just an example test class.
     * Normally, you don't test ROCKET framework code
     * (like default User repository IRepository<User, Guid> here).
     * Only test your custom repository methods.
     */
    [Collection(MongoTestCollection.Name)]
    public class SampleRepositoryTests : MyProjectNameMongoDbTestBase
    {
        private readonly IRepository<User, Guid> _userRepository;

        public SampleRepositoryTests()
        {
            _userRepository = GetRequiredService<IRepository<User, Guid>>();
        }

        [Fact]
        public async Task Should_Query_User()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                //Act
                var adminUser = await _userRepository
                    .GetMongoQueryable()
                    .FirstOrDefaultAsync(u => u.UserName == "admin");

                //Assert
                adminUser.ShouldNotBeNull();
            });
        }
    }
}
