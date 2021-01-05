using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class KobiPassportStatus
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public static List<KobiPassportStatus> Statuses =
        new List<KobiPassportStatus>() {
             new KobiPassportStatus(){ ID= "GrowingRoom", Text= "חדר הנבטה" },
             new KobiPassportStatus(){ ID= "Active", Text= "פעיל" },
             new KobiPassportStatus(){ ID= "Finished", Text= "נגמר" },
             new KobiPassportStatus(){ ID= "Destroyed", Text= "הושמד" },
        };

    }
}
