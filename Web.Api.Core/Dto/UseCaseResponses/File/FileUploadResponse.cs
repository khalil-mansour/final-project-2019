using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
   public class FileUploadResponse : UseCaseResponseMessage
    {
        public IEnumerable<Error> Errors { get; }
        public string UploadedFileId { get;  }

        public FileUploadResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base (success, message)
        {
            Errors = errors;
        }

        public FileUploadResponse(string name, bool success = false, string message = null) : base(success, message)
        {
            UploadedFileId = name;
        }
    }
}
