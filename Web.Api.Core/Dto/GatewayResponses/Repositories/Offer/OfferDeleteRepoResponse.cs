

using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.Offer
{
    public sealed class OfferDeleteRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.Offer Offer { get; }

        public OfferDeleteRepoResponse(Domain.Entities.Offer offer = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Offer = offer;
        }
    }
}
