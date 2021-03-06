using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.User
{
    public sealed class UserProfileUpdateRepoResponse : BaseGatewayResponse
    {
        public Profile Profile { get; }

        public UserProfileUpdateRepoResponse(Profile profile = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Profile = profile;
        }
    }
}
