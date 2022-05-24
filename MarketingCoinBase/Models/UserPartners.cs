using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class UserPartners
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long userPartID { get; set; }

        [Column]
        [Required]
        [ForeignKey("userID")]
        public long userID { get; set; }
        public virtual Users user { get; set; }

        [Column]
        [Required]
        [ForeignKey("partnerID")]
        public long partnerID { get; set; }
        public virtual Partners partner { get; set; }

        [Column]
        [Required]
        [ForeignKey("balanceID")]
        public long balanceID { get; set; }
        public virtual AccountBalances balance { get; set; }
        [Column]
        [Required]
        [ForeignKey("commissionID")]
        public long commissionID { get; set; }
        public virtual Commissions commission { get; set; }
    }
}
