using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Enteties
{
    public class AccountLogEntity
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
