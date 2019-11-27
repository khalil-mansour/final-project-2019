using Newtonsoft.Json;
using System;

namespace Web.Api.Models.Request
{
    public class UserUpdateRequest
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("postal_code")]
        public string Postal_Code { get; set; }

        [JsonProperty("province_id")]
        public int? Province_Id { get;  set; }
    }
}
