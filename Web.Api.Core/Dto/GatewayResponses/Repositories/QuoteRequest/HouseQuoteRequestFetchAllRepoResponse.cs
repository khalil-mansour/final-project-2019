using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest
{
    public sealed class HouseQuoteRequestFetchAllRepoResponse: BaseGatewayResponse
    {
        public List<HouseQuoteRequest> HouseQuoteRequests { get; }

        public HouseQuoteRequestFetchAllRepoResponse(List<HouseQuoteRequest> houseQuoteRequests = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
           HouseQuoteRequests = houseQuoteRequests;
        }
    }
}
