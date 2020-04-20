namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 请求排序
    /// </summary>
    public interface ISortedResultRequest {
        /// <summary>
        /// 排序信息
        /// 包含排序字段和升降序方式 (ASC or DESC)
        /// 通过 (,)进行分割，以便包含更多的排序规则
        /// </summary>
        /// <example>
        /// 示例:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// </example>
        string Sorting { get; set; }
    }
}