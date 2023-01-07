using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Api.Models
{
    public class ConfirmMobileNoModel
    {
        public string MobileNo { get; set; }
        public string ConfirmNo { get; set; }
        public int PersonTypeId { get; set; }
    }
}