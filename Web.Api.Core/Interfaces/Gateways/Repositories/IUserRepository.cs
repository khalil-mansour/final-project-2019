using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Dto.GatewayResponses.Repositories.User;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository
    {
        Task<UserRegisterRepoResponse> Create(User user);
        Task<UserLoginRepoResponse> FindById(string id);
        Task<UserUpdateRepoResponse> UpdateUser(string userId, User user);
        Task<UserProfileUpdateRepoResponse> UpdateUserProfile(string userId, Profile profile);
        Task<UserFetchRepoResponse> GetUser(string userId);
        Task<UserProfileUpdateRepoResponse> GetProfile(string userId);
    }
}
