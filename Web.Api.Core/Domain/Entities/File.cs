using System;

namespace Web.Api.Core.Domain.Entities
{
    public class File
    {
        public string UserId { get; }
        public int DocumentType { get; }
        public string StorageId { get; }
        public DateTime CreatedDate { get; }
        public bool Visible { get; }
        public string FileName { get; }

        internal File(string userId, int documentType, DateTime createdDate, string storageId, bool visible, string fileName)
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
