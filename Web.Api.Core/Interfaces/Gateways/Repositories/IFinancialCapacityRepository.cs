using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IFinancialCapacityRepository
    {
        Task<FinancialCapacityRegisterRepoResponse> Create(FinancialCapacity financialCapacity);

        Task<FinancialCapacityFindRepoResponse> FindById(string id);
    }
}
