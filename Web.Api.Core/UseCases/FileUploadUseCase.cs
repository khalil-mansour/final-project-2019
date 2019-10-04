using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseResponses;
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

        public Task<bool> Handle(FileUploadRequest message, Interfaces.IOutputPort<FileUploadResponse> outputPort)
        {
            var uploadedFileId = UploadFile(message);
            var response = await _fileRepository.CreateFile(message.UserId, uploadedFileId);
            
            outputPort.Handle(response.Success ? new LoginUserResponse(response.User, false, null) : new LoginUserResponse(new[] { new Error("login_failure", "Invalid credentials.") }));

            return response.Success;
        }

        private string UploadFile(FileUploadRequest message)
        { 
            StorageClient storage = StorageClient.Create();
            using (var f = message.File.OpenReadStream())
            {
                string objectName = message.UserId.ToString() + DateTime.Now.ToString(); 
                var response = storage.UploadObject(message.BucketName, objectName, null, f);
                return response.Id;
            }
        }

    }
    }
    }
}
