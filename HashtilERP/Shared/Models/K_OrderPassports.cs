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
        public string UserName { get; set; }
        public int? SelectedAVG { get; set; }
        public int? K_PassportNum { get; set; }
        public int? Platns => PassportMagashAmpunt * SelectedAVG;

        //For edit passport for order amount
        public int? MagashToCompare {
            get {return PassportMagashAmpunt;  }
            set { MagashMiddle = value; }
        }
        public int? MagashMiddle { get; set; } = 0;
        public K_Passport K_Passport { get; set; }
    }
}
