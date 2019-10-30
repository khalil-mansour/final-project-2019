using Xunit;
using FluentAssertions;
using Web.Api.Core.UseCases;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Moq;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Web.Api.Core.UnitTests
{
    public class FileFetchAllUseCaseUnitTest
    {
        // mocked data
        private readonly string userID = "5t5t";


        [Fact]
        public async void Should_FetchAllFiles_When_GET()
        {
            // given

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .SetupGet(m => m[It.Is<string>(s => s == "BucketName")])
                .Returns("app_document_bucket");

            var mockFileRepository = new Mock<IFileRepository>();
            mockFileRepository
                .Setup(repo => repo.FetchAll(It.IsAny<string>()))
                .Returns(Task.FromResult(
                    new FileFetchAllRepoResponse(It.IsAny<IEnumerable<File>>(), true)));

            var useCase = new FileFetchAllUseCase(mockConfiguration.Object, mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<FileFetchAllResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<FileFetchAllResponse>()));

            // when

            var response = await useCase.Handle(
                new FileFetchAllRequest(userID), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
