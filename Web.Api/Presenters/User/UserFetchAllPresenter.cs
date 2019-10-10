using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters.User
{
    public class UserFetchAllPresenter : IOutputPort<UserFetchAllResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UserFetchAllPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserFetchAllResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonConvert.SerializeObject(response);
        }
    }
}
