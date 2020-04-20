using Aiwins.Rocket.Uow;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public class RocketEfCoreDbContextInitializationContext {
        public IUnitOfWork UnitOfWork { get; }

        public RocketEfCoreDbContextInitializationContext (IUnitOfWork unitOfWork) {
            UnitOfWork = unitOfWork;
        }
    }
}