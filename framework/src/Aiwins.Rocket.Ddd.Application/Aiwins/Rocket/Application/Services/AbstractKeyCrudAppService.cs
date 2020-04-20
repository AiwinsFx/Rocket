using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories;
using Aiwins.Rocket.Linq;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.ObjectMapping;

namespace Aiwins.Rocket.Application.Services {
    public abstract class AbstractKeyCrudAppService<TEntity, TEntityDto, TKey>
        : AbstractKeyCrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity {
            protected AbstractKeyCrudAppService (IRepository<TEntity> repository) : base (repository) {

            }
        }

    public abstract class AbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : AbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto, TEntityDto>
        where TEntity : class, IEntity {
            protected AbstractKeyCrudAppService (IRepository<TEntity> repository) : base (repository) {

            }
        }

    public abstract class AbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : AbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntity : class, IEntity {
            protected AbstractKeyCrudAppService (IRepository<TEntity> repository) : base (repository) {

            }
        }

    public abstract class AbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : AbstractKeyCrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity {
            protected AbstractKeyCrudAppService (IRepository<TEntity> repository) : base (repository) {

            }

            protected override TEntityDto MapToGetListOutputDto (TEntity entity) {
                return MapToGetOutputDto (entity);
            }
        }

    public abstract class AbstractKeyCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : ApplicationService,
        ICrudAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity {
            public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }

            protected IRepository<TEntity> Repository { get; }

            protected virtual string GetPolicyName { get; set; }

            protected virtual string GetListPolicyName { get; set; }

            protected virtual string CreatePolicyName { get; set; }

            protected virtual string UpdatePolicyName { get; set; }

            protected virtual string DeletePolicyName { get; set; }

            protected AbstractKeyCrudAppService (IRepository<TEntity> repository) {
                Repository = repository;
                AsyncQueryableExecuter = DefaultAsyncQueryableExecuter.Instance;
            }

            public virtual async Task<TGetOutputDto> GetAsync (TKey id) {
                await CheckGetPolicyAsync ();

                var entity = await GetEntityByIdAsync (id);
                return MapToGetOutputDto (entity);
            }

            public virtual async Task<PagedResultDto<TGetListOutputDto>> GetListAsync (TGetListInput input) {
                await CheckGetListPolicyAsync ();

                var query = CreateFilteredQuery (input);

                var totalCount = await AsyncQueryableExecuter.CountAsync (query);

                query = ApplySorting (query, input);
                query = ApplyPaging (query, input);

                var entities = await AsyncQueryableExecuter.ToListAsync (query);

                return new PagedResultDto<TGetListOutputDto> (
                    totalCount,
                    entities.Select (MapToGetListOutputDto).ToList ()
                );
            }

            public virtual async Task<TGetOutputDto> CreateAsync (TCreateInput input) {
                await CheckCreatePolicyAsync ();

                var entity = MapToEntity (input);

                TryToSetTenantId (entity);

                await Repository.InsertAsync (entity, autoSave : true);

                return MapToGetOutputDto (entity);
            }

            public virtual async Task<TGetOutputDto> UpdateAsync (TKey id, TUpdateInput input) {
                await CheckUpdatePolicyAsync ();

                var entity = await GetEntityByIdAsync (id);
                //TODO: 此处检查输入的id是否与实体的id不同。如果是默认值，则进行规范化，否则抛出异常
                MapToEntity (input, entity);
                await Repository.UpdateAsync (entity, autoSave : true);

                return MapToGetOutputDto (entity);
            }

            public virtual async Task DeleteAsync (TKey id) {
                await CheckDeletePolicyAsync ();

                await DeleteByIdAsync (id);
            }

            protected abstract Task DeleteByIdAsync (TKey id);

            protected abstract Task<TEntity> GetEntityByIdAsync (TKey id);

            protected virtual async Task CheckGetPolicyAsync () {
                await CheckPolicyAsync (GetPolicyName);
            }

            protected virtual async Task CheckGetListPolicyAsync () {
                await CheckPolicyAsync (GetListPolicyName);
            }

            protected virtual async Task CheckCreatePolicyAsync () {
                await CheckPolicyAsync (CreatePolicyName);
            }

            protected virtual async Task CheckUpdatePolicyAsync () {
                await CheckPolicyAsync (UpdatePolicyName);
            }

            protected virtual async Task CheckDeletePolicyAsync () {
                await CheckPolicyAsync (DeletePolicyName);
            }

            /// <summary>
            /// 排序
            /// </summary>
            /// <param name="query">查询语句</param>
            /// <param name="input">输入参数</param>
            protected virtual IQueryable<TEntity> ApplySorting (IQueryable<TEntity> query, TGetListInput input) {
                // 尝试排序
                if (input is ISortedResultRequest sortInput) {
                    if (!sortInput.Sorting.IsNullOrWhiteSpace ()) {
                        return query.OrderBy (sortInput.Sorting);
                    }
                }

                // 尝试调用默认排序方法
                if (input is ILimitedResultRequest) {
                    return ApplyDefaultSorting (query);
                }

                // 不排序
                return query;
            }

