﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class K_OrderRemark
    {
        public int K_OrderRemarkId { get; set; }
        public int? JobId { get; set; }
        public string UserName { get; set; }
        public DateTime? K_OrderRemarkStamp { get; set; }
        public string Remark { get; set; }
    }
}
