using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class RegisterUserRequest : IUseCaseRequest<RegisterUserResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int UserType { get;  }
        public string Phone { get; }
        public string PostalCode{ get; }
        public string Province{ get; }

        public RegisterUserRequest(int id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, string province)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = usertype;
            Phone = phone;
            PostalCode = postalcode;
            Province = province;
        }
    }
}
