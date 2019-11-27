using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.File;
using Web.Api.Core.Dto.UseCaseResponses.File;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.File;

namespace Web.Api.Core.UseCases.File
{
    public sealed class FileDeleteUseCase : IFileDeleteUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;

        public FileDeleteUseCase(IConfiguration configuration, IFileRepository fileRepository)
        {
            _configuration = configuration;
            _fileRepository = fileRepository;
        }


        public async Task<bool> HandleAsync(FileDeleteRequest message, IOutputPort<FileDeleteResponse> outputPort)
        {
            var response = await _fileRepository.Delete(message.DocumentId);

            if (response.Success)
            {
                try
                {
                    RemoveFromGCloud(response.File.StorageId);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Error trying to remove document from GCloud.");
                    outputPort.Handle(new FileDeleteResponse(new[] { new Error(e.HResult.ToString(), "Document deleted from database, but error trying to remove document from GCloud.") }));
                    return true;
                }
            }

            outputPort.Handle(response.Success ? new FileDeleteResponse(response.File, true) : new FileDeleteResponse(new[] { new Error("Action failed", "Error attempting to delete document from database.") }));
            

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }

        private void RemoveFromGCloud(string storageId)
        {
            var storage = StorageClient.Create();
            storage.DeleteObject(_configuration.GetSection("BucketName").Value, storageId);
        }
    }
}
