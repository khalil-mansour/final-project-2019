using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class FileFetchUseCase : IFileFetchUseCase
    {
        private readonly IFileRepository _fileRepository;

        public FileFetchUseCase(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<bool> Handle(FileFetchRequest message, IOutputPort<FileFetchResponse> outputPort)
        {
            var response = await _fileRepository.Fetch(message.UploadedFileId);

            outputPort.Handle(response.Success ? new FileFetchResponse(response.File, true) : new FileFetchResponse(response.Errors));
            return response.Success;
        }
    }
}
