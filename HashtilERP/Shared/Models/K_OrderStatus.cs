using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public static class K_OrderStatus
    {
        public const string NeedToSchedule = "ממתין לתיאום";
        public const string SchedualeWasOk = "תואם";
        public const string NotAnswering = "לא עונה";
        public const string WillLetUsKnow = "ייעדכן";
        public const string WillTalkToOren = "ידבר עם אורן";
        public const string WantsMore = "רוצה להגדיל";
        public const string WantsLess = "רוצה להקטין";
        public const string WantsToPostponed = "רוצה לדחות";
        public const string WasCanceled = "בוטל";

    }
}
