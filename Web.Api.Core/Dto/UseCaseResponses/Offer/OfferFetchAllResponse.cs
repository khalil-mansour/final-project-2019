using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Offer
{
    public class OfferFetchAllResponse : UseCaseResponseMessage
    {
        public IEnumerable<Domain.Entities.Offer> Offers { get; }
        public IEnumerable<Error> Errors { get; }

        public OfferFetchAllResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public OfferFetchAllResponse(IEnumerable<Domain.Entities.Offer> offers, bool success = false, string message = null) : base(success, message)
        {
            Offers = offers;
        }
    }
}
