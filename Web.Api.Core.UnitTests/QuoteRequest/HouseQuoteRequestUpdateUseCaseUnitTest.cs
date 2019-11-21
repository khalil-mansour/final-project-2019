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
    public class HouseQuoteRequestUpdateUseCaseUnitTest
    {
        [Fact]
        public async void Should_Update_HouseQuoteRequest_When_PUT()
        {
            // given

            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();
            HouseQuoteRequestUpdateRequest houseQuoteUpdate = new Fixture().Create<HouseQuoteRequestUpdateRequest>();
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
            var mockFileRepository = new Mock<IFileRepository>();

            mockQuoteRepository
                .Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<HouseQuoteRequest>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestUpdateRepoResponse(houseQuoteRequest, true)));

            mockFileRepository
                .Setup(repo => repo.GetFile(It.IsAny<int>()))
                .Returns(file);

            var useCase = new HouseQuoteRequestUpdateUseCase(mockConfiguration.Object, mockQuoteRepository.Object, mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<HouseQuoteRequestUpdateResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<HouseQuoteRequestUpdateResponse>()));

            // when

            var response = await useCase.Handle(
                houseQuoteUpdate, mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
