using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class FileUploadRequest
    {

        public IFormFile File { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("document_type_id")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
