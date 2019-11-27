using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests.User;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class UserFetchUseCase : IUserFetchUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;

        public UserFetchUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(UserFetchRequest message, IOutputPort<UserFetchResponse> outputPort)
        {
            var response = await _userRepository.GetUser(message.UserID);

            outputPort.Handle(response.Success ? new UserFetchResponse(response.User, true) : new UserFetchResponse(response.Errors));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}