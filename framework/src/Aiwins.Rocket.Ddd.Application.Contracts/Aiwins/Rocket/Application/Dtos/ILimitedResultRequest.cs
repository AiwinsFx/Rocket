namespace Aiwins.Rocket.Application.Dtos {
    public interface ILimitedResultRequest {
        /// <summary>
        /// 最大数据量
        /// 通常用于数据分页
        /// </summary>
        int MaxResultCount { get; set; }
    }
}