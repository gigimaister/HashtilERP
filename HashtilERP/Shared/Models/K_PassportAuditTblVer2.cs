using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class K_PassportAuditTblVer2
    {
        [Key]
        public int Id { get; set; }
        public int K_PassportId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string AuditString { get; set; }
    }
}
