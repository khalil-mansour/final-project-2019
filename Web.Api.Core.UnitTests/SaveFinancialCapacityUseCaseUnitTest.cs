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
    public class SaveFinancialCapacityUseCaseUnitTest
    {   
        // mocked data
        private readonly string id = "1w23e";
        private readonly int annualIncome = 443444;
        private readonly int downPayment = 4300;
        private readonly int mensualDebt = 43;
        private readonly float interestRate = 4.4f;
        private readonly int municipalTaxes = 2900;
        private readonly int heatingCost  = 175;
        private readonly int condoFee = 0;

        [Fact]
        public async void Should_RegisterFinancialCapacity_When_Submit()
        {
            // given 

            // financialCapacityRepository symbolizes database
            var mockFinancialCapacityRepository = new Mock<IFinancialCapacityRepository>();
            mockFinancialCapacityRepository
                .Setup(repo => repo.Create(It.IsAny<FinancialCapacity>()))
                .Returns(Task.FromResult(new Dto.GatewayResponses.Repositories.FinancialCapacityRegisterRepoResponse(null, true)));

            // the main use case
            var useCase = new RegisterFinancialCapacityUseCase(mockFinancialCapacityRepository.Object);

            // link between layers
            var mockOutputPort = new Mock<IOutputPort<Dto.UseCaseResponses.FinancialCapacityRegisterRepoResponse>>();
            mockOutputPort
                .Setup(outputPort => outputPort
                .Handle(It.IsAny<Dto.UseCaseResponses.FinancialCapacityRegisterRepoResponse>()));

            // when

            var response = await useCase.HandleAsync(
                new FinancialCapacityRegisterRequest(id, 
                    annualIncome, 
                    downPayment, 
                    mensualDebt, 
                    interestRate, 
                    municipalTaxes, 
                    heatingCost, 
                    condoFee), 
                mockOutputPort.Object);
                        
            // done

            response.Should().BeTrue();
        }
    }
}
