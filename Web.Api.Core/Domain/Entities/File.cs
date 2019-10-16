using Newtonsoft.Json;
using System;

namespace Web.Api.Core.Domain.Entities
{
    public class File
    {

        [JsonProperty("uid")]
        public string UserId { get; }

        [JsonProperty("document_type_id")]
        public int DocumentType { get; }

        [JsonProperty("storage_file_id")]
        public string StorageId { get; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; }

        [JsonProperty("visiable")]
        public bool Visible { get; }

        [JsonProperty("user_file_name")]
        public string FileName { get; }

        internal File(string userId, int documentType, string fileName, string storageId, DateTime createdDate, bool visible)
        {
            UserId = userId;
            DocumentType = documentType;
            CreatedDate = createdDate;
            StorageId = storageId;
            Visible = visible;
            FileName = fileName;
        }
    }
}
