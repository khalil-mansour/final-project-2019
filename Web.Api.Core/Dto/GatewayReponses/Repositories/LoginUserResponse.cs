using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses;

namespace Web.Api.Core.Dto.GatewayReponses.Repositories
{
    public sealed class LoginUserResponse : BaseGatewayResponse
    {
        public User User { get; }
        public LoginUserResponse(User user = null, bool success = false, Error error = null) : base(success, error)
        {
            User = user;
        }
    }
}
