using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
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
        [Fact]
        public async void Should_Create_HouseQuoteRequest_When_Receiving_HouseQuote()
        {
            // given

            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();
            HouseQuoteRequestCreateRequest houseQuoteCreate = new Fixture().Create<HouseQuoteRequestCreateRequest>();
            Domain.Entities.File file = new Fixture().Create<Domain.Entities.File>();

            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigSection = new Mock<IConfigurationSection>();

            mockConfigSection
                .Setup(v => v.Value)
                .Returns("app_document_bucket");

            mockConfiguration
                .Setup(k => k.GetSection("BucketName"))
                .Returns(mockConfigSection.Object);

            var mockQuoteRepository = new Mock<IQuoteRequestRepository>();
            mockQuoteRepository
                .Setup(repo => repo.Create(It.IsAny<HouseQuoteRequest>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestCreateRepoResponse(houseQuoteRequest, true)));

            var mockFileRepository = new Mock<IFileRepository>();
            mockFileRepository
                .Setup(repo => repo.GetFile(It.IsAny<int>()))
                .Returns(file);

            var useCase = new HouseQuoteRequestCreateUseCase(mockConfiguration.Object, mockQuoteRepository.Object, mockFileRepository.Object);

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
