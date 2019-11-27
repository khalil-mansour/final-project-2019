using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class UserRegisterRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.User User { get; }

        public UserRegisterRepoResponse(Domain.Entities.User user = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            User = user;
        }
    }
}