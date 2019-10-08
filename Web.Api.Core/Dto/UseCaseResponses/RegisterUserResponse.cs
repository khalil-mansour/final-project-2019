using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class RegisterUserResponse : UseCaseResponseMessage
    {
        public User User { get; }
        public IEnumerable<Error> Errors { get; }

        public RegisterUserResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public RegisterUserResponse(User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }
    }
}