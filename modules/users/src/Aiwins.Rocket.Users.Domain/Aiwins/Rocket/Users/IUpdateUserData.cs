using JetBrains.Annotations;

namespace Aiwins.Rocket.Users {
    public interface IUpdateUserData {
        bool Update ([NotNull] IUserData user);
    }
}