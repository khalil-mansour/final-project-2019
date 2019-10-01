using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class LoginUserResponse : UseCaseResponseMessage
    {
        public Token Token { get; }
        public IEnumerable<Error> Errors { get; }

        public LoginUserResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base (success, message)
        {
            Errors = errors;
        }

        public LoginUserResponse(Token token, bool success = false, string message = null) : base(success, message)
        {
            Token = token;
        }
    }
}
