using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("ApplicationOrganizations")]
    public class ApplicationOrganizations
    {
        [ForeignKey("ApplicationLink")]
        public Guid Application { get; set; }
        [ForeignKey("OrganizationLink")]
        public Guid Organization { get; set; }
        [ForeignKey("LicenseLink")]
        public Guid License { get; set; }

        public virtual Application ApplicationLink { get; set; }

        public virtual Organization OrganizationLink { get; set; }

        public virtual License LicenseLink { get; set; }
    }
}
