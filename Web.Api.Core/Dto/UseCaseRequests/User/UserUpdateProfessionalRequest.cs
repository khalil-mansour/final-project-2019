using Web.Api.Core.Dto.UseCaseRequests.User;
using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class UserUpdateProfessionalRequest : IUseCaseRequest<UserUpdateResponse>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
        public string Postal_Code { get; set; }
        public int? Province { get; set; }
        public UserProfileUpdateRequest ProfessionalProfile { get; }

        public UserUpdateProfessionalRequest(
            string userId, string firstname, string lastname,
            string email, string phone,
            string birthday, string postalCode, int? province, UserProfileUpdateRequest profProfile)
        {
            UserId = userId;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Phone = phone;
            Birthday = birthday;
            Postal_Code = postalCode;
            Province = province;
            ProfessionalProfile = profProfile;
        }
    }
}
