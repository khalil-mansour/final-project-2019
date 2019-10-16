using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class FileFetchRepoResponse : BaseGatewayResponse
    {
        public File File { get; }

        public FileFetchRepoResponse(File file = null, bool success = false, Error error = null) : base(success, error)
        {
            File = file;
        }
    }
}
