using System;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现接口 <see cref="IPagedAndSortedResultRequest"/>。
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest {
        public virtual string Sorting { get; set; }
    }
}