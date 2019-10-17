using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class QuoteRequestRepository : IQuoteRequestRepository
    {
        public async Task<HouseQuoteRequestCreateRepoResponse> Create(HouseQuoteRequest houseQuoteRequest)
        {
            throw new NotImplementedException();
        }
    }
}
