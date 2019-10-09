using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IFileRepository
    {
        Task<FileUploadResponse> Create(File file);
        Task<FileFetchResponse> Fetch(string storageId);
        Task<FileFetchAllResponse> FetchAll(string userId);
    }
}
