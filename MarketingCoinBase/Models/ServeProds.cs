using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class ServeProds
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long serveProdID { get; set; }
        
        [Required]
        public string typeOfService { get; set; }

        public double price { get; set; }

        [Column]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? createdAt { get; set; }

        [ForeignKey("partnerID")]
        public long partnerID { get; set; }
        public virtual Partners partners { get; set; }
    }
}
