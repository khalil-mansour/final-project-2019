using AutoFixture;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases.QuoteRequest;
using Xunit;

namespace Web.Api.Core.UnitTests.QuoteRequest
{
    public class HouseQuoteRequestGetDetailUseCaseUnitTest
    {

        [Fact]
        public async void Should_GetDetail_HouseQuoteRequest_When_Receiving_HouseQuoteId()
        {
            // given
            HouseQuoteRequestGetDetailRequest houseQuoteRequestDetail = new Fixture().Create<HouseQuoteRequestGetDetailRequest>();
            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();
            Domain.Entities.File file = new Fixture().Create<Domain.Entities.File>();

            var mockQuoteRepository = new Mock<IQuoteRequestRepository>();
            var mockFileRepository = new Mock<IFileRepository>();

            mockQuoteRepository
                .Setup(repo => repo.GetDetailFor(It.IsAny<int>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestGetDetailResponse(houseQuoteRequest, true)));

            mockFileRepository
                .Setup(repo => repo.GetFile(It.IsAny<int>()))
                .Returns(file);

            var useCase = new HouseQuoteRequestGetDetailUseCase(mockQuoteRepository.Object, mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<HouseQuoteRequestGetDetailResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<HouseQuoteRequestGetDetailResponse>()));

            // when

            var response = await useCase.Handle(
               houseQuoteRequestDetail, mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}

