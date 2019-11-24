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
    public sealed class OfferFetchAllByReqUseCase : IOfferFetchAllByReqUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOfferRepository _offerRepository;


        public OfferFetchAllByReqUseCase(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(OfferFetchAllByReqRequest message, Interfaces.IOutputPort<OfferFetchAllByReqResponse> outputPort)
        {
            var response = await _offerRepository.FetchAllByRequest(message.RequestId);

            outputPort.Handle(response.Success ? new OfferFetchAllByReqResponse(response.Offers, true) : new OfferFetchAllByReqResponse(new[] { new Error("Ation Failed", "Failed to fetch all offers for user.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
