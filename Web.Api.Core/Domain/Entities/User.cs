using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class User
    {

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        internal User(string id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
