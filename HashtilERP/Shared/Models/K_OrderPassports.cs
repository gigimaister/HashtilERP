using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class K_OrderPassports
    {
        public int PassportsToOrdersId { get; set; }
        public int? JobId { get; set; }
        public int? K_PassportId { get; set; }       
        public int? PassportMagashAmpunt { get; set; }
    }
}
