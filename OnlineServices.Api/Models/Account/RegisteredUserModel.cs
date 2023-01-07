namespace OnlineServices.Api.Models
{
  public class RegisteredUserModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string MobileNumber { get; set; }
        public bool MobileNumberConfirmed { get; set; }
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public int? EmployeeNo { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }
}