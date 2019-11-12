using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    class FileDownloadRequest
    {
        
        [JsonProperty("uid")]
        public string UserId { get; set; }

        [JsonProperty("storage_file_id")]
        public string StorageId { get; }

    }
}
