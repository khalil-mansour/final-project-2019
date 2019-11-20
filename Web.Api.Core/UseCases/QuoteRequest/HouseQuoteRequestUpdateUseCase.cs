using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestUpdateUseCase : IHouseQuoteRequestUpdateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IQuoteRequestRepository _quoteRequestRepository;

        public HouseQuoteRequestUpdateUseCase(IQuoteRequestRepository quoteRequestRepository)
        {
            _quoteRequestRepository = quoteRequestRepository;
        }

        public async Task<bool> Handle(HouseQuoteRequestUpdateRequest message, IOutputPort<HouseQuoteRequestUpdateResponse> outputPort)
        {
            var response = await _quoteRequestRepository.Update(
                message.QuoteRequestId,
                new HouseQuoteRequest(message.UserId, message.HouseType,
                new HouseLocation(message.HouseLocationRequest.PostalCode,
                message.HouseLocationRequest.City,
                message.HouseLocationRequest.ProvinceId,
                message.HouseLocationRequest.Address,
                message.HouseLocationRequest.ApartmentUnit),
                message.ListingPrice,
                DateTime.Now,
                message.DownPayment,
                message.Offer,
                message.FirstHouse,
                message.Description,
                message.DocumentsId,
                message.MunicipalEvaluationUrl));

            outputPort.Handle(response.Success ? new HouseQuoteRequestUpdateResponse(response.HouseQuoteRequest, true, null) : new HouseQuoteRequestUpdateResponse(new[] { new Error("Update Failed", "Unable to update house quote request") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
