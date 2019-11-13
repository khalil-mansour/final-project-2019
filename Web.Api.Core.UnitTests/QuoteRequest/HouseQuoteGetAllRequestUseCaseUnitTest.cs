using AutoFixture;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases.QuoteRequest;
using Xunit;

namespace Web.Api.Core.UnitTests.QuoteRequest
{

    public class HouseQuoteGetAllRequestUseCaseUnitTest
    {
        // mocked data
        private readonly string userID = "5t5t";


        [Fact]
        public async void Should_FetchAll_HouseQuoteRequest_When_Receiving_UserId()
        {
            // given
            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();

            var mockQuoteRepository = new Mock<IQuoteRequestRepository>();
            mockQuoteRepository
                .Setup(repo => repo.GetAllQuoteForUser(It.IsAny<string>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestGetAllRepoResponse(new List<HouseQuoteRequest> { houseQuoteRequest }, true)));

            var useCase = new HouseQuoteGetAllRequestUseCase(mockQuoteRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<HouseQuoteGetAllRequestResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<HouseQuoteGetAllRequestResponse>()));

            // when

            var response = await useCase.Handle(
                new HouseQuoteRequestGetAllRequest(userID), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
