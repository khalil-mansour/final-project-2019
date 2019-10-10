using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class UserRegisterResponse : UseCaseResponseMessage
    {
        public Domain.Entities.User User { get; }
        public IEnumerable<Error> Errors { get; }

        public UserRegisterResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UserRegisterResponse(Domain.Entities.User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }
    }
}