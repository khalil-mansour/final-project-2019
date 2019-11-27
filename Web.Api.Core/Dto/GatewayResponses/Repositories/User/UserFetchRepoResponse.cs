using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class UserFetchRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.User User { get; }

        public UserFetchRepoResponse(Domain.Entities.User user = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            User = user;
        }
    }
}