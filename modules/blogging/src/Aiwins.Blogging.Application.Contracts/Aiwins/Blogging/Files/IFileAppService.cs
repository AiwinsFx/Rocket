using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Blogging.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<RawFileDto> GetAsync(string name);

        Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input);
    }
}
