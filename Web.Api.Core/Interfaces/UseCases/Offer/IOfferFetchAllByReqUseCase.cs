using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;

namespace Web.Api.Core.Interfaces.UseCases.Offer
{
    public interface IOfferFetchAllByReqUseCase : IUseCaseRequestHandler<OfferFetchAllByReqRequest, OfferFetchAllByReqResponse>
    {
    }
}
