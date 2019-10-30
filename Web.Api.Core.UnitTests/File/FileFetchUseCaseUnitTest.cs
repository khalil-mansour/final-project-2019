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

            var mockFileRepository = new Mock<IFileRepository>();
            mockFileRepository
                .Setup(repo => repo.Fetch(It.IsAny<string>()))
                .Returns(Task.FromResult(new FileFetchRepoResponse(It.IsAny<File>(), true)));

            var useCase = new FileFetchUseCase(It.IsAny<IConfiguration>(), mockFileRepository.Object);

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
