using Aiwins.Rocket.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.MultiTenancy {
    public class CurrentUserTenantResolveContributor : TenantResolveContributorBase {
        public const string ContributorName = "CurrentUser";

        public override string Name => ContributorName;

        public override void Resolve (ITenantResolveContext context) {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser> ();
            if (currentUser.IsAuthenticated != true) {
                return;
            }

            context.Handled = true;
            context.TenantIdOrName = currentUser.TenantId?.ToString ();
        }
    }
}