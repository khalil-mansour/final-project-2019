using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class FileFetchAllUseCase : IFileFetchAllUseCase
    {
        private readonly IFileRepository _fileRepository;

        public FileFetchAllUseCase(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(FileFetchAllRequest message, IOutputPort<FileFetchAllResponse> outputPort)
        {
            var response = await _fileRepository.FetchAll(message.UserId);

            outputPort.Handle(response.Success ? new FileFetchAllResponse(response.Files, true) : new FileFetchAllResponse(response.Errors));
            return response.Success;
        }
    }
}
