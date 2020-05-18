using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("EmailTemplate")]
    public class EmailTemplate
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Subject { get; set; }
        [Required, MaxLength(2000)]
        public string Body { get; set; }
        [Required, MaxLength(20)]
        public string Type { get; set; }
    }
}
