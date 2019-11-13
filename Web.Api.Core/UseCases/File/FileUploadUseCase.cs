using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class FileUploadUseCase : IFileUploadUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

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
            string signedUrl;
            try
            {
                uploadedFileName = UploadFile(message);
                signedUrl = SignUrl(uploadedFileName);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error uploading to GCLOUD.");
                outputPort.Handle(new FileUploadResponse(new Error(e.HResult.ToString(), "Failed to upload file to the Google Cloud service.")));
                return true;
            }

            var response = await _fileRepository.
                Create(new Domain.Entities.File(
                    message.UserId,
                    message.DocTypeId,
                    message.File.FileName,
                    uploadedFileName,
                    DateTime.Now,
                    message.Visible
                    ));

            if (response.File != null)
                response.File.Url = signedUrl;

            outputPort.Handle(response.Success ? new FileUploadResponse(response.File, true) : new FileUploadResponse(new Error("Action Failed", "Error attempting to upload file.")));

            // logging
            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

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

        private string SignUrl(string storageId)
        {
            UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            return urlSigner.Sign(_configuration.GetSection("BucketName").Value, storageId, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}
