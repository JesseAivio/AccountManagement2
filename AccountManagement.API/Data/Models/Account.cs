using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(40)]
        public string Email { get; set; }
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
        [Required, MaxLength(100)]
        public string Salt { get; set; }
        [Required, MaxLength(20)]
        public string Role { get; set; }
        [ForeignKey("OrganizationLink")]
        public Guid Organization { get; set; }
        
        public virtual Organization OrganizationLink { get; set; }
    }
}
