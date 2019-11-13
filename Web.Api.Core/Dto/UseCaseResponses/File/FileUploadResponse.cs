using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
   public class FileUploadResponse : UseCaseResponseMessage
    {
        public Error Error { get; }
        public File File { get; }

        public FileUploadResponse(Error error, bool success = false, string message = null) : base (success, message)
        {
            Error = error;
        }

        public FileUploadResponse(File file, bool success = false, string message = null) : base(success, message)
        {
            File = file;
        }
    }
}
