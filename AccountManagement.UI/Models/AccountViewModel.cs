using AccountManagement.Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.UI.Models
{
    public class AccountViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Organization> Organizations { get; set; }
        public IEnumerable<AccountLog> AccountLogs { get; set; }
        public Pager Pager { get; set; }
    }
}
