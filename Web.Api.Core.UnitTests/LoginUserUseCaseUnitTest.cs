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
    public class LoginUserUseCaseUnitTest
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
                .Returns(Task.FromResult(new LoginUserResponse(null, true)));

            // the main use case
            var useCase = new LoginUserUseCase(mockUserRepository.Object);

            // link between layers
            var mockOutputPort = new Mock<IOutputPort<Dto.UseCaseResponses.LoginUserResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<Dto.UseCaseResponses.LoginUserResponse>()));

            // when

            var response = await useCase.Handle(new LoginUserRequest(id), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
