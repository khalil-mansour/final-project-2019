using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class UserUpdateUseCase : IUserUpdateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;

        public UserUpdateUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(UserUpdateRequest message, IOutputPort<UserUpdateResponse> outputPort)
        {
            DateTimeOffset? birthday = null;
            // check timestamp
            if (message.Birthday != null)
                birthday = DateTimeOffset.Parse(message.Birthday);

            var response = await _userRepository.
                UpdateUser(
                    message.Id,
                    new Domain.Entities.User(
                        message.Id,
                        message.FirstName,
                        message.LastName,
                        "",
                        0,
                        message?.Phone,
                        message?.PostalCode,                        
                        message?.Province,
                        birthday));

            outputPort.Handle(response.Success ? new UserUpdateResponse(response.User, true) : new UserUpdateResponse(response.Errors));

            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}