using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Rocket.Identity {
    public interface IIdentityClaimTypeRepository : IBasicRepository<IdentityClaimType, Guid> {
        /// <summary>
        /// 判断是否存在给定名字 <see cref="IdentityClaimType"/> 实体对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="ignoredId">
        /// 可忽略的实体对象标识
        /// 如果存在给定的对象实体 <paramref name="ignoredId"/> 则忽略它
        /// </param>
        Task<bool> AnyAsync (string name, Guid? ignoredId = null);

        Task<List<IdentityClaimType>> GetListAsync (string sorting, int maxResultCount, int skipCount, string filter);
    }
}