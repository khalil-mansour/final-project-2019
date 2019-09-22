using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class RegisterUserRequest : IUseCaseRequest<RegisterUserResponse>
    {
        public string Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public RegisterUserRequest(string id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
