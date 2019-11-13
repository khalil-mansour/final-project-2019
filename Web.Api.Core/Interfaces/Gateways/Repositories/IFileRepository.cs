using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.File;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IFileRepository
    {
        Task<FileUploadRepoResponse> Create(File file);
        Task<FileFetchRepoResponse> Fetch(string storageId);
        Task<FileFetchRepoResponse> FetchByDocId(int docID);
        Task<FileFetchAllRepoResponse> FetchAll(string userId);
        Task<FileDeleteRepoResponse> Delete(int docId);
    }
}
