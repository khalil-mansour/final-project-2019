using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IFileRepository
    {
        Task<FileUploadResponse> Create(File file);
        Task<FetchFileResponse> Fetch(int user_id, int doc_type, string name);
    }
}
