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
using AutoFixture;

namespace Web.Api.Core.UnitTests.Offer
{
    public class OfferUpdateUseCaseUnitTest
    {
        private readonly int offerID = 12;

        [Fact]
        public async void Should_UpdateOffer_When_PUT()
        {
            // given

            Domain.Entities.Offer offer = new Fixture().Create<Domain.Entities.Offer>();

            var mockOfferRepository = new Mock<IOfferRepository>();
            mockOfferRepository
                .Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Domain.Entities.Offer>()))
                .Returns(Task.FromResult(new OfferUpdateRepoResponse(It.IsAny<Domain.Entities.Offer>(), true)));

            var useCase = new OfferUpdateUseCase(mockOfferRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<OfferUpdateResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<OfferUpdateResponse>()));

            // when

            var response = await useCase.Handle(
                new OfferUpdateRequest(
                    offer.UserId,
                    offer.QuoteRequestId,
                    offer.AnnualInterestRate,
                    offer.Loan,
                    offer.Mensuality,
                    offer.RateType,
                    offer.ContractDuration,
                    offer.LoanDuration,
                    offer.PaymentFrequency,
                    offer.Description,
                    offer.Submitted), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
