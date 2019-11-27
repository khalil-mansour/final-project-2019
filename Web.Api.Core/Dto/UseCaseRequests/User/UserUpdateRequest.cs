using Web.Api.Core.Dto.UseCaseResponses.User;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class UserUpdateRequest : IUseCaseRequest<UserUpdateResponse>
    {
        public string Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Phone { get; }
        public string PostalCode { get; }
        public int? Province { get; }
        public string Birthday { get; }

        public UserUpdateRequest(string id, string firstName, string lastName, string phone, string postalcode, int? province, string birthdate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            PostalCode = postalcode;
            Province = province;
            Birthday = birthdate;
        }
    }
}
