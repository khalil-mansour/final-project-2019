
namespace Web.Api.Core.Domain.Entities
{
    public class Profile
    {
        public string UserId { get; }

        public int Sex { get; }

        public string BusinessName { get; }

        public string BusinessPhone { get; }

        public string BusinessEmail { get; }

        public string Bio { get; }

        public string AvatarImage{ get; }

        internal Profile(int sex, string businessName, string businessPhone, string businessEmail, string bio, string avatarImage)
        {
            Sex = sex;
            BusinessName = businessName;
            BusinessPhone = businessPhone;
            BusinessEmail = businessEmail;
            Bio = bio;
            AvatarImage = avatarImage;
        }

        internal Profile(string userid, int sex, string businessName, string businessPhone, string businessEmail, string bio, string avatarImage)
        {
            UserId = userid;
            Sex = sex;
            BusinessName = businessName;
            BusinessPhone = businessPhone;
            BusinessEmail = businessEmail;
            Bio = bio;
            AvatarImage = avatarImage;
        }
    }
}
