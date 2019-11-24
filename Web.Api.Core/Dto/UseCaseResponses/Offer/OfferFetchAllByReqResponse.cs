using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Offer
{
    public class OfferFetchAllByReqResponse : UseCaseResponseMessage
    {
        public IEnumerable<Domain.Entities.Offer> Offers { get; }
        public IEnumerable<Error> Errors { get; }

        public OfferFetchAllByReqResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public OfferFetchAllByReqResponse(IEnumerable<Domain.Entities.Offer> offers, bool success = false, string message = null) : base(success, message)
        {
            Offers = offers;
        }
    }
}
