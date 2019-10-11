﻿using Google.Cloud.Storage.V1;
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
            string uploadedFileName;
            try
            {
                uploadedFileName = UploadFile(message);
            }
            catch (Exception e)
            {
                outputPort.Handle(new FileUploadResponse(new Error(e.HResult.ToString(), "Failed to upload file to the Google Cloud service.")));
                return false;
            }


            var response = await _fileRepository.
                Create(new File(
                    message.UserId,
                    message.DocTypeId,
                    message.File.FileName,
                    uploadedFileName,
                    DateTime.Now,
                    message.Visible
                    ));

            outputPort.Handle(response.Success ? new FileUploadResponse(uploadedFileName, true) : new FileUploadResponse(response.Error));
            return response.Success;
        }

        private string UploadFile(FileUploadRequest message)
        {
            StorageClient storage = StorageClient.Create();
            using (var f = message.File.OpenReadStream())
            {
                var bucket = _configuration.GetSection("BucketName").Value;
                string objectName = $"{message.UserId}{message.DocTypeId}{DateTime.Now.ToString("MM/dd/yyyy/ HH:mm:ss")}";
                var response = storage.UploadObject(bucket, objectName, message.File.ContentType, f);
                return response.Name;
            }
        }
    }
}
