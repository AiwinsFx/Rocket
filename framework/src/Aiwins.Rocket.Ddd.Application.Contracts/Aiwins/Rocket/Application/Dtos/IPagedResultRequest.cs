namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 请求分页
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest {
        /// <summary>
        /// 分页跳过的数据数量
        /// </summary>
        int SkipCount { get; set; }
    }
}