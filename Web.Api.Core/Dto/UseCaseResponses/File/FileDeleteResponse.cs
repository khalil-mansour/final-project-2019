using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.File
{
    public class FileDeleteResponse : UseCaseResponseMessage
    {
        public Domain.Entities.File File { get; }

        public IEnumerable<Error> Errors { get; }

        public FileDeleteResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public FileDeleteResponse(Domain.Entities.File file, bool success = false, string message = null) : base(success, message)
        {
            File = file;
        }
    }
}
