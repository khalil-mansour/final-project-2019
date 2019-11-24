using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.Offer
{
    public sealed class OfferUpdateRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.Offer Offer { get; }

        public OfferUpdateRepoResponse(Domain.Entities.Offer offer = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Offer = offer;
        }
    }
}
