using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository
    {
        Task<UserRegisterRepoResponse> Create(User user);
        Task<UserLoginRepoResponse> FindById(string id);
    }
}
