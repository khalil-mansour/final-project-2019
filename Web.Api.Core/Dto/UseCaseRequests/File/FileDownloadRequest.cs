using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    class FileDownloadRequest
    {
        
        public string UserId { get; set; }
        public string StorageId { get; }

    }
}
