using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.File
{
    public sealed class FileFetchAllRepoResponse : BaseGatewayResponse
    {
        public IEnumerable<Domain.Entities.File> Files { get; }

        public FileFetchAllRepoResponse(IEnumerable<Domain.Entities.File> files = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Files = files;
        }
    }
}
