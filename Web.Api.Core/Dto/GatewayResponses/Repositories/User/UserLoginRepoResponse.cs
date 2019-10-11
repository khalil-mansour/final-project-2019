using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class UserLoginRepoResponse : BaseGatewayResponse
    {
        public User User { get; }
        public UserLoginRepoResponse(User user = null, bool success = false, Error error = null) : base(success, error)
        {
            User = user;
        }
    }
}
