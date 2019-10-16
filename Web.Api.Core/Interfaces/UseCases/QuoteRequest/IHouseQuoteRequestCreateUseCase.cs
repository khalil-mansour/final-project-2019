using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.UseCases.QuoteRequest
{
    public interface IHouseQuoteRequestCreateUseCase : IUseCaseRequestHandler<HouseQuoteCreateRequest, HouseQuoteRequestCreateRepoResponse>
   {

    }
}
