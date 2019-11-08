using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;

namespace Web.Api.Core.Interfaces.UseCases
{
    public interface IHouseQuoteRequestCreateUseCase : IUseCaseRequestHandler<HouseQuoteCreateRequest, HouseQuoteRequestGetDeailtResponse>
    {

    }
}
