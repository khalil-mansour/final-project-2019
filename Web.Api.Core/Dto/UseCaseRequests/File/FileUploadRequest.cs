using Microsoft.AspNetCore.Http;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseResponses;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileUploadRequest : IUseCaseRequest<FileUploadResponse>
    {
        public IFormFile File {get;}
        public string UserId { get; }
        public int DocTypeId { get; }
        public bool Visible { get; }

        public FileUploadRequest(IFormFile file, string userId, int docTypeId, bool visible)
        {
            File = file;
            UserId = userId;
            DocTypeId = docTypeId;
            Visible = visible;
        }
    }
}
