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
        public Error Error { get; }

        public FileFetchAllResponse(Error error, bool success = false, string message = null) : base(success, message)
        {
            Error = error;
        }

        public FileFetchAllResponse(IEnumerable<File> files, bool success = false, string message = null) : base(success, message)
        {
            Files = files;
        }
    }
}
