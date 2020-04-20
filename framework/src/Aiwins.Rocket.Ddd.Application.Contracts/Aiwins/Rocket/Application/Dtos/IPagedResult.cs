namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 数据分页
    /// </summary>
    /// <typeparam name="T">集合的数据类型 <see cref="IListResult{T}.Items"/> list</typeparam>
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount {

    }
}