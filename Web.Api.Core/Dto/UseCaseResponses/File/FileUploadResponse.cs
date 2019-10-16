using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
   public class FileUploadResponse : UseCaseResponseMessage
    {
        public Error Error { get; }
        public string UploadedFileId { get;  }

        public FileUploadResponse(Error error, bool success = false, string message = null) : base (success, message)
        {
            Error = error;
        }

        public FileUploadResponse(string name, bool success = false, string message = null) : base(success, message)
        {
            UploadedFileId = name;
        }
    }
}
