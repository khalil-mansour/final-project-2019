using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests.User;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.User;

namespace Web.Api.Core.UseCases.User
{
    public sealed class UserProfileFetchUseCase : IUserProfileFetchUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;

        public UserProfileFetchUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(UserProfileFetchRequest message, IOutputPort<UserProfileFetchResponse> outputPort)
        {
            var response = await _userRepository.GetProfile(message.ID);

            outputPort.Handle(response.Success ? new UserProfileFetchResponse(response.Profile, true) : new UserProfileFetchResponse(response.Errors));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}
