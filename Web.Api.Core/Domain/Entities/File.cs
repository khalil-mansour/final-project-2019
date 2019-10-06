using System;

namespace Web.Api.Core.Domain.Entities
{
    public class File
    {
        public int UserId { get; }
        public int DocumentType { get; }
        public string Name { get; }
        public DateTime LastModified { get; }
        public string Url { get; }
        public bool  Visible { get; }

        internal File(int userId, int documentType, DateTime lastModified, string url, bool visible)
        {
            UserId = userId;
            DocumentType = documentType;
            LastModified = lastModified;
            Url = url;
            Visible = visible;
        }
    }
}
