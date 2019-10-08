using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class FileUploadRequest
    {

        [JsonProperty("userid")]
        public int UserId { get; set; }

        [JsonProperty("documenttypeid")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
