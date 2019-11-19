using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
    public class HouseQuoteRequestFetchAllRequest : IUseCaseRequest<HouseQuoteRequestFetchAllResponse>
    {
        public string UserId { get; }

        public HouseQuoteRequestFetchAllRequest(string userId)
        {
            UserId = userId;
        }

    }
}
