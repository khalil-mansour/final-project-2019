using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.User
{
    public class UserProfileFetchRequest : IUseCaseRequest<UserProfileFetchResponse>
    {
        public string ID { get; }

        public UserProfileFetchRequest(string id)
        {
            ID = id;
        }
    }
}
