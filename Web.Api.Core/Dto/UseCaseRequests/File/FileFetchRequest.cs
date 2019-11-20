using Newtonsoft.Json;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileFetchRequest : IUseCaseRequest<FileFetchResponse>
    {

        [JsonProperty("file_id")]
        public int FileID { get; }

        public FileFetchRequest(int fileId)
        {
            FileID = fileId;
        }
    }
}
