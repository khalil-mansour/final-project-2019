using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileFetchRequest : IUseCaseRequest<FileFetchResponse>
    {
        public string StorageId { get; }

        public FileFetchRequest(string storageId)
        {
            StorageId = storageId;
        }
    }
}
