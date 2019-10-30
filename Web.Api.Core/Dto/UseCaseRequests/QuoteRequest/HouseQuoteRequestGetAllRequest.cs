using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
    public class HouseQuoteRequestGetAllRequest : IUseCaseRequest<HouseQuoteGetAllRequestResponse>
    {
        public string UserId { get; }

        public HouseQuoteRequestGetAllRequest(string userId)
        {
            UserId = userId;
        }

    }
}
