using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.User
{
    public class UserFetchRequest : IUseCaseRequest<UserFetchResponse>
    {
        public string UserID { get; }

        public UserFetchRequest(string userId)
        {
            UserID = userId;
        }
    }
}
