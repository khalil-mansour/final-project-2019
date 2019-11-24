using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.Offer;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IOfferRepository
    {
        Task<OfferCreateRepoResponse> Create(Offer offer);
        Task<OfferFetchRepoResponse> Fetch(int offerId);
        Task<OfferFetchAllRepoResponse> FetchAllByUser(string userId);
        Task<OfferDeleteRepoResponse> Delete(int offerId);
        Task<OfferFetchAllByReqRepoResponse> FetchAllByRequest(int requestId);
        Task<OfferUpdateRepoResponse> Update(int offerId, Offer offer);
    }
}
