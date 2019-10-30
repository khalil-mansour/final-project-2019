﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class FileUploadRequest
    {

        public IFormFile File { get; set; }

        [JsonProperty("uid")]
        public string UserId { get; set; }

        [JsonProperty("documenttypeid")]
        public int DocumentTypeId { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
