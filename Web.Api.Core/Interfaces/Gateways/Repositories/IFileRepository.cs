using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.File;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IFileRepository
    {
        Task<FileUploadRepoResponse> Create(File file);
        Task<FileFetchRepoResponse> Fetch(int docId);
        Task<FileFetchAllRepoResponse> FetchAll(string userId);
        Task<FileDeleteRepoResponse> Delete(int docId);
        File GetFile(int fileId);
    }
}
