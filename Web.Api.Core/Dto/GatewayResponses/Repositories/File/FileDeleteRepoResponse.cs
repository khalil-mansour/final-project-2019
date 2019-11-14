using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.File
{
    public sealed class FileDeleteRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.File File { get; }

        public FileDeleteRepoResponse(Domain.Entities.File file, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            File = file;
        }
    }
}
