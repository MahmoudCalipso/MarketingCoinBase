using System.ComponentModel.DataAnnotations;

namespace MarketingCoinBase.DTO.RequestModels
{
    public class SignInRequestModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
