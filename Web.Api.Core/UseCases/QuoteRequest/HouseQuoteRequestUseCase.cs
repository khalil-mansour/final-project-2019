using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases.QuoteRequest
{
    public sealed class HouseQuoteRequestUseCase : IHouseQuoteRequestCreateUseCase
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        public HouseQuoteRequestUseCase(IQuoteRequestRepository quoteRequestReposiroty) {
            _quoteRequestRepository = quoteRequestReposiroty;
        }

        public Task<bool> Handle(HouseQuoteCreateRequest message, IOutputPort<HouseQuoteCreateResponse> outputPort)
        {
            // confirm user exists with ID
            var response = await _quoteRequestRepository.Create(
                new HouseQuoteRequest(message.HouseType, 
                new HouseLocation(message.HouseLocationRequest.PostalCode, message.HouseLocationRequest.CityId, message.HouseLocationRequest.ProvinceId, message.HouseLocationRequest.Street, message.HouseLocationRequest.AppartementUnits),
                message.ListingPrice, message.DownPayment, message.Offer, message.FirstHouse, message.Description, message.MunicipalEvaluationUrl)

            outputPort.Handle(response.Success ? new UserLoginResponse(response.User, true, null) : new UserLoginResponse(new[] { new Error("login_failure", "Invalid credentials.") }));
            return response.Success;
        }
    }
}
