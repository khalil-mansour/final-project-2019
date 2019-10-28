using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto;
namespace Web.Api.Core.UseCases
{
    public sealed class FindFinancialCapacityUseCase : IFinancialCapacityFindUseCase
    {
        private readonly IFinancialCapacityRepository _financialCapacityRepository;

        public FindFinancialCapacityUseCase(IFinancialCapacityRepository financialCapacityRepository)
        {
            _financialCapacityRepository = financialCapacityRepository;
        }

        public async Task<bool> Handle(FinancialCapacityFindRequest message, IOutputPort<FinancialCapacityFindResponse> outputPort)
        {
            // confirm financialCapacity exists with ID
            var response = await _financialCapacityRepository.FindById(message.ID);

            outputPort.Handle(response.Success ? 
                new FinancialCapacityFindResponse(response.FinancialCapacity, true, null) : 
                new FinancialCapacityFindResponse(new[] { new Error("find_financial_capacity_failure", "Unset financial capacity.") }));
            return response.Success;

        }
    }
}
