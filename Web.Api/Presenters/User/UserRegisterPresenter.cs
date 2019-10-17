using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

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
             
            ContentResult.Content = response.Success ? UserResponse.ToJson(response.User) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

    }
}
