using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases;
using Xunit;

namespace Web.Api.Core.UnitTests
{
    public class UserLoginUseCaseUnitTest
    {
        private readonly string id = "1234";

        [Fact]
        public async void Should_LoginUser_When_Submit()
        {
            // given

            // userRepository symobolizes database
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.FindById(It.IsAny<string>()))
                .Returns(Task.FromResult(new UserLoginRepoResponse(null, true)));

            // the main use case
            var useCase = new UserLoginUseCase(mockUserRepository.Object);

            // link between layers
            var mockOutputPort = new Mock<IOutputPort<Dto.UseCaseResponses.UserLoginResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<Dto.UseCaseResponses.UserLoginResponse>()));

            // when

            var response = await useCase.HandleAsync(new UserLoginRequest(id), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
