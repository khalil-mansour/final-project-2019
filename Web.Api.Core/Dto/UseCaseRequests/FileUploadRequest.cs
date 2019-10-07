using Microsoft.AspNetCore.Http;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseResponses;
using System;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileUploadRequest : IUseCaseRequest<FileUploadResponse>
    {
        public IFormFile File {get;}
        public int Id { get; }
        public int UserId { get; }
        public int DocTypeId { get; }
        public string Name { get; }
        public DateTime LastModified { get; }
        public string Url { get; }
        public bool Visible { get; }
        public string BucketName { get; }

        public FileUploadRequest(IFormFile file, int id, int userId, int docTypeId, string name, DateTime lastModified, string url, bool visible, string bucketName)
        {
            File = file;
            Id = id;
            UserId = userId;
            DocTypeId = docTypeId;
            Name = name;
            LastModified = lastModified;
            Url = url;
            Visible = visible;
            BucketName = bucketName;
        }
    }
}
