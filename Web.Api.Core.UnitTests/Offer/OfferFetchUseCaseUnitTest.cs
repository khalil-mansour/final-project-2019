using Xunit;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Web.Api.Core.Dto.GatewayResponses.Repositories.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases.Offer;
using Web.Api.Core.Dto.UseCaseRequests.Offer;

namespace Web.Api.Core.UnitTests.Offer
{
    public class OfferFetchUseCaseUnitTest
    {
        // mocked data
        private readonly int offerId = 33;
        
        [Fact]
        public async void Should_FetchOffer_When_GET()
        {
            // given

            var mockOfferRepository = new Mock<IOfferRepository>();

            mockOfferRepository
                .Setup(repo => repo.Fetch(It.IsAny<int>()))
                .Returns(Task.FromResult(new OfferFetchRepoResponse(It.IsAny<Domain.Entities.Offer>(), true)));

            var useCase = new OfferFetchUseCase(mockOfferRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<OfferFetchResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<OfferFetchResponse>()));

            // when

            var response = await useCase.Handle(
                new OfferFetchRequest(offerId), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
