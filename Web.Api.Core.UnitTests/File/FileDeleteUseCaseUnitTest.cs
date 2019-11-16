using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Web.Api.Core.Dto.GatewayResponses.Repositories.File;
using Web.Api.Core.Dto.UseCaseRequests.File;
using Web.Api.Core.Dto.UseCaseResponses.File;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases.File;
using Xunit;

namespace Web.Api.Core.UnitTests.File
{
    public class FileDeleteUseCaseUnitTest
    {
        private readonly int documentID = 40;

        [Fact]
        public async void Should_DeleteFile_When_DELETE()
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
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                // returns specific file
                .Returns(Task.FromResult(new FileDeleteRepoResponse(new Domain.Entities.File("mockId", 1, "mockFileName", "storageID", DateTime.Now, true), true)));

            var useCase = new FileDeleteUseCase(mockConfiguration.Object, mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<FileDeleteResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<FileDeleteResponse>()));

            // when

            var response = await useCase.Handle(new FileDeleteRequest(documentID), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
