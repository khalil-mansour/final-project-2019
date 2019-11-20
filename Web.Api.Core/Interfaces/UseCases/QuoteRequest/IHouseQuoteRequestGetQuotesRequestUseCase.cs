using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;

namespace Web.Api.Core.Interfaces.UseCases.QuoteRequest
{
    public interface IHouseQuoteRequestFetchAllUseCase : IUseCaseRequestHandler<HouseQuoteRequestFetchAllRequest, HouseQuoteRequestFetchAllResponse>
    {
    }
}
