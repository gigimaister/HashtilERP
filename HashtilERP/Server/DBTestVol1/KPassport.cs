﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using HashtilERP.Shared.Models.Mobile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HashtilERP.DBTestVol1
{
    public partial class KPassport
    {
        public int KPassportId { get; set; }
        public int PassportNum { get; set; }
        public string GrowingRoom { get; set; }
        public DateTime? SowDate { get; set; }
        public string Hamama { get; set; }
        public string Gamlon { get; set; }
        public int? MagashAmount { get; set; }
        public decimal? PlantsAmount { get; set; }
        public bool? IsNeedToBeAudit { get; set; }
        public bool? IsNeedToBeChecked { get; set; }
        public bool? IsSavedForCx { get; set; }
        public bool? IsLowAvg { get; set; }
        public bool? IsNeedToCut { get; set; }
        public bool? HasBeenAudited { get; set; }
        public string PassportStatus { get; set; }
        public int? PassportStatusCode { get; set; }
        public int? PassportAvg { get; set; }
        public int? OriginalMagashAmount { get; set; }
        public string ItemCode { get; set; }
        public int SapDocEntry { get; set; }
        public string UserName { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? GrowingDays { get; set; }
        public string PassportCondition { get; set; }
        public int? PassportStartingAvg { get; set; }
        public DateTime? MetzayEnteringDate { get; set; }
        public DateTime? MetzayOutGoingDate { get; set; }
        public DateTime? AvgenteringDate { get; set; }
        public DateTime? GrowingRoomExitDay { get; set; }
        public string Gidul { get; set; }
        public string Zan { get; set; }
        public int? CelsTray { get; set; }
        public string SaveForCxRemarks { get; set; }

        [NotMapped]
        public virtual MobileUser MobileUser { get; set; }
    }
}