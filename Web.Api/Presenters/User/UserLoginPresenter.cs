using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters
{
    public class UserLoginPresenter : IOutputPort<UserLoginResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UserLoginPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserLoginResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            ContentResult.Content = response.Success ? UserResponse.ToJson(response.User) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
