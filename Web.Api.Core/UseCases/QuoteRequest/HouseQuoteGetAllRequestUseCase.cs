using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteGetAllRequestUseCase: IHouseQuoteRequestGetQuotesRequestUseCase
    {

        private readonly IQuoteRequestRepository _quoteRequestRepository;
        public HouseQuoteGetAllRequestUseCase(IQuoteRequestRepository quoteRequestReposiroty) {
            _quoteRequestRepository = quoteRequestReposiroty;
        }

        public async Task<bool> Handle(HouseQuoteRequestGetAllRequest message, IOutputPort<HouseQuoteGetAllRequestResponse> outputPort)
        {
            var response = await _quoteRequestRepository.GetAllQuoteForUser(message.UserId);
            outputPort.Handle(response.Success ? new HouseQuoteGetAllRequestResponse(response.HouseQuoteRequests, true, null) : new HouseQuoteGetAllRequestResponse(new[] { new Error("Action Failed", "Enable to fetch house quote request") }));
            return response.Success;
        }
    }
}
