using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
    public class HouseQuoteRequestGetDetailRequest : IUseCaseRequest<HouseQuoteRequestGetDetailResponse>

    {
        public int QuoteRequestId { get; }

        public HouseQuoteRequestGetDetailRequest(int quoteRequestId)
        {
            QuoteRequestId = quoteRequestId;
        }
}
}
