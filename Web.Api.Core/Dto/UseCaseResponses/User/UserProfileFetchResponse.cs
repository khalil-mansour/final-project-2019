using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.User
{
    public class UserProfileFetchResponse : UseCaseResponseMessage
    {
        public Profile Profile { get; }
        public IEnumerable<Error> Errors { get; }

        public UserProfileFetchResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UserProfileFetchResponse(Profile profile, bool success = false, string message = null) : base(success, message)
        {
            Profile = profile;
        }
    }
}
