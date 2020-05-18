using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Library.API.Models
{
    public class AccountLog
    {
        public Guid Id { get; set; }

        public string Account { get; set; }

        public string Application { get; set; }

        public bool wasSuccessful { get; set; }

        public string HWID { get; set; }

        public string IpAddress { get; set; }

        public DateTime Date { get; set; }
    }
}
