using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters
{
    public class UserRegisterPresenter : IOutputPort<UserRegisterRepoResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UserRegisterPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserRegisterRepoResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.User, Formatting.Indented) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
