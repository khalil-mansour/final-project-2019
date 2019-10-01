using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters
{
    public class LoginUserPresenter : IOutputPort<LoginUserResponse>
    {
        public JsonContentResult ContentResult { get; }

        public LoginUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(LoginUserResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.Token, Formatting.Indented) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
