using JetBrains.Annotations;

namespace Aiwins.Rocket.Uow {
    public interface IUnitOfWorkAccessor {
        [CanBeNull]
        IUnitOfWork UnitOfWork { get; }

        void SetUnitOfWork ([CanBeNull] IUnitOfWork unitOfWork);
    }
}