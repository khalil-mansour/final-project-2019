using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.Offer;

namespace Web.Api.Core.UseCases.Offer
{
    public sealed class OfferFetchUseCase : IOfferFetchUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOfferRepository _offerRepository;


        public OfferFetchUseCase(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(OfferFetchRequest message, Interfaces.IOutputPort<OfferFetchResponse> outputPort)
        {
            var response = await _offerRepository.Fetch(message.OfferID);

            outputPort.Handle(response.Success ? new OfferFetchResponse(response.Offer, true) : new OfferFetchResponse(new[] { new Error("Ation Failed", "No offer found with matching ID.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
