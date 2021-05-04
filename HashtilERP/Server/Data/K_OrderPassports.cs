using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Data
{
    public partial class K_OrderPassports
    {
        [Key]
        public int PassportsToOrdersId { get; set; }
        public int? JobId { get; set; }
        public int? K_PassportId { get; set; }
        public int? PassportMagashAmpunt { get; set; }
        public string UserName { get; set; }
        public int? SelectedAVG { get; set; }
        public int? K_PassportNum { get; set; }

        public K_Passport K_Passport { get; set; }

    }
}
