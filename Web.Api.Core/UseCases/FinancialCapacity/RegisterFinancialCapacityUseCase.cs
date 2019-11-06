using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class RegisterFinancialCapacityUseCase : IFinancialCapacityRegisterUseCase
    {
        private readonly IFinancialCapacityRepository _financialCapacityRepository;

        public RegisterFinancialCapacityUseCase(IFinancialCapacityRepository financialCapacityRepository)
        {
            _financialCapacityRepository = financialCapacityRepository;
        }

        public async Task<bool> Handle(FinancialCapacityRegisterRequest message, IOutputPort<FinancialCapacityRegisterRepoResponse> outputPort)
        {
            var response = await _financialCapacityRepository.
                Create(new FinancialCapacity(
                    message.Id,
                    message.AnnualIncome, 
                    message.DownPayment, 
                    message.MensualDebt, 
                    message.InterestRate, 
                    message.MunicipalTaxes, 
                    message.HeatingCost, 
                    message.CondoFee));

            outputPort.Handle(response.Success ? 
            new FinancialCapacityRegisterRepoResponse(response.FinancialCapacity, true) : 
            new FinancialCapacityRegisterRepoResponse(response.Errors));
            return response.Success;
        }
    }
}