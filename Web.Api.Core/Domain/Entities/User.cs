using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("firstname")]
        public string FirstName { get; }

        [JsonProperty("lastname")]
        public string LastName { get; }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("user_type_id")]
        public int UserType { get;  }

        [JsonProperty("phone")]
        public string Phone { get; }

        [JsonProperty("postal_code")]
        public string PostalCode{ get; }

        [JsonProperty("province")]
        public string Province{ get; }

        internal User(string id, string firstName, string lastName, string email, int usertype, string phone, string postalcode, string province)
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
