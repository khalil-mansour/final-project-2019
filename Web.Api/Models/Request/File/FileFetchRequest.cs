using Newtonsoft.Json;

namespace Web.Api.Models.Request
{
    public class FileFetchRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
