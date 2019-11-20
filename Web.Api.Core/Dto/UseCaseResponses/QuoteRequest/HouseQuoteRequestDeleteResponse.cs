using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.QuoteRequest
{
    public class HouseQuoteRequestDeleteResponse : UseCaseResponseMessage
    {
        public HouseQuoteRequest HouseQuoteRequest { get; }

        public IEnumerable<Error> Errors { get; }

        public HouseQuoteRequestDeleteResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public HouseQuoteRequestDeleteResponse(HouseQuoteRequest houseQuoteRequest, bool success = false, string message = null) : base(success, message)
        {
            HouseQuoteRequest = houseQuoteRequest;
        }
    }
}
