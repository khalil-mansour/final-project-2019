using Microsoft.AspNetCore.Http;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class FileUploadRequest : IUseCaseRequest<FileUploadResponse>
    {
        public IFormFile File {get;}
        public int UserId { get; }
        public string BucketName { get; }

        public FileUploadRequest(IFormFile file, int userId, string bucketName)
        {
           File = file;
           UserId = userId;
           BucketName = bucketName;

        }
    }
}
