using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class FileUploadRequest
    {
        [JsonProperty("file")]
        public IFormFile File { get; set; }

        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("document_type_id")]
        public int Document_Type_Id { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
