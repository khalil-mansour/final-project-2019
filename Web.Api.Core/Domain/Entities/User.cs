using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int UserType { get;  }
        public string Phone { get; }
        public string PostalCode{ get; }
        public string Province{ get; }

        internal User(int id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, string province)
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
