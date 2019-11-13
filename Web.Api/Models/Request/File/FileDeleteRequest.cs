using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request.File
{
    public class FileDeleteRequest
    {
        [JsonProperty("document_id")]
        public int DocumentId { get; set; }
    }
}
