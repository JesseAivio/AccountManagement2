using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Library.API.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        public string Organization { get; set; }
    }
}
