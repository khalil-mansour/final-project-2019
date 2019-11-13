using Xunit;
using FluentAssertions;
using Web.Api.Core.UseCases;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Moq;
using Web.Api.Core.Dto.GatewayResponses.Repositories.File;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Text;

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

            // in memory mocked file (FormFile is a garbage object and terrible to test)
            IFormFile mockFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt")
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

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
                .Setup(repo => repo.Create(It.IsAny<Domain.Entities.File>()))
                .Returns(Task.FromResult(new FileUploadRepoResponse(It.IsAny<Domain.Entities.File>(), true)));

            var useCase = new FileUploadUseCase(mockConfiguration.Object, mockFileRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<FileUploadResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<FileUploadResponse>()));

            // when

            var response = await useCase.Handle(
                new FileUploadRequest(mockFile, userId, docType, visible), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }

    }
}
