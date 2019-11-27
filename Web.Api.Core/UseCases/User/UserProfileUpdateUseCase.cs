using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests.User;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class UserProfileUpdateUseCase : IUserProfileUpdateUseCase
    {
        // logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;

        public UserProfileUpdateUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(UserProfileUpdateRequest message, IOutputPort<UserProfileUpdateResponse> outputPort)
        {
            var response = await _userRepository.
                UpdateUserProfile(
                    message.Id,
                    new Profile(
                        message.Sex,
                        message.BusinessName,
                        message.BusinessPhone,
                        message.BusinessEmail,
                        message.Bio,
                        message.AvatarImage));

            outputPort.Handle(response.Success ?
                new UserProfileUpdateResponse(response.Profile, true)
                :
                new UserProfileUpdateResponse(new[] { new Error("Update Failed", "Unable to update broker user.") }));


            if (!response.Success)
                logger.Error(response.Errors.First()?.Description);

            return response.Success;
        }
    }
}