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
    public class FindFinancialCapacityUseCaseUnitTest
    {
        private readonly string id = "1234";

        [Fact]
        public async void Should_FindFinancialCapacity_When_Submit()
        {
            // given

            // FinancialCapacityRepository symobolizes database
            var mockFinancialCapacityRepository = new Mock<IFinancialCapacityRepository>();
            mockFinancialCapacityRepository
                .Setup(repo => repo.FindById(It.IsAny<string>()))
                .Returns(Task.FromResult(new FinancialCapacityFindRepoResponse(null, true)));

            // the main use case
            var useCase = new FindFinancialCapacityUseCase(mockFinancialCapacityRepository.Object);

            // link between layers
            var mockOutputPort = new Mock<IOutputPort<Dto.UseCaseResponses.FinancialCapacityFindResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<Dto.UseCaseResponses.FinancialCapacityFindResponse>()));

            // when

            var response = await useCase.Handle(new FinancialCapacityFindRequest(id), mockOutputPort.Object);

            // done

            response.Should().BeTrue();
        }
    }
}