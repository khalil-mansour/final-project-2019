using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class UserRegisterRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("user_type_id")]
        public int User_Type_Id { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("postal_code")]
        public string Postal_Code { get; set; }

        [JsonProperty("province_id")]
        public int? Province { get;  set; }
    }
}
