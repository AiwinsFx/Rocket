namespace Aiwins.Rocket.EntityFrameworkCore {
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : IEfCoreDbContext {
            TDbContext GetDbContext ();
        }
}