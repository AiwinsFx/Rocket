using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Users;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;
using Xunit;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore.Samples
{
    /* This is just an example test class.
     * Normally, you don't test ROCKET framework code
     * (like default User repository IRepository<User, Guid> here).
     * Only test your custom repository methods.
     */
    public class SampleRepositoryTests : MyProjectNameEntityFrameworkCoreTestBase
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
                    .Where(u => u.UserName == "admin")
                    .FirstOrDefaultAsync();

                //Assert
                adminUser.ShouldNotBeNull();
            });
        }
    }
}
