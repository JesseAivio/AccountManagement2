using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("AccountLog")]
    public class AccountLog
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("AccountLink")]
        public Guid Account { get; set; }
        [ForeignKey("ApplicationLink")]
        public Guid Application { get; set; }
        public bool wasSuccessful { get; set; }
        [Required, MaxLength(50)]
        public string HWID { get; set; }
        [Required, MaxLength(15)]
        public string IpAddress { get; set; }

        public DateTime Date { get; set; }

        public virtual Account AccountLink { get; set; }

        public virtual Application ApplicationLink { get; set; }
    }
}