using Newtonsoft.Json;
using System;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Models.Response
{
    public class UserResponse
    {
        [JsonProperty("user_id")]
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

        [JsonProperty("postal_code")]
        public string PostalCode{ get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("province_id")]
        public int? Province{ get; set; }

        public UserResponse() {
        }

       public static string ToJson(User user) {
            var response = new UserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = user.UserType,
                Phone = user.Phone,
                PostalCode = user.PostalCode,
                Province = user.Province
            };
            // check if DateTime? has a value before mapping response
            if (user.Birthday.HasValue)
                response.Birthday = user.Birthday.Value.ToString("yyyy-MM-dd");

            return JsonConvert.SerializeObject(response, Formatting.Indented);

        }
    }
}
