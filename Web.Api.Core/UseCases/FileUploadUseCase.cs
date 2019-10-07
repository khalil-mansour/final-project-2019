using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
   public sealed class FileUploadUseCase : IFileUploadUseCase
    {       

        private readonly IFileRepository _fileRepository;

        public FileUploadUseCase(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        
        public async Task<bool> Handle(FileUploadRequest message, Interfaces.IOutputPort<FileUploadResponse> outputPort)
        {
            var uploadedFileId = UploadFile(message);
            var response = await _fileRepository.
                Create(new File(
                    message.UserId,
                    message.DocTypeId,
                    message.LastModified,
                    message.Url,
                    message.Visible),
                    uploadedFileId);
            
            outputPort.Handle(response.Success ? new FileUploadResponse(uploadedFileId, false, null) : new FileUploadResponse(new[] { new Error("upload_failure", "File could not be uploaded.") }));
            return response.Success;
        }
        
        private string UploadFile(FileUploadRequest message)
        { 
            StorageClient storage = StorageClient.Create();
            using (var f = message.File.OpenReadStream())
            {
                // *** TO VERIFY ***
                string objectName = message.UserId.ToString() + DateTime.Now.ToString(); 

                var response = storage.UploadObject(message.BucketName, objectName, null, f);
                return response.Id;
            }
        }
    }
}
