namespace Aiwins.Rocket.Uow {
    public interface IUnitOfWorkManagerAccessor {
        IUnitOfWorkManager UnitOfWorkManager { get; }
    }
}