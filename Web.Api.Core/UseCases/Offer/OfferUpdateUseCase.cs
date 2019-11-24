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
    public sealed class OfferUpdateUseCase : IOfferUpdateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IOfferRepository _offerRepository;

        public OfferUpdateUseCase(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(OfferUpdateRequest message, IOutputPort<OfferUpdateResponse> outputPort)
        {
            var response = await _offerRepository.Update(
                message.Id,
                (new Domain.Entities.Offer(
                    message.UserId,
                    message.QuoteRequestId,
                    message.AnnualInterestRate,
                    message.Loan,
                    message.Mensuality,
                    message.RateType,
                    message.ContractDuration,
                    message.LoanDuration,
                    message.PaymentFrequency,
                    message.Description,
                    message.Submitted)));

            outputPort.Handle(response.Success ? new OfferUpdateResponse(response.Offer, true) : new OfferUpdateResponse(new[] { new Error("Ation Failed", "Failed to update offer.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
