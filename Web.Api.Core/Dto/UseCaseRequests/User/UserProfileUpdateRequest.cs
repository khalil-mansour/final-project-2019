using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.User
{
    public class UserProfileUpdateRequest : IUseCaseRequest<UserProfileUpdateResponse>
    {
        public string Id { get; }
        public int Sex { get; }
        public string BusinessName { get; }
        public string BusinessPhone { get; }
        public string BusinessEmail { get; }
        public string Bio { get; }
        public string AvatarImage { get; }

        public UserProfileUpdateRequest(string id, int sex, string businessName, string businessphone, string businessemail, string bio, string avatarimage)
        {
            Id = id;
            Sex = sex;
            BusinessName = businessName;
            BusinessPhone = businessphone;
            BusinessEmail = businessemail;
            Bio = bio;
            AvatarImage = avatarimage;
        }
    }
}
