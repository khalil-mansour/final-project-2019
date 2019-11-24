using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Offer
{
    public class OfferCreateRequest : IUseCaseRequest<OfferCreateResponse>
    {
        public string UserId { get; }

        public int QuoteRequestId { get; }

        public bool Submitted { get; }

        public OfferCreateRequest(string userId, int quoteRequestId, bool submitted)
        {
            UserId = userId;
            QuoteRequestId = quoteRequestId;
            Submitted = submitted;
        }
    }
}
