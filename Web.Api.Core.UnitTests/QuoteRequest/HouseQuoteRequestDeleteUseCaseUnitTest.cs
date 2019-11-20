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
    public class HouseQuoteRequestDeleteUseCaseUnitTest
    {
        [Fact]
        public async void Should_Update_HouseQuoteRequest_When_PUT()
        {
            // given

            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();
            HouseQuoteRequestDeleteRequest houseQuoteDelete = new Fixture().Create<HouseQuoteRequestDeleteRequest>();

            var mockQuoteRepository = new Mock<IQuoteRequestRepository>();
            mockQuoteRepository
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestDeleteRepoResponse(houseQuoteRequest, true)));

            var useCase = new HouseQuoteRequestDeleteUseCase(mockQuoteRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<HouseQuoteRequestDeleteResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<HouseQuoteRequestDeleteResponse>()));

            // when

            var response = await useCase.Handle(
                houseQuoteDelete, mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
