using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Library.API.Models
{
    public class License
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Application { get; set; }
        public bool isFree { get; set; }
        public bool isLocked { get; set; }
        public DateTime RenewDate { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
