using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;

namespace Web.Api.Core.Interfaces.UseCases.QuoteRequest
{
    public interface IHouseQuoteRequestCreateUseCase : IUseCaseRequestHandler<HouseQuoteCreateRequest, HouseQuoteCreateResponse>
    {

    }
}
