using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.User
{
    public class UserUpdateResponse : UseCaseResponseMessage
    {
        public Domain.Entities.User User { get; }
        public IEnumerable<Error> Errors { get; }

        public UserUpdateResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UserUpdateResponse(Domain.Entities.User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }
    }
}