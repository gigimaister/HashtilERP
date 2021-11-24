using HashtilERP.Shared.Models;
using HashtilERP.Shared.Models.Mobile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HashtilERP.Shared.Models
{
    public class K_Passport
    {
        [Key]
        public int K_PassportId { get; set; }

        public int? SapDocEntry { get; set; }
        public int? PassportNum { get; set; }
        public string GrowingRoom { get; set; }
        public DateTime? SowDate { get; set; } = DateTime.Today;
        public DateTime? DateEnd { get; set; } = DateTime.Today;
        public DateTime? MetzayEnteringDate { get; set; }
        public DateTime? MetzayOutGoingDate { get; set; }
        public DateTime? AVGEnteringDate { get; set; }
        public int? GrowingDays { get; set; }
        public int PassportAge => Convert.ToInt32(((TimeSpan)(DateTime.Now - SowDate)).Days);
        public string Gidul { get; set; }
        public string Zan { get; set; }
        public string GidulZan => Gidul + " " + Zan;
        public string Hamama { get; set; }
        public string Gamlon { get; set; }
        public int? MagashAmount { get; set; }
        public int? OriginalMagashAmount { get; set; }
        public int? PlantsAmount { get; set; }
        public bool? IsNeedToBeAudit { get; set; }
        public bool? IsNeedToBeChecked { get; set; } = false;
        public bool? IsSavedForCx { get; set; }
        public bool? IsLowAVG { get; set; }
        public bool? IsNeedToCut { get; set; }
        public bool? HasBeenAudited { get; set; }
        public string PassportStatus { get; set; }
        public int? PassportStatusCode { get; set; }
        public int? PassportAVG { get; set; } = -1;
        public int? PassportStartingAVG { get; set; }
        public string ItemCode { get; set; }
        public string UserName { get; set; }
        public string PassportCondition { get; set; }
        public DateTime? GrowingRoomExitDay { get; set; }
        public int CelsTray { get; set; }
        public string SaveForCxRemarks { get; set; }
        public int? AutoPlantsCalc => K_PassportFunctions.GetKPassportNumOfPlants(PassportAVG, PassportStartingAVG, MagashAmount);
        public string AutoPassportAVG => K_PassportFunctions.GetKorderAvg(PassportAVG, PassportStartingAVG);
        public double? AuditAge => (Convert.ToDouble(((TimeSpan)(DateTime.Now - SowDate)).Days) / Convert.ToDouble(GrowingDays));
        public int HamamaInt => Convert.ToInt32(Hamama);
        public int GamlonInt => Convert.ToInt32(Gamlon);

        [ForeignKey("SapDocEntry")]
        public virtual Passport Passport { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual KPassportInsertAudit KPassportInsertAudit { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual List<PassportAuditForm> PassportAuditForms { get; set; }


        [ForeignKey("K_PassportId")]
        public virtual List<UpdateK_PassportAudit> UpdateK_PassportAudit { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual List<K_PassportAuditTblVer2> k_PassportAuditTblVer2s { get; set; }

        public virtual MobileUser MobileUser { get; set; } = new MobileUser();
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
             new GrowingRoomNumber(){ ID= "5", Text= "5"},
             new GrowingRoomNumber(){ ID= "6", Text= "6"},
    };
}
public static class Status
{
    public const string GrowingRoom = "חדר הנבטה";
    public const string WaitingForOK = "ממתין לאישור";
    public const string InGreenHouse = "פרוס בחממה";
    public const string Finished = "נגמר";
    public const string Destroyed = "הושמד";
    public const string NotChecked = "לא נבדק";

}

public enum PassportStatusCode
{
  Growingroom,
  WaitingForConfirmation,
  InsideGreenHouse,
  Finish,
  Destroyed,
  StillNotChecked
}

public static class PassportAlgorithm
{
    public static K_Passport ArgsToObject(K_Passport k_Passport)
    {
        var kPassport = new K_Passport();
        kPassport = k_Passport;
        return kPassport;
    }

    public static bool IsAVGLow(K_Passport k_Passport)
    {
        var magashType = k_Passport.CelsTray;
        var passportAVG = k_Passport.PassportAVG;
        switch (magashType)
        {
            case 442:
                if (passportAVG < 400)
                {
                    return true;
                }
                break;
            case 180:
                if (passportAVG < 160)
                {
                    return true;
                }
                break;
            case 187:
                if (passportAVG < 160)
                {
                    return true;
                }
                break;
            case 308:
                if (passportAVG < 277)
                {
                    return true;
                }
                break;
            case 260:
                if (passportAVG < 232)
                {
                    return true;
                }
                break;
            case 250:
                if (passportAVG < 222)
                {
                    return true;
                }
                break;

        }
        return false;
    }
}
