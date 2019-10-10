using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

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
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.User, Formatting.Indented) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
