using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class UserUpdateRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.User User { get; }

        public UserUpdateRepoResponse(Domain.Entities.User user = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            User = user;
        }        
    }
}