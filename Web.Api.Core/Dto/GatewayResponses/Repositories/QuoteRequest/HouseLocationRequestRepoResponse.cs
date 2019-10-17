using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest
{
    public sealed class HouseLocationRequestRepoResponse : BaseGatewayResponse
    {

        public HouseLocation HouseLocation { get; }

        public HouseLocationRequestRepoResponse(HouseLocation houseLocation = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            HouseLocation = houseLocation;
        }

    }
}
