using AutoFixture;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
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

    public class HouseQuoteGetAllRequestUseCaseUnitTest
    {
        // mocked data
        private readonly string userID = "5t5t";


        [Fact]
        public async void Should_FetchAll_HouseQuoteRequest_When_Receiving_UserId()
        {
            // given
            HouseQuoteRequest houseQuoteRequest = new Fixture().Create<HouseQuoteRequest>();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.Create(It.IsAny<User>()))
                .Returns(Task.FromResult(new Dto.GatewayResponses.Repositories.UserRegisterRepoResponse(null, true)));

            var mockQuoteRepository = new Mock<IQuoteRequestRepository>();
            mockQuoteRepository
                .Setup(repo => repo.GetAllQuoteForUser(It.IsAny<string>()))
                .Returns(Task.FromResult(
                    new HouseQuoteRequestFetchAllRepoResponse(new List<HouseQuoteRequest> { houseQuoteRequest }, true)));

            var useCase = new HouseQuoteRequestFetchAllUseCase(mockQuoteRepository.Object, mockUserRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<HouseQuoteRequestFetchAllResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<HouseQuoteRequestFetchAllResponse>()));

            // when

            var response = await useCase.Handle(
                new HouseQuoteRequestFetchAllRequest(userID), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}
