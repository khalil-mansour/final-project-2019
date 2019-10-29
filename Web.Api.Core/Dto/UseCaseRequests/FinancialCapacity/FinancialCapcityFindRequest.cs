using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FinancialCapacityFindRequest : IUseCaseRequest<FinancialCapacityFindResponse>
    {
        public string ID { get; }

        public FinancialCapacityFindRequest(string id)
        {
            ID = id;
        }
    }
}