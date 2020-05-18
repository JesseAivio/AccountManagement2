using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public IList<ApplicationOrganizations> ApplicationOrganizations { get; set; }
    }
}
