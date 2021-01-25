using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Data
{
    public partial class UpdateK_PassportAudit
    {
        [Key]
        public int UpdateK_PassportAuditId { get; set; }
        public int? K_PassportId { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
        public string HamBefore { get; set; }
        public string HamAfter { get; set; }
        public string GamBefore { get; set; }
        public string GamAfter { get; set; }
        public string MagBefore { get; set; }
        public string MagAfter { get; set; }
        public string AVGBefore { get; set; }
        public string AVGAfter { get; set; }
        public string StatBefore { get; set; }
        public string StatAfter { get; set; }
    }
}
