using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
   public class FileUploadResponse : UseCaseResponseMessage
    {
        public Error Error { get; }
        public Domain.Entities.File File { get; }

        public FileUploadResponse(Error error, bool success = false, string message = null) : base (success, message)
        {
            Error = error;
        }

        public FileUploadResponse(Domain.Entities.File file, bool success = false, string message = null) : base(success, message)
        {
            File = file;
        }
    }
}
