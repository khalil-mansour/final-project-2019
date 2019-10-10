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

namespace Web.Api.Core.UnitTests
{   
    public class RegisterUserUseCaseUnitTest
    {   
        // mocked data
        private readonly string id = "1w23e";
        private readonly string firstName = "boubou";
        private readonly string lastName = "Bouboubou";
        private readonly string email = "boubou@boubou.com";
        private readonly int userType = 1;
        private readonly string phone = "";
        private readonly string province  = "Qc";
        private readonly string postalcode = "1221";


        [Fact]
        public async void Should_RegisterUser_When_Submit()
        {
            // given

            // userRepository symbolizes database
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.Create(It.IsAny<User>()))
                .Returns(Task.FromResult(new Dto.GatewayResponses.Repositories.UserRegisterRepoResponse(null, true)));

            // the main use case
            var useCase = new RegisterUserUseCase(mockUserRepository.Object);

            // link between layers
            var mockOutputPort = new Mock<IOutputPort<Dto.UseCaseResponses.UserRegisterRepoResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<Dto.UseCaseResponses.UserRegisterRepoResponse>()));

            // when

            var response = await useCase.Handle(new UserRegisterRequest(id, firstName, lastName, email, userType, phone, postalcode, province), mockOutputPort.Object);
                        
            // done

            response.Should().BeTrue();
        }
    }
}
