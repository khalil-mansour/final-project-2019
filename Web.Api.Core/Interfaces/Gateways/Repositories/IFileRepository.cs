using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IFileRepository
    {
        Task<FileUploadRepoResponse> Create(File file);
        Task<FileFetchRepoResponse> Fetch(string storageId);
        Task<FileFetchAllRepoResponse> FetchAll(string userId);
        File GetFile(int fileId);
    }
}
