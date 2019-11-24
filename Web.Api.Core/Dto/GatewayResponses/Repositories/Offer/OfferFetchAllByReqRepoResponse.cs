using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.Offer
{
    public sealed class OfferFetchAllByReqRepoResponse : BaseGatewayResponse
    {
        public IEnumerable<Domain.Entities.Offer> Offers { get; }

        public OfferFetchAllByReqRepoResponse(IEnumerable<Domain.Entities.Offer> offers = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Offers = offers;
        }
    }
}
