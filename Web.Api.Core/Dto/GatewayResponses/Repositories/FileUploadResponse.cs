using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class FileUploadResponse : BaseGatewayResponse
    {
        public File File { get; }
        public FileUploadResponse(File file = null, bool suceess = false, Error error = null) : base(suceess, error)
        {
            File = file;
        }
    }
}
