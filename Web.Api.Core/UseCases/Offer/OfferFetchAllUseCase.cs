using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.Offer;

namespace Web.Api.Core.UseCases.Offer
{
    public sealed class OfferFetchAllUseCase : IOfferFetchAllUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOfferRepository _offerRepository;


        public OfferFetchAllUseCase(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> HandleAsync(OfferFetchAllRequest message, Interfaces.IOutputPort<OfferFetchAllResponse> outputPort)
        {
            var response = await _offerRepository.FetchAllByUser(message.UserId);

            outputPort.Handle(response.Success ? new OfferFetchAllResponse(response.Offers, true) : new OfferFetchAllResponse(new[] { new Error("Ation Failed", "Failed to fetch all offers for user.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
