using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.File
{
    public sealed class FileUploadRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.File File { get; }
        public FileUploadRepoResponse(Domain.Entities.File file = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            File = file;
        }
    }
}
