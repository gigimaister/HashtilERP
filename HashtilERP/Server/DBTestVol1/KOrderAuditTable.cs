using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace HashtilERP.DBTestVol1
{
    public class KOrderAuditTable
    {
        public int K_OrderAuditTableId { get; set; }
        public int? JobId { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
        public string AuditString { get; set; }
    }
}
