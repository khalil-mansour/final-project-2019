using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class UserLoginRequest : IUseCaseRequest<UserLoginResponse>
    {
        public string ID { get; }

        public UserLoginRequest(string id)
        {
            ID = id;
        }
    }
}
