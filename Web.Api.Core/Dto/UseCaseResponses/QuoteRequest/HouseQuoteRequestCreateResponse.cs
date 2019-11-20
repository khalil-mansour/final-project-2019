using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.QuoteRequest
{
    public class HouseQuoteRequestCreateResponse : UseCaseResponseMessage
    {

        public HouseQuoteRequest HouseQuoteRequest { get; }

        public IEnumerable<Error> Errors { get; }

        public HouseQuoteRequestCreateResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public HouseQuoteRequestCreateResponse(HouseQuoteRequest houseQuoteRequest, bool success = false, string message = null) : base(success, message)
        {
            HouseQuoteRequest = houseQuoteRequest;
        }
    }
}
