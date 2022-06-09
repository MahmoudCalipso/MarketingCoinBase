using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketingCoinBase.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long userID { get; set; }
        [Column]
        [Required]
        [Index(IsUnique = true )]
        public  string username { get; set; }
        [Column]
        [Required]
        [Index(IsUnique = true)]
        [RegularExpression(@"((\+|00)216)?[0-9]{8}", ErrorMessage = "Characters are not allowed.")]
        public string phone { get; set; }
        [Column]
        [Required]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string email { get; set; }
        [Column]
        [Required]
        public string password { get; set; }
        [Column]
        [Required]
        public bool status { get; set; }
        [Column]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? createdAt { get; set; }
        [Column]
        [ForeignKey("userRef")]
        public virtual Users user { get; set; }
      
        [Column]
        [Required]
        [ForeignKey("roleID")]
        public long roleID { get; set; }
        public virtual Roles role { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
        
