using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class FileFetchResponse : UseCaseResponseMessage
    {
        public Domain.Entities.File File { get; }
        public Error Error { get; }

        public FileFetchResponse(Error error, bool success = false, string message = null) : base(success, message)
        {
            Error = error;
        }

        public FileFetchResponse(Domain.Entities.File file, bool success = false, string message = null) : base(success, message)
        {
            File = file;
        }
    }
}
