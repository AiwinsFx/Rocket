using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Domain.Repositories {
    public abstract class RepositoryBase<TEntity> : BasicRepositoryBase<TEntity>, IRepository<TEntity>, IUnitOfWorkManagerAccessor
    where TEntity : class, IEntity {
        public IDataFilter DataFilter { get; set; }

        public ICurrentUser CurrentUser { get; set; }

        public ICurrentTenant CurrentTenant { get; set; }

        public IUnitOfWorkManager UnitOfWorkManager { get; set; }

        public IPermissionChecker PermissionChecker { get; set; }

        public virtual Type ElementType => GetQueryable ().ElementType;

        public virtual Expression Expression => GetQueryable ().Expression;

        public virtual IQueryProvider Provider => GetQueryable ().Provider;
        public virtual string PolicyName { get; set; }

        public virtual IQueryable<TEntity> WithDetails () {
            return GetQueryable ();
        }

        public virtual IQueryable<TEntity> WithDetails (params Expression<Func<TEntity, object>>[] propertySelectors) {
            return GetQueryable ();
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return GetEnumerator ();
        }

        public IEnumerator<TEntity> GetEnumerator () {
            return GetQueryable ().GetEnumerator ();
        }

        protected abstract IQueryable<TEntity> GetQueryable ();

        public abstract Task<TEntity> FindAsync (
            Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        public async Task<TEntity> GetAsync (
            Expression<Func<TEntity, bool>> predicate,
            bool includeDetails = true,
            CancellationToken cancellationToken = default) {
            var entity = await FindAsync (predicate, includeDetails, cancellationToken);

            if (entity == null) {
                throw new EntityNotFoundException (typeof (TEntity));
            }

            return entity;
        }

        public abstract Task DeleteAsync (Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);

        protected virtual async Task<TQueryable> ApplyDataFilters<TQueryable> (TQueryable query)
        where TQueryable : IQueryable<TEntity> {
            if (typeof (ISoftDelete).IsAssignableFrom (typeof (TEntity))) {
                query = (TQueryable) query.WhereIf (DataFilter.IsEnabled<ISoftDelete> (), e => ((ISoftDelete) e).IsDeleted == false);
            }

            var scope = await CheckPolicyScopeAsync ();
            switch (scope) {
                case PermissionScopeType.Platform:
                    // 平台
                    return query;
                case PermissionScopeType.Granted:
                case PermissionScopeType.All:
                    // 租户
                    if (typeof (IMultiTenant).IsAssignableFrom (typeof (TEntity))) {
                        var tenantId = CurrentTenant.Id;
                        query = (TQueryable) query.WhereIf (DataFilter.IsEnabled<IMultiTenant> (), e => ((IMultiTenant) e).TenantId == tenantId);
                    }
                    return query;
                case PermissionScopeType.Domain:
                    // 管辖
                    if (typeof (IMultiTenant).IsAssignableFrom (typeof (TEntity))) {
                        var tenantId = CurrentTenant.Id;
                        query = (TQueryable) query.WhereIf (DataFilter.IsEnabled<IMultiTenant> (), e => ((IMultiTenant) e).TenantId == tenantId);
                    }
                    return query;
                case PermissionScopeType.Owner:
                    // 个人
                    var userId = CurrentUser.Id;
                    if (typeof (IMultiTenant).IsAssignableFrom (typeof (TEntity))) {
                        var tenantId = CurrentTenant.Id;
                        query = (TQueryable) query.WhereIf (DataFilter.IsEnabled<IMultiTenant> (), e => ((IMultiTenant) e).TenantId == tenantId);
                    }
                    if (typeof (IMustHaveCreator).IsAssignableFrom (typeof (TEntity))) {
                        query = (TQueryable) query.Where (e => ((IMustHaveCreator) e).CreatorId == userId);
                    } else if (typeof (IMayHaveCreator).IsAssignableFrom (typeof (TEntity))) {
                        query = (TQueryable) query.Where (e => ((IMayHaveCreator) e).CreatorId.HasValue && ((IMayHaveCreator) e).CreatorId.Value == userId);
                    }
                    return query;
                default:
                    // 禁止
                    return (TQueryable) query.Where(m => false);
            }
        }

        protected virtual async Task<PermissionScopeType> CheckPolicyScopeAsync () {
            if (string.IsNullOrEmpty (PolicyName)) {
                return PermissionScopeType.Granted;
            }

            var result = await PermissionChecker.GetResultAsync (PolicyName);
            return result.ScopeType;
        }

    }

    public abstract class RepositoryBase<TEntity, TKey> : RepositoryBase<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey> {
            public abstract Task<TEntity> GetAsync (TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

            public abstract Task<TEntity> FindAsync (TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

            public virtual async Task DeleteAsync (TKey id, bool autoSave = false, CancellationToken cancellationToken = default) {
                var entity = await FindAsync (id, cancellationToken : cancellationToken);
                if (entity == null) {
                    return;
                }

                await DeleteAsync (entity, autoSave, cancellationToken);
            }
        }
}