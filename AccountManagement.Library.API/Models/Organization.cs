using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Library.API.Models
{
    public class Organization
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string BusinessId { get; set; }

        public string Info { get; set; }

        public string Email { get; set; }
    }
}
