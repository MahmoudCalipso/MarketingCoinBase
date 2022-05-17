using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class Roles
    {
        public Roles()
        {
            users = new List<Users>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long roleID { get; set; }
        [Required]
        public string roleName { get; set; }
        [NotMapped]
        public virtual ICollection<Users> users { get; }
    }
}
