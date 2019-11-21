using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IQuoteRequestRepository
    {
        Task<HouseQuoteRequestCreateRepoResponse> Create(HouseQuoteRequest houseQuoteRequest);
        Task<HouseQuoteRequestFetchAllRepoResponse> GetAllQuoteRequestsForUser(string userId);
        Task<HouseQuoteRequestFetchAllRepoResponse> GetAllQuotes();
        Task<HouseQuoteRequestGetDetailResponse> GetDetailFor(int quoteRequestId);
        Task<HouseQuoteRequestDeleteRepoResponse> Delete(int quoteRequestId);
        Task<HouseQuoteRequestUpdateRepoResponse> Update(int quoteRequestId, HouseQuoteRequest houseQuoteRequest);
    }
}
