using Newtonsoft.Json;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileFetchAllRequest : IUseCaseRequest<FileFetchAllResponse>
    {

        [JsonProperty("uid")]
        public string UserId { get; }

        public FileFetchAllRequest(string userId)
        {
            UserId = userId;
        }
    }
}
