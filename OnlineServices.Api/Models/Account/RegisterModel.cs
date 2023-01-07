using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Api.Models
{
    public class RegisterModel
    {
        public string MobileNo { get; set; }
        public int PersonTypeId { get; set; }
    }
}