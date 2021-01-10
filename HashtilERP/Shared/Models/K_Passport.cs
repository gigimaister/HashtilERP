using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HashtilERP.Shared.Models
{
    public class K_Passport
    {
        public long K_PassportId { get; set; } 

       
        public int PassportNum { get; set; }
        public string GrowingRoom { get; set; }
        public DateTime? SowDate { get; set; } = DateTime.Today;
        public int PassportAge => Convert.ToInt32(((TimeSpan)(DateTime.Now - SowDate)).Days);
        public string Hamama { get; set; }
        public string Gamlon { get; set; }
        public int? MagashAmount { get; set; }
        public int? OriginalMagashAmount { get; set; }
        public int? PlantsAmount { get; set; }
        public bool? IsNeedToBeAudit { get; set; }
        public bool? IsNeedToBeChecked { get; set; }
        public string PassportStatus { get; set; }
        public int? PassportAVG { get; set; }
       



    }

   
}

public class GrowingRoomNumber
{
    public string ID { get; set; }
    public string Text { get; set; }
    public static List<GrowingRoomNumber> Statuses =
    new List<GrowingRoomNumber>() {
             new GrowingRoomNumber(){ ID= "1", Text= "1"},
             new GrowingRoomNumber(){ ID= "2", Text= "2"},
             new GrowingRoomNumber(){ ID= "3", Text= "3"},
             new GrowingRoomNumber(){ ID= "4", Text= "4"},
    };
}
public static class Status
{
    public const string GrowingRoom = "חדר הנבטה";
    public const string WaitingForOK = "ממתין לאישור";
    public const string InGreenHouse = "פרוס בחממה";
    public const string Finished = "נגמר";
    public const string Destroyed = "הושמד";

}
