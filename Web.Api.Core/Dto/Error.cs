using Newtonsoft.Json;

namespace Web.Api.Core.Dto
{
    public sealed class Error
    {
        [JsonProperty("code")]
        public string Code { get; }

        [JsonProperty("description")]
        public string Description { get; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}