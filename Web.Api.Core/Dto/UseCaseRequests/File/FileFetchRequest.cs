using Newtonsoft.Json;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileFetchRequest : IUseCaseRequest<FileFetchResponse>
    {

        [JsonProperty("storage_file_id")]
        public string StorageId { get; }

        public FileFetchRequest(string storageId)
        {
            StorageId = storageId;
        }
    }
}
