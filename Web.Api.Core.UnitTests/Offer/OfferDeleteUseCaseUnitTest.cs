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
    public class OfferDeleteUseCaseUnitTest
    {
        // mocked data
        private readonly int offerId = 33;

        [Fact]
        public async void Should_DeleteOffer_When_DELETE()
        {
            // given

            var mockOfferRepository = new Mock<IOfferRepository>();

            mockOfferRepository
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                .Returns(Task.FromResult(new OfferDeleteRepoResponse(It.IsAny<Domain.Entities.Offer>(), true)));

            var useCase = new OfferDeleteUseCase(mockOfferRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<OfferDeleteResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<OfferDeleteResponse>()));

            // when

            var response = await useCase.HandleAsync(
                new OfferDeleteRequest(offerId), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
