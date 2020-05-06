using System;
using System.Security.Claims;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Identity {
    public abstract class IdentityClaim : Entity<Guid>, IMultiTenant {
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 身份声明类型
        /// </summary>
        public virtual string ClaimType { get; protected set; }

        /// <summary>
        /// 身份声明的值
        /// </summary>
        public virtual string ClaimValue { get; protected set; }

        protected IdentityClaim () {

        }

        protected internal IdentityClaim (Guid id, [NotNull] Claim claim, Guid? tenantId) : this (id, claim.Type, claim.Value, tenantId) {

        }

        protected internal IdentityClaim (Guid id, [NotNull] string claimType, string claimValue, Guid? tenantId) {
            Check.NotNull (claimType, nameof (claimType));

            Id = id;
            ClaimType = claimType;
            ClaimValue = claimValue;
            TenantId = tenantId;
        }

        /// <summary>
        /// 创建一个新实例对象
        /// </summary>
        /// <returns></returns>
        public virtual Claim ToClaim () {
            return new Claim (ClaimType, ClaimValue);
        }

        public virtual void SetClaim ([NotNull] Claim claim) {
            Check.NotNull (claim, nameof (claim));

            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}