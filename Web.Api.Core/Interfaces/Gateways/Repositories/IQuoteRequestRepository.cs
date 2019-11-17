using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IQuoteRequestRepository
    {
        Task<HouseQuoteRequestCreateRepoResponse> Create(HouseQuoteRequest houseQuoteRequest);
        Task<HouseQuoteRequestGetAllRepoResponse> GetAllQuoteForUser(string userId);
        Task<HouseQuoteRequestGetAllRepoResponse> GetAllQuotes();
        Task<HouseQuoteRequestGetDetailResponse> GetDetailFor(int quoteRequestId);
    }
}
