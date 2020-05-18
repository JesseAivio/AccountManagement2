using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.UI.Models
{
    public class ApplicationViewModel
    {
        public IEnumerable<Application> Applications { get; set; }
        public IEnumerable<License> Licenses { get; set; }
        public Pager Pager { get; set; }
    }
}
