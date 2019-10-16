using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest
{
    public sealed class HouseQuoteRequestCreateRepoResponse : BaseGatewayResponse
    {
        public HouseQuoteRequest HouseQuoteRequest { get; }

        public HouseQuoteRequestCreateRepoResponse(HouseQuoteRequest houseQuoteRequest = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
           HouseQuoteRequest = houseQuoteRequest;
        }

    }
}
