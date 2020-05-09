using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Testing;

namespace MyCompanyName.MyProjectName
{
    /* All test classes are derived from this class, directly or indirectly.
     */
    public abstract class MyProjectNameTestBase<TStartupModule> : RocketIntegratedTest<TStartupModule> 
        where TStartupModule : IRocketModule
    {
        protected override void SetRocketApplicationCreationOptions(RocketApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        protected virtual Task WithUnitOfWorkAsync(Func<Task> func)
        {
            return WithUnitOfWorkAsync(new RocketUnitOfWorkOptions(), func);
        }

        protected virtual async Task WithUnitOfWorkAsync(RocketUnitOfWorkOptions options, Func<Task> action)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                using (var uow = uowManager.Begin(options))
                {
                    await action();

                    await uow.CompleteAsync();
                }
            }
        }

        protected virtual Task<TResult> WithUnitOfWorkAsync<TResult>(Func<Task<TResult>> func)
        {
            return WithUnitOfWorkAsync(new RocketUnitOfWorkOptions(), func);
        }

        protected virtual async Task<TResult> WithUnitOfWorkAsync<TResult>(RocketUnitOfWorkOptions options, Func<Task<TResult>> func)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                using (var uow = uowManager.Begin(options))
                {
                    var result = await func();
                    await uow.CompleteAsync();
                    return result;
                }
            }
        }
    }
}
