using System.ComponentModel.DataAnnotations;

namespace MarketingCoinBase.DTO.RequestModels
{
    public class SignUpRequestModel
    {

        [Required]
        public string username { get; set; }
     
        [Required]
        [RegularExpression(@"((\+|00)216)?[0-9]{8}", ErrorMessage = "Characters are not allowed.")]
        public string phone { get; set; }
      
        [Required]
        [EmailAddress]
        public string email { get; set; }
       
        [Required]
        public string password { get; set; }
   
        [Required]
        public bool status { get; set; }

    }
}
