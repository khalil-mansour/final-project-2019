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
using Microsoft.AspNetCore.Http;

namespace Web.Api.Core.UnitTests
{
    public class FileUploadUseCaseUnitTest
    {
        // mocked data
        private readonly string userId = "r4r4r";
        private readonly int docType = 1;
        private readonly bool visible = false;


        [Fact]
        public async void Should_UploadFile_When_POST()
        {
            // given

            var mockFileRepository = new Mock<IFileRepository>();
            mockFileRepository
                .Setup(repo => repo.Create(It.IsAny<File>()))
                .Returns(Task.FromResult(new FileUploadRepoResponse(It.IsAny<File>(), true)));

            var useCase = new FileUploadUseCase(It.IsAny<IConfiguration>(), mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<FileUploadResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<FileUploadResponse>()));

            // when

            var response =await useCase.Handle(
                new FileUploadRequest(It.IsAny<IFormFile>(), userId, docType, visible), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }

    }
}
