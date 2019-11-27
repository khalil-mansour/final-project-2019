using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses.User;

namespace Web.Api.Core.Interfaces.UseCases
{
    public interface IUserUpdateUseCase : IUseCaseRequestHandler<UserUpdateRequest, UserUpdateResponse>
    {
    }
}
