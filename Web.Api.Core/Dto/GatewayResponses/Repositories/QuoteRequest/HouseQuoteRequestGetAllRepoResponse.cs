using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest
{
    public sealed class HouseQuoteRequestGetAllRepoResponse: BaseGatewayResponse
    {

        public List<HouseQuoteRequest> HouseQuoteRequests { get; }

        public HouseQuoteRequestGetAllRepoResponse(List<HouseQuoteRequest> houseQuoteRequests = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
           HouseQuoteRequests = houseQuoteRequests;
        }
    }
}
