using AutoFixture;
using FluentAssertions;
using Moq;
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
    public class HouseQuoteRequestCreateUseCaseUnitTest
    {
        // mocked data


        [Fact]
        public async void Should_Create_HouseQuoteRequest_When_Receiving_HouseQuote()
        {
            // given
            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();
            HouseQuoteRequestCreateRequest houseQuoteCreate = new Fixture().Create<HouseQuoteRequestCreateRequest>();

            var mockQuoteRepository = new Mock<IQuoteRequestRepository>();
            mockQuoteRepository
                .Setup(repo => repo.Create(It.IsAny<HouseQuoteRequest>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestCreateRepoResponse(houseQuoteRequest, true)));

            var useCase = new HouseQuoteRequestCreateUseCase(mockQuoteRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<HouseQuoteRequestCreateResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<HouseQuoteRequestCreateResponse>()));

            // when

            var response = await useCase.Handle(
               houseQuoteCreate , mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
