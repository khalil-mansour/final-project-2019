using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
    public class HouseQuoteRequestDeleteRequest : IUseCaseRequest<HouseQuoteRequestDeleteResponse>
    {
        public int HouseQuoteRequestId { get; }

        public HouseQuoteRequestDeleteRequest(int houseQuoteRequestId)
        {
            HouseQuoteRequestId = houseQuoteRequestId;
        }
    }
}
