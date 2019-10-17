using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class UserRegisterUseCase : IUserRegisterUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;

        public UserRegisterUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UserRegisterRequest message, IOutputPort<UserRegisterResponse> outputPort)
        {
            var response = await _userRepository.
                Create(new User(
                    message.Id,
                    message.FirstName,
                    message.LastName,
                    message.Email,
                    message.UserType,
                    message?.Phone,
                    message?.PostalCode,
                    message?.Province));

            outputPort.Handle(response.Success ? new UserRegisterResponse(response.User, true) : new UserRegisterResponse(new Error(response.Error.Code, "Error attempting to register a new User.")));

            if (!response.Success)
                logger.Error(response.Error.Description);

            return response.Success;
        }
    }
}