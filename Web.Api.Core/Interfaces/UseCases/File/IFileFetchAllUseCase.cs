using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;

namespace Web.Api.Core.Interfaces.UseCases.File
{
    public interface IFileFetchAllUseCase : IUseCaseRequestHandler<FileFetchAllRequest, FileFetchAllResponse>
    {
    }
}
