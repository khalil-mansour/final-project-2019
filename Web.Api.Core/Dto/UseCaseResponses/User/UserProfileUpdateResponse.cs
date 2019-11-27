using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.User
{
    public class UserProfileUpdateResponse : UseCaseResponseMessage
    {
        public Profile Profile { get; }

        public IEnumerable<Error> Errors { get; }

        public UserProfileUpdateResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UserProfileUpdateResponse(Profile profile, bool success = false, string message = null) : base(success, message)
        {
            Profile =  profile;
        }
    }
}
