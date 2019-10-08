using System;

namespace Web.Api.Core.Domain.Entities
{
    public class File
    {
        public int UserId { get; }
        public int DocumentType { get; }
        public string Name { get; }
        public DateTime CreatedDate { get; }
        public bool  Visible { get; }

        internal File(int userId, int documentType, DateTime createdDate, string name, bool visible)
        {
            UserId = userId;
            DocumentType = documentType;
            CreatedDate = createdDate;
            Name = name;
            Visible = visible;
        }
    }
}
