using Microsoft.AspNetCore.Http;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseResponses;
using Newtonsoft.Json;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileUploadRequest : IUseCaseRequest<FileUploadResponse>
    {

        public IFormFile File {get;}

        [JsonProperty("uid")]
        public string UserId { get; }

        [JsonProperty("document_type_id")]
        public int DocTypeId { get; }

        [JsonProperty("visible")]
        public bool Visible { get; }

        public FileUploadRequest(IFormFile file, string userId, int docTypeId, bool visible)
        {
            File = file;
            UserId = userId;
            DocTypeId = docTypeId;
            Visible = visible;
        }
    }
}
