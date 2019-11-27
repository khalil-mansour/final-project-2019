using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class UserLoginRequest
    {
        [JsonProperty("user_id")]
        public string User_Id { get; set; }
    }
}
