using Microsoft.AspNetCore.Http;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseResponses;


namespace Web.Api.Core.Dto.UseCaseRequests
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
