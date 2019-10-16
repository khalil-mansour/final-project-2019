using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class UserLoginResponse : UseCaseResponseMessage
    {
        public User User { get; }
        public Error Error { get; }

        public UserLoginResponse(Error error, bool success = false, string message = null) : base (success, message)
        {
            Error = error;
        }

        public UserLoginResponse(User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }
    }
}
