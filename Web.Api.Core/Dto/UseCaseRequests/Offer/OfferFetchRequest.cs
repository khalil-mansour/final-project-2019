using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Offer
{
    public class OfferFetchRequest : IUseCaseRequest<OfferFetchResponse>
    {
        public int OfferID { get; }

        public OfferFetchRequest(int offerId)
        {
            OfferID = offerId;
        }
    }
}
