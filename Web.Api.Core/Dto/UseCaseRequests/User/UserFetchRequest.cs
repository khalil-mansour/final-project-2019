using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.User
{
   public class UserFetchRequest : IUseCaseRequest<UserFetchResponse>
    {
        public string ID { get; }

        public UserFetchRequest(string id)
        {
            ID = id;
        }
    }
}
