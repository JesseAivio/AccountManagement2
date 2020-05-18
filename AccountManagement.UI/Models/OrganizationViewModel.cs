using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.UI.Models
{
    public class OrganizationViewModel
    {
        public IEnumerable<Organization> Organizations { get; set; }
        public IEnumerable<ApplicationOrganizations> ApplicationOrganizations { get; set; }
        public IEnumerable<Application> Applications { get; set; }
        public IEnumerable<License> Licenses { get; set; }
        public Pager Pager { get; set; }
    }
}
