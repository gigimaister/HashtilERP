using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class KobiPassportModel
    {
        public long Id { get; set; }
        public string PassportNum { get; set; }
        public string GrowingRoom { get; set; }
        public int? Hamama { get; set; }
        public int? Gamlon { get; set; }
        public DateTime SowDate { get; set; }
        public DateTime MarketDate { get; set; }
        public int MagashAmount { get; set; }
        public int PlantsAmount { get; set; }
        public int? Avarage { get; set; }
        public string Status { get; set; }
        public bool IsNeedToBeAudit { get; set; }
        public bool IsNeedToBeChecked { get; set; }
        public string Gidul { get; set; }
        public string Zan { get; set; }
        public int GrowingDaysToMarket { get; set; }
        
    }
}
