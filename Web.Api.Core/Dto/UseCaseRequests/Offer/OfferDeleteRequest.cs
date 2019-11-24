using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Offer
{
    public class OfferDeleteRequest : IUseCaseRequest<OfferDeleteResponse>
    {
        public int OfferId { get; }

        public OfferDeleteRequest(int offerId)
        {
            OfferId = offerId;
        }
    }
}
