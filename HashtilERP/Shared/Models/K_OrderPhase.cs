using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
   public static class K_OrderPhase
    {
        public const string StandBy = "בהמתנה";
        public const string AttachedPassports = "ירדו כמויות";
        public const string InProgress = "בהוצאה";
        public const string Finish = "מוכנה";
        public const string Canceled = "בוטלה";
        public const string WasReturned = "חזרה";
    }
}
