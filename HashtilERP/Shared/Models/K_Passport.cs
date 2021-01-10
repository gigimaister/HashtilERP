using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HashtilERP.Shared.Models
{
    public class K_Passport
    {
        public long K_PassportId { get; set; } 

       
        public int PassportNum { get; set; }
        public int? GrowingRoom { get; set; }
        public DateTime? SowDate { get; set; } = DateTime.Today;
        public int PassportAge => Convert.ToInt32(((TimeSpan)(DateTime.Now - SowDate)).Days);
        public string Hamama { get; set; }
        public string Gamlon { get; set; }
        public int? MagashAmount { get; set; }
        public int? PlantsAmount { get; set; }
        public bool? IsNeedToBeAudit { get; set; }
        public bool? IsNeedToBeChecked { get; set; }
        public string PassportStatus { get; set; }
        public int? PassportAVG { get; set; }
       



    }

   
}
public static class Status
{
    public const string GrowingRoom = "חדר הנבטה";
    public const string WaitingForOK = "ממתין לאישור";
    public const string InGreenHouse = "פרוס בחממה";
    public const string Finished = "נגמר";
    public const string Destroyed = "הושמד";

}
