
using System;

namespace Web.Api.Core.Domain.Entities
{
    public class User
    {
        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public int UserType { get;  }

        public string Phone { get; }

        public string PostalCode{ get; }

        public int? Province{ get; }

        public DateTimeOffset? Birthday { get; set; }

        public Profile Profile { get; set; }


        internal User(string id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, int? province, DateTimeOffset? birthday)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = usertype;
            Phone = phone;
            PostalCode = postalcode;
            Province = province;
            Birthday = birthday;
        }

        internal User(string id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, int? province, DateTimeOffset? birthday, Profile profile)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = usertype;
            Phone = phone;
            PostalCode = postalcode;
            Province = province;
            Birthday = birthday;
            Profile = profile;
        }

        internal User(string id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, int? province, DateTime birthday)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = usertype;
            Phone = phone;
            PostalCode = postalcode;
            Province = province;
            Birthday = DateTimeOffset.Parse(birthday.ToString());
        }

        internal User(string id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, int? province, DateTime birthday, Profile profile)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = usertype;
            Phone = phone;
            PostalCode = postalcode;
            Province = province;
            Birthday = DateTimeOffset.Parse(birthday.ToString());
            Profile = profile;
        }
    }
}
