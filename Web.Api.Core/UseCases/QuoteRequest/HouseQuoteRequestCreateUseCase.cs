﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestCreateUseCase : IHouseQuoteRequestCreateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IQuoteRequestRepository _quoteRequestRepository;

        public HouseQuoteRequestCreateUseCase(IQuoteRequestRepository quoteRequestReposiroty)
        {
            _quoteRequestRepository = quoteRequestReposiroty;
        }

        public async Task<bool> Handle(HouseQuoteRequestCreateRequest message, IOutputPort<HouseQuoteRequestCreateResponse> outputPort)
        {
            var response = await _quoteRequestRepository.Create(
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

            outputPort.Handle(response.Success ? new HouseQuoteRequestCreateResponse(response.HouseQuoteRequest, true, null) : new HouseQuoteRequestCreateResponse(new[] { new Error("Action Failed", "Enable to create house quote request") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
