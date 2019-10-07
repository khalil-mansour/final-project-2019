using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class FileUploadRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userid")]
        public int UserId { get; set; }

        [JsonProperty("documenttypeid")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastmodified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
