using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.QuoteRequest
{
    public class HouseQuoteRequestFetchAllResponse : UseCaseResponseMessage
    {
        public List<HouseQuoteRequest> HouseQuoteRequests { get; }

        public IEnumerable<Error> Errors { get; }

        public HouseQuoteRequestFetchAllResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public HouseQuoteRequestFetchAllResponse(List<HouseQuoteRequest> houseQuoteRequests, bool success = false, string message = null) : base(success, message)
        {
            HouseQuoteRequests = houseQuoteRequests;
        }
    }
}
