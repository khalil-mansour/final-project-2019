using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestDeleteUseCase : IHouseQuoteRequestDeleteUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IQuoteRequestRepository _quoteRequestRepository;

        public HouseQuoteRequestDeleteUseCase(IQuoteRequestRepository quoteRequestRepository)
        {
            _quoteRequestRepository = quoteRequestRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestDeleteRequest message, IOutputPort<HouseQuoteRequestDeleteResponse> outputPort)
        {
            var response = await _quoteRequestRepository.Delete(message.HouseQuoteRequestId);

            outputPort.Handle(response.Success ?
                new HouseQuoteRequestDeleteResponse(response.HouseQuoteRequest, true)
                :
                new HouseQuoteRequestDeleteResponse(new[] { new Error("Delete Failed", "Failed to delete the quote request.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;

        }
    }
}
