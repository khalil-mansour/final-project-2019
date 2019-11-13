using Xunit;
using FluentAssertions;
using Web.Api.Core.UseCases;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Moq;
using Web.Api.Core.Domain.Entities;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.Extensions.Configuration;
using System;
using Web.Api.Core.Dto.GatewayResponses.Repositories.File;

namespace Web.Api.Core.UnitTests
{
    public class FileFetchUseCaseUnitTest
    {
        // mocked data
        private readonly string storageID = "f4f4f";       
        
        [Fact]
        public async void Should_FetchFile_When_GET()
        {
            // given

            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigSection = new Mock<IConfigurationSection>();

            mockConfigSection
                .Setup(v => v.Value)
                .Returns("app_document_bucket");

            mockConfiguration
                .Setup(k => k.GetSection("BucketName"))
                .Returns(mockConfigSection.Object);

            var mockFileRepository = new Mock<IFileRepository>();
            mockFileRepository
                .Setup(repo => repo.Fetch(It.IsAny<string>()))
                // returns specific file
                .Returns(Task.FromResult(new FileFetchRepoResponse(new Domain.Entities.File("mockId", 1, "mockFileName", storageID, DateTime.Now, true), true)));

            var useCase = new FileFetchUseCase(mockConfiguration.Object, mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<FileFetchResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<FileFetchResponse>()));

            // when

            var response = await useCase.Handle(
                new FileFetchRequest(storageID), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
