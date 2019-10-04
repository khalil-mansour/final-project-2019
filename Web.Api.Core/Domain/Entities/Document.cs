using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class Document
    {
        public int UserId { get; }
        public int DocumentType { get; }
        public string Name { get; }
        public DateTime LastModified { get; }
        public string Url { get; }
        public bool  Visible { get; }
    }
}
