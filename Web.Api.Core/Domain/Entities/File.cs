using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Web.Api.Core.UnitTests")]
namespace Web.Api.Core.Domain.Entities
{
    public class File
    {
        public int Id { get; }

        public string UserId { get; }

        public int DocumentType { get; }

        public string StorageId { get; }

        public DateTime CreatedDate { get; }

        public bool Visible { get; }

        public string FileName { get; }

        public string Url { get; set; }

        internal File(string userId, int documentType, string fileName, string storageId, DateTime createdDate, bool visible)
        {
            UserId = userId;
            DocumentType = documentType;
            CreatedDate = createdDate;
            StorageId = storageId;
            Visible = visible;
            FileName = fileName;
        }

        internal File(int id, string userId, int documentType, string fileName, string storageId, DateTime createdDate, bool visible)
        {
            Id = id;
            UserId = userId;
            DocumentType = documentType;
            CreatedDate = createdDate;
            StorageId = storageId;
            Visible = visible;
            FileName = fileName;
        }
    }
}
