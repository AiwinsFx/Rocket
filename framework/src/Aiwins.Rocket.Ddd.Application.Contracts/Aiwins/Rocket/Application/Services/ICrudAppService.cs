using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;

namespace Aiwins.Rocket.Application.Services {
    public interface ICrudAppService<TEntityDto, in TKey>
        : ICrudAppService<TEntityDto, TKey, PagedAndSortedResultRequestDto> {

        }

    public interface ICrudAppService<TEntityDto, in TKey, in TGetListInput>
        : ICrudAppService<TEntityDto, TKey, TGetListInput, TEntityDto, TEntityDto> {

        }

    public interface ICrudAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        : ICrudAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput> {

        }

    public interface ICrudAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : ICrudAppService<TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput> {

        }

    public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : IApplicationService {
            Task<TGetOutputDto> GetAsync (TKey id);

            Task<PagedResultDto<TGetListOutputDto>> GetListAsync (TGetListInput input);

            Task<TGetOutputDto> CreateAsync (TCreateInput input);

            Task<TGetOutputDto> UpdateAsync (TKey id, TUpdateInput input);

            Task DeleteAsync (TKey id);
        }
}