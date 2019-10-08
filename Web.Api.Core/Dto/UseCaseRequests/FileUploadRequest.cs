using Microsoft.AspNetCore.Http;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseResponses;
using System;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileUploadRequest : IUseCaseRequest<FileUploadResponse>
    {
        public IFormFile File {get;}
        public int UserId { get; }
        public int DocTypeId { get; }
        public bool Visible { get; }

        public FileUploadRequest(IFormFile file, int userId, int docTypeId, bool visible)
        {
            File = file;
            UserId = userId;
            DocTypeId = docTypeId;
            Visible = visible;
        }
    }
}
