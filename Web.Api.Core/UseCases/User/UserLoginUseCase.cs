using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Dto;
namespace Web.Api.Core.UseCases
{
    public sealed class UserLoginUseCase : IUserLoginUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;

        public UserLoginUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UserLoginRequest message, IOutputPort<UserLoginResponse> outputPort)
        {
            // confirm user exists with ID
            var response = await _userRepository.FindById(message.ID);

            outputPort.Handle(response.Success ? new UserLoginResponse(response.User, false, null) : new UserLoginResponse(new Error(response.Error.Code, "Invalid credentials.")));

            if (!response.Success)
                logger.Error(response.Error.Description);

            return response.Success;

        }
    }
}
