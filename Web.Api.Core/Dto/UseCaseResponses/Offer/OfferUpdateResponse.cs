using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Offer
{
    public class OfferUpdateResponse : UseCaseResponseMessage
    {
        public Domain.Entities.Offer Offer { get; }
        public IEnumerable<Error> Errors { get; }

        public OfferUpdateResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public OfferUpdateResponse(Domain.Entities.Offer offer, bool success = false, string message = null) : base(success, message)
        {
            Offer = offer;
        }
    }
}
