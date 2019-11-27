using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.Offer;

namespace Web.Api.Core.UseCases.Offer
{
    public sealed class OfferCreateUseCase : IOfferCreateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOfferRepository _offerRepository;

        public OfferCreateUseCase(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> HandleAsync(OfferCreateRequest message, IOutputPort<OfferCreateResponse> outputPort)
        {
            var response = await _offerRepository.
                Create(new Domain.Entities.Offer(
                    message.UserId,
                    message.QuoteRequestId,
                    message.Submitted));

            outputPort.Handle(response.Success ? new OfferCreateResponse(response.Offer, true) : new OfferCreateResponse(new[] { new Error("Ation Failed", "Failed to insert a new offer.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
