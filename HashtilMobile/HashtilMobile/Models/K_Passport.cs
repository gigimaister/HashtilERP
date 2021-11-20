using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HashtilMobile.Models
{
    public class K_Passport
    {
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
    }
}
