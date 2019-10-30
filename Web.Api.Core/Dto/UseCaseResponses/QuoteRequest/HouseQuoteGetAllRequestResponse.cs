using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.QuoteRequest
{
    public class HouseQuoteGetAllRequestResponse : UseCaseResponseMessage
    {
        public List<HouseQuoteRequest> HouseQuoteRequests { get; }

        public IEnumerable<Error> Errors { get; }

        public HouseQuoteGetAllRequestResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public HouseQuoteGetAllRequestResponse(List<HouseQuoteRequest> houseQuoteRequests, bool success = false, string message = null) : base(success, message)
        {
            HouseQuoteRequests = houseQuoteRequests;
        }
    }
}
