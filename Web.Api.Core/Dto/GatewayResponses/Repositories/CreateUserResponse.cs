﻿using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class CreateUserResponse : BaseGatewayResponse
    {
        public User User { get; }
        public CreateUserResponse(User user = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            User = user;
        }
    }
}