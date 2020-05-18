using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Data.Models
{
    [Table("License")]
    public class License
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(30)]
        public string Key { get; set; }
        [ForeignKey("ApplicationLink")]
        public Guid Application { get; set; }
        public bool isFree { get; set; }
        public bool isLocked { get; set; }
        public DateTime RenewDate { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual Application ApplicationLink { get; set; }
    }
}
