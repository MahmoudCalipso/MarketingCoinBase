﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingCoinBase.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long userID { get; set; }
        [Column]
        [Required]
        public  string username { get; set; }
        [Column]
        [Required]
        [RegularExpression(@"((\+|00)216)?[0-9]{8}", ErrorMessage = "Characters are not allowed.")]
        public string phone { get; set; }
        [Column]
        [Required]
        [EmailAddress]
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
        [Required]
        [ForeignKey("userRef")]
        public long userRef { get; set; }
        public virtual Users user { get; set; }
        [Column]
        [Required]
        [ForeignKey("balanceID")]
        public long balanceID { get; set; }
        public virtual AccountBalances balance { get; set; }
        [Column]
        [Required]
        [ForeignKey("roleID")]
        public long roleID { get; set; }
        public virtual Roles role { get; set; }
        [Column]
        [Required]
        [ForeignKey("commissionID")]
        public long commissionID { get; set; }
        public virtual Commissions commission { get; set; }
    }
}
        