            /// <summary>
            /// 默认排序
            /// </summary>
            /// <param name="query">查询语句</param>
            protected virtual IQueryable<TEntity> ApplyDefaultSorting (IQueryable<TEntity> query) {
                if (typeof (TEntity).IsAssignableTo<ICreationAuditedObject> ()) {
                    return query.OrderByDescending (e => ((ICreationAuditedObject) e).CreationTime);
                }

                throw new RocketException ("No sorting specified but this query requires sorting. Override the ApplyDefaultSorting method for your application service derived from AbstractKeyCrudAppService!");
            }

            /// <summary>
            /// 分页
            /// </summary>
            /// <param name="query">查询语句</param>
            /// <param name="input">输入参数</param>
            protected virtual IQueryable<TEntity> ApplyPaging (IQueryable<TEntity> query, TGetListInput input) {
                // 尝试执行分页操作
                if (input is IPagedResultRequest pagedInput) {
                    return query.PageBy (pagedInput);
                }

                // 尝试取最大数量
                if (input is ILimitedResultRequest limitedInput) {
                    return query.Take (limitedInput.MaxResultCount);
                }

                // 不分页
                return query;
            }

            /// <summary>
            /// 创建一个查询语句 <see cref="IQueryable{TEntity}"/>
            /// 此方法可以进行过滤查询，但不应该进行排序或分页操作
            /// 排序可以参考方法 <see cref="ApplySorting"/> ，分页参考方法 <see cref="ApplyPaging"/>
            /// </summary>
            /// <param name="input">输入参数</param>
            protected virtual IQueryable<TEntity> CreateFilteredQuery (TGetListInput input) {
                return Repository;
            }

            /// <summary>
            /// 将 <see cref="TEntity"/> 映射为 <see cref="TGetOutputDto"/>.
            /// 默认使用 <see cref="IObjectMapper"/>
            /// 可以被自定义映射器覆盖
            /// </summary>
            protected virtual TGetOutputDto MapToGetOutputDto (TEntity entity) {
                return ObjectMapper.Map<TEntity, TGetOutputDto> (entity);
            }

            /// <summary>
            /// 将 <see cref="TEntity"/> 映射为 <see cref="TGetListOutputDto"/>.
            /// 默认使用 <see cref="IObjectMapper"/>
            /// 可以被自定义映射器覆盖
            /// </summary>
            protected virtual TGetListOutputDto MapToGetListOutputDto (TEntity entity) {
                return ObjectMapper.Map<TEntity, TGetListOutputDto> (entity);
            }

            /// <summary>
            /// 将 <see cref="TCreateInput"/> 映射为 <see cref="TEntity"/> 创建新实体的时候
            /// 默认使用 <see cref="IObjectMapper"/>
            /// 可以被自定义映射器覆盖
            /// </summary>
            protected virtual TEntity MapToEntity (TCreateInput createInput) {
                var entity = ObjectMapper.Map<TCreateInput, TEntity> (createInput);
                SetIdForGuids (entity);
                return entity;
            }

            /// <summary>
            /// 当实体标识类型为 <see cref="Guid"/> 设置实体标识 <see cref="TKey"/> 
            /// 当创建一个新的对象的时候将调用此方法
            /// </summary>
            protected virtual void SetIdForGuids (TEntity entity) {
                var entityWithGuidId = entity as IEntity<Guid>;

                if (entityWithGuidId == null || entityWithGuidId.Id != Guid.Empty) {
                    return;
                }

                EntityHelper.TrySetId (
                    entityWithGuidId,
                    () => GuidGenerator.Create (),
                    true
                );
            }

            /// <summary>
            /// 将 <see cref="TUpdateInput"/> 映射为 <see cref="TEntity"/> 更新实体属性
            /// 默认使用 <see cref="IObjectMapper"/>
            /// 可以被自定义映射器覆盖
            /// </summary>
            protected virtual void MapToEntity (TUpdateInput updateInput, TEntity entity) {
                ObjectMapper.Map (updateInput, entity);
            }

            protected virtual void TryToSetTenantId (TEntity entity) {
                if (entity is IMultiTenant && HasTenantIdProperty (entity)) {
                    var tenantId = CurrentTenant.Id;

                    if (!tenantId.HasValue) {
                        return;
                    }

                    var propertyInfo = entity.GetType ().GetProperty (nameof (IMultiTenant.TenantId));

                    if (propertyInfo == null || propertyInfo.GetSetMethod (true) == null) {
                        return;
                    }

                    propertyInfo.SetValue (entity, tenantId);
                }
            }

            protected virtual bool HasTenantIdProperty (TEntity entity) {
                return entity.GetType ().GetProperty (nameof (IMultiTenant.TenantId)) != null;
            }
        }
}