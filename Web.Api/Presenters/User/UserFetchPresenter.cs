using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters
{
    public class UserFetchPresenter : IOutputPort<UserFetchResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UserFetchPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserFetchResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? UserResponse.ToJson(response.User) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
