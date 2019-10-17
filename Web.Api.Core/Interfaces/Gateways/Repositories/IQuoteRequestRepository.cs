using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IQuoteRequestRepository
    {
        Task<HouseQuoteRequestCreateRepoResponse> Create(HouseQuoteRequest houseQuoteRequest);

    }
}
