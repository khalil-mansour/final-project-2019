using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Offer
{
    public class OfferFetchAllByReqRequest : IUseCaseRequest<OfferFetchAllByReqResponse>
    {
        public int RequestId { get; }

        public OfferFetchAllByReqRequest(int requestId)
        {
            RequestId = requestId;
        }
    }
}
