using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class UserRegisterRequest
    {
        [JsonProperty("uid")]
        public string Id { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("user_type_id")]
        public int UserType { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("province")]
        public string Province { get;  set; }
    }
}
