using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class LoginUserRequest : IUseCaseRequest<LoginUserResponse>
    {
        public string ID { get; }

        public LoginUserRequest(string id)
        {
            ID = id;
        }
    }
}
