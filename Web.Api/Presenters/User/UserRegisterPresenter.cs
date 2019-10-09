using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters
{
    public class UserRegisterPresenter : IOutputPort<UserRegisterResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UserRegisterPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserRegisterResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonConvert.SerializeObject(response);
        }
    }
}
