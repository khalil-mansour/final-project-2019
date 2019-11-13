using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.File
{
    public sealed class FileFetchRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.File File { get; }

        public FileFetchRepoResponse(Domain.Entities.File file = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            File = file;
        }
    }
}
