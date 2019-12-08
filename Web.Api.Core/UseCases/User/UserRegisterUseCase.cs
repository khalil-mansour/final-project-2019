using System;
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

        public async Task<bool> HandleAsync(UserRegisterRequest message, IOutputPort<UserRegisterRepoResponse> outputPort)
        {
            DateTimeOffset? birthday = null;
            // check timestamp
            if (message.Birthday != null)
                birthday = DateTimeOffset.Parse(message.Birthday);


            var response = await _userRepository.
                Create(new Domain.Entities.User(
                    message.Id,
                    message.FirstName,
                    message.LastName,
                    message.Email,
                    message.UserType,
                    message?.Phone,
                    message?.PostalCode,
                    message?.Province,
                    birthday));

            outputPort.Handle(response.Success ? new UserRegisterRepoResponse(response.User, true) : new UserRegisterRepoResponse(new[] { new Error("Registration Failed", "Failed to create a new user.") }));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}