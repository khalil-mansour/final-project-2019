using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters
{
    public class UserUpdatePresenter : IOutputPort<UserUpdateResponse>, IOutputPort<UserProfileUpdateResponse>, IOutputPort<UserProfileFetchResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UserUpdatePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserUpdateResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? Models.Response.UserResponse.ToJson(response.User) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(UserProfileUpdateResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? Models.Response.UserProfileUpdateResponse.ToJson(response.Profile) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(UserProfileFetchResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? Models.Response.UserProfileUpdateResponse.ToJson(response.Profile) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
