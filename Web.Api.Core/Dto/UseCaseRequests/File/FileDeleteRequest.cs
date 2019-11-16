using Newtonsoft.Json;
using Web.Api.Core.Dto.UseCaseResponses.File;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.File
{
    public class FileDeleteRequest : IUseCaseRequest<FileDeleteResponse>
    {
        [JsonProperty("doc_id")]
        public int DocumentId { get; }

        public FileDeleteRequest(int docId)
        {
            DocumentId = docId;
        }
    }
}
