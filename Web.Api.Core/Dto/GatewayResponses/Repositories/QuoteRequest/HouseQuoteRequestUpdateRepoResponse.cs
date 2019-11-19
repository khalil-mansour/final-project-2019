using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest
{
    public class HouseQuoteRequestUpdateRepoResponse : BaseGatewayResponse
    {
        public HouseQuoteRequest HouseQuoteRequest { get; }

        public HouseQuoteRequestUpdateRepoResponse(HouseQuoteRequest houseQuoteRequest = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            HouseQuoteRequest = houseQuoteRequest;
        }
    }
}
