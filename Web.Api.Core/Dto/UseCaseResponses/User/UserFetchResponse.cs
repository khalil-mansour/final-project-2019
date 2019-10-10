using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.UseCaseResponses.User
{
   public  class UserFetchResponse
    {
        public Domain.Entities.User User { get; }
        public IEnumerable<Error> Errors { get; }

        public UserFetchResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base (success, message)
        {
            Errors = errors;
        }

        public UserFetchResponse(Domain.Entities.User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }
    }
}
