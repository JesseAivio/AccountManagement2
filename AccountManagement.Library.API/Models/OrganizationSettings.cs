using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Library.API.Models
{
    public class OrganizationSettings
    {
        public Guid Id { get; set; }

        public string Organization { get; set; }

        public string Setting { get; set; }

        public string Value { get; set; }
    }
}
