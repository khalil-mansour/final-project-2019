using Newtonsoft.Json;
using System;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class UserProfileUpdateResponse
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("business_phone")]
        public string BusinessPhone { get; set; }

        [JsonProperty("business_email")]
        public string BusinessEmail { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("avatar_image")]
        public string AvatarImage { get; set; }

        public UserProfileUpdateResponse() { }

        public static string ToJson(Profile profile)
        {
            var response = new UserProfileUpdateResponse()
            {
                UserId = profile.UserId,
                Sex = profile.Sex,
                BusinessName = profile.BusinessName,
                BusinessPhone = profile.BusinessPhone,
                BusinessEmail = profile.BusinessEmail,
                Bio = profile.Bio,
                AvatarImage = profile.AvatarImage
            };
            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }
    }
}
