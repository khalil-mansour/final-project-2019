using Xunit;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Web.Api.Core.Dto.GatewayResponses.Repositories.Offer;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases.Offer;
using System.Collections.Generic;
using Web.Api.Core.Dto.UseCaseRequests.Offer;

namespace Web.Api.Core.UnitTests.Offer
{
    public class OfferFetchAllUseCaseUnitTest
    {
        private readonly string userId = "coco";

        [Fact]
        public async void Should_FetchAllOffers_When_GET()
        {
            // given

            var mockOfferRepository = new Mock<IOfferRepository>();

            mockOfferRepository
                .Setup(repo => repo.FetchAllByUser(It.IsAny<string>()))
                    .Returns(Task.FromResult(new OfferFetchAllRepoResponse(It.IsAny<IEnumerable<Domain.Entities.Offer>>(), true)));

            var useCase = new OfferFetchAllUseCase(mockOfferRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<OfferFetchAllResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<OfferFetchAllResponse>()));

            // when

            var response = await useCase.Handle(
                new OfferFetchAllRequest(userId), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
