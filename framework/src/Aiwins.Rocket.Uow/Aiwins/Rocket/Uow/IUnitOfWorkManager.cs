using JetBrains.Annotations;

namespace Aiwins.Rocket.Uow {
    public interface IUnitOfWorkManager {
        [CanBeNull]
        IUnitOfWork Current { get; }

        [NotNull]
        IUnitOfWork Begin ([NotNull] RocketUnitOfWorkOptions options, bool requiresNew = false);

        [NotNull]
        IUnitOfWork Reserve ([NotNull] string reservationName, bool requiresNew = false);

        void BeginReserved ([NotNull] string reservationName, [NotNull] RocketUnitOfWorkOptions options);

        bool TryBeginReserved ([NotNull] string reservationName, [NotNull] RocketUnitOfWorkOptions options);
    }
}