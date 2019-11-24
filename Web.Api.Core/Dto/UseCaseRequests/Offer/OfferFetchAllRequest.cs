using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Offer
{
    public class OfferFetchAllRequest : IUseCaseRequest<OfferFetchAllResponse>
    {
        public string UserId { get; }

        public OfferFetchAllRequest(string userId)
        {
            UserId = userId;
        }
    }
}
