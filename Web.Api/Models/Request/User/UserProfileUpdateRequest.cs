using Newtonsoft.Json;
using System;

namespace Web.Api.Models.Request.User
{
    public class UserProfileUpdateRequest
    {
        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("business_name")]
        public string Business_Name { get; set; }

        [JsonProperty("business_phone")]
        public string Business_Phone { get; set; }

        [JsonProperty("business_email")]
        public string Business_Email { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("avatar_image")]
        public string Avatar_Image { get; set; }
    }
}
