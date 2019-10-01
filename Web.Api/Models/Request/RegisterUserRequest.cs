using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models.Request
{
    public class RegisterUserRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("usertype")]
        public int UserType { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
    }
}
