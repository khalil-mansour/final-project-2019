using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.User;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases.User;

namespace Web.Api.Core.UseCases.User
{
    class UserFetchUseCase : IUserFetchUseCase
    {

        private readonly IUserRepository _userRepository;

        public UserFetchUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UserFetchRequest message, IOutputPort<UserFetchResponse> outputPort)
        {
            // confirm user exists with ID
            var response = await _userRepository.FindById(message.ID);

            outputPort.Handle(response.Success ? new UserFetchResponse(response.User, false, null) : new UserFetchResponse(new[] { new Error("Retrieving user failed", "Invalid parameter") }));
            return response.Success;

        }
    }
}
