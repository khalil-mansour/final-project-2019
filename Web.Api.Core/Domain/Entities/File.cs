using System;

namespace Web.Api.Core.Domain.Entities
{
    public class File
    {
        public int UserId { get; }
        public int DocumentType { get; }
        public string StorageId { get; }
        public DateTime CreatedDate { get; }
        public bool Visible { get; }
        public string FileName { get; }

        internal File(int userId, int documentType, DateTime createdDate, string storageId, bool visible, string fileName)
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
