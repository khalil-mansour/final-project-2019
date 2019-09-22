using System;
using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class RegisterUserResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public string Error { get; }

        public RegisterUserResponse(Error error, bool success = false, string message = null) : base(success, message)
        {
            Error = error.Description;
        }

        public RegisterUserResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}