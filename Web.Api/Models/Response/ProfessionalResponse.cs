using Newtonsoft.Json;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class ProfessionalResponse
    {
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

        public ProfessionalResponse() { }

        public static ProfessionalResponse MapProperty(Profile profile)
        {
            return new ProfessionalResponse()
            {
                Sex = profile.Sex,
                BusinessName = profile.BusinessName,
                BusinessPhone = profile.BusinessPhone,
                BusinessEmail = profile.BusinessEmail,
                Bio = profile.Bio,
                AvatarImage = profile.AvatarImage
            };
        }
    }
}
