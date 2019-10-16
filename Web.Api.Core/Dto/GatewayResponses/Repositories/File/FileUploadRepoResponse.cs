using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class FileUploadRepoResponse : BaseGatewayResponse
    {
        public File File { get; }
        public FileUploadRepoResponse(File file = null, bool success = false, Error error = null) : base(success, error)
        {
            File = file;
        }
    }
}
