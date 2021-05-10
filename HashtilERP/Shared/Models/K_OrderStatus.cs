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

        //for who cancel Korder
        public const string WhoHashtil = "המשתלה";

        public const string WhoCx = "הלקוח";



        //not active at this moment
        public static Dictionary<int, string> KOrdertPhase()
        {
            var dict = new Dictionary<int, string>
            {
                { 0,"בהמתנה"},
                { 1,"בהוצאה"},
                { 2,"מוכנה"},
                { 3,"בוטלה"},
                { 4,"חזרה"},
                { 5,""},
                { 6,""},
            };
            return dict;
        }

        public static List<string> GetKOrderStatus()
        {
            var korderstatus = new List<string>
            {
                NeedToSchedule,
                SchedualeWasOk,
                NotAnswering,
                WillLetUsKnow,
                WillTalkToOren,
                WantsMore,
                WantsLess,
                WantsToPostponed,
                WasCanceled
            };

            return korderstatus;
        }
        public static List<string> GetWhoCancelKorderList()
        {
            var korderWhocancelList = new List<string>
            {
                WhoHashtil,
                WhoCx
            };

            return korderWhocancelList;
        }

    }
}
