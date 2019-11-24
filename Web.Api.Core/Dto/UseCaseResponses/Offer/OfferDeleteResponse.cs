using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Offer
{
    public class OfferDeleteResponse : UseCaseResponseMessage
    {
        public Domain.Entities.Offer Offer { get; }

        public IEnumerable<Error> Errors { get; }

        public OfferDeleteResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public OfferDeleteResponse(Domain.Entities.Offer offer, bool success = false, string message = null) : base(success, message)
        {
            Offer = offer;
        }
    }
}
