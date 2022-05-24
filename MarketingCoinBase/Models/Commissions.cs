using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class Commissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long commissionID { get; set; }
        [Required]
        public double totMumbers { get; set; }

        public double commission { get; set; }
        [Required]
        public int datePeriod { get; set; }

        public virtual UserPartners userPartners { get; set; }


    }
}
