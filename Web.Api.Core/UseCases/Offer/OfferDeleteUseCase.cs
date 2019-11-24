using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.Offer;

namespace Web.Api.Core.UseCases.Offer
{
    public sealed class OfferDeleteUseCase : IOfferDeleteUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOfferRepository _offerRepository;


        public OfferDeleteUseCase(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(OfferDeleteRequest message, Interfaces.IOutputPort<OfferDeleteResponse> outputPort)
        {
            var response = await _offerRepository.Delete(message.OfferId);

            outputPort.Handle(response.Success ? new OfferDeleteResponse(response.Offer, true) : new OfferDeleteResponse(new[] { new Error("Ation Failed", "Failed to delete offer.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
