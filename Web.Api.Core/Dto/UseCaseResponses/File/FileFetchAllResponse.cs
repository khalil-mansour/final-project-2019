using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class FileFetchAllResponse : UseCaseResponseMessage
    {
        public IEnumerable<File> Files { get; }
        public IEnumerable<Error> Errors { get; }

        public FileFetchAllResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public FileFetchAllResponse(IEnumerable<File> files, bool success = false, string message = null) : base(success, message)
        {
            Files = files;
        }
    }
}
