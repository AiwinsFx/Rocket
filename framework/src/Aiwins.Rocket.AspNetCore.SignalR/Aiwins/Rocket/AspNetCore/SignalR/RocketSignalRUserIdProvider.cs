using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Users;
using Microsoft.AspNetCore.SignalR;

namespace Aiwins.Rocket.AspNetCore.SignalR {
    public class RocketSignalRUserIdProvider : IUserIdProvider, ITransientDependency {
        public ICurrentUser CurrentUser { get; }

        public RocketSignalRUserIdProvider (ICurrentUser currentUser) {
            CurrentUser = currentUser;
        }

        public virtual string GetUserId (HubConnectionContext connection) {
            return CurrentUser.Id?.ToString ();
        }
    }
}