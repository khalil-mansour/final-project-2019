using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories
{
    public sealed class FinancialCapacityFindRepoResponse : BaseGatewayResponse
    {
        public FinancialCapacity FinancialCapacity { get; }
        public FinancialCapacityFindRepoResponse(FinancialCapacity financialCapacity = null, 
            bool success = false, 
            IEnumerable<Error> errors = null) : base(success, errors)
        {
            FinancialCapacity = financialCapacity;
        }
    }
}
