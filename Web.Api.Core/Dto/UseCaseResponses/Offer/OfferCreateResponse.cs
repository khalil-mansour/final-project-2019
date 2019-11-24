using System.Collections.Generic;

using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Offer
{
    public class OfferCreateResponse : UseCaseResponseMessage
    {
        public Domain.Entities.Offer Offer { get; }
        public IEnumerable<Error> Errors { get; }

        public OfferCreateResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public OfferCreateResponse(Domain.Entities.Offer offer, bool success = false, string message = null) : base(success, message)
        {
            Offer = offer;
        }
    }
}
