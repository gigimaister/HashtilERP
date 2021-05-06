using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.DBTestVol1
{
    public class KOrderRemark
    {
        public int K_OrderRemarkId { get; set; }
        public int? JobId { get; set; }
        public string UserName { get; set; }
        public DateTime? K_OrderRemarkStamp { get; set; }
        public string Remark { get; set; }
    }
}
