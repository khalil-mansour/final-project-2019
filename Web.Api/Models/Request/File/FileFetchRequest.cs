using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class FileFetchRequest
    {
        [JsonProperty("storage_file_id")]
        public string StorageId { get; set; }
    }
}
