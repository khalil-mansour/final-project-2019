using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public FileUploadUseCase(IConfiguration configuraton, IFileRepository fileRepository)
        {
            _configuration = configuraton; 
            _fileRepository = fileRepository;
        }
        
        public async Task<bool> Handle(FileUploadRequest message, Interfaces.IOutputPort<FileUploadResponse> outputPort)
        {
            var uploadedFileName = UploadFile(message);

            var response = await _fileRepository.
                Create(new File(
                    message.UserId,
                    message.DocTypeId,
                    DateTime.Now,
                    uploadedFileName,
                    message.Visible)
                   );
            
            outputPort.Handle(response.Success ? new FileUploadResponse(uploadedFileName, false, null) : new FileUploadResponse(new[] { new Error("upload_failure", "File could not be uploaded.") }));
            return response.Success;
        }
        
        private string UploadFile(FileUploadRequest message)
        { 
            StorageClient storage = StorageClient.Create();
                        using (var f = message.File.OpenReadStream())
            {
                // *** TO VERIFY ***
                string objectName = $"{message.UserId}{message.DocTypeId}{DateTime.Now.ToString("MM/dd/yyyy")}";

                var response = storage.UploadObject(_configuration.GetSection("BucketName").Value, objectName, null, f);
                return response.Name;
            }
        }
    }
}
