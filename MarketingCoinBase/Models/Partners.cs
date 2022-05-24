using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class Partners
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long partnerID { get; set; }

        [Required] 
        [StringLength(50)]
        public string partnerName { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }

        [Required]
        [StringLength(50)]
        public string location { get; set; }

        [NotMapped]
        public virtual ICollection<ServeProds> serveProds { get; }

    }
}
