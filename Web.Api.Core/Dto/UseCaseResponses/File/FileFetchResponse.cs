using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class FileFetchResponse : UseCaseResponseMessage
    {
        public Domain.Entities.File File { get; }
        public IEnumerable<Error> Errors { get; }

        public FileFetchResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public FileFetchResponse(Domain.Entities.File file, bool success = false, string message = null) : base(success, message)
        {
            File = file;
        }
    }
}
