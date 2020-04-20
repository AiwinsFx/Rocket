using System;
using System.ComponentModel.DataAnnotations;

namespace Aiwins.Rocket.Application.Dtos {
    /// <summary>
    /// 实现了接口 <see cref="IPagedResultRequest"/>。
    /// </summary>
    [Serializable]
    public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest {
        [Range (0, int.MaxValue)]
        public virtual int SkipCount { get; set; }
    }
}