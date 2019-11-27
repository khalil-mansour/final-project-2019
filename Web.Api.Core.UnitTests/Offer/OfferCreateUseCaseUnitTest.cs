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
    public class OfferCreateUseCaseUnitTest
    {
        // mocked data
        private readonly string userId = "coco";
        private readonly int quoteRequestID = 2;
        private readonly bool status = false;

        [Fact]
        public async void Should_CreateOffer_When_POST()
        {
            // given

            var mockOfferRepository = new Mock<IOfferRepository>();

            mockOfferRepository
                .Setup(repo => repo.Create(It.IsAny<Domain.Entities.Offer>()))
                .Returns(Task.FromResult(new OfferCreateRepoResponse(It.IsAny<Domain.Entities.Offer>(), true)));

            var useCase = new OfferCreateUseCase(mockOfferRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<OfferCreateResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<OfferCreateResponse>()));

            // when

            var response = await useCase.HandleAsync(
                new OfferCreateRequest(userId, quoteRequestID, status), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
