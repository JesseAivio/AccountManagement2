using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("Organization")]
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(9)]
        public string BusinessId { get; set; }
        [Required, MaxLength(100)]
        public string Info { get; set; }
        [Required, MaxLength(40)]
        public string Email { get; set; }

        public IList<ApplicationOrganizations> ApplicationOrganizations { get; set; }
    }
}
