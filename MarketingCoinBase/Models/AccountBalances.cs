using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class AccountBalances
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long balanceID { get; set; }

        [Required]
        public double soldeBalance { get; set; }
        [Required]
        public double soldeInvest { get; set; } 
        public virtual ICollection<Users> users { get; set; }
    }
}
