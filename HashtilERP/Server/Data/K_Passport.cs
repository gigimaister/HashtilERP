﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HashtilERP.Data
{
    
    public partial class K_Passport
    {
        [Key]
        public int K_PassportId { get; set; }
        public int? SapDocEntry { get; set; }
        public int? PassportNum { get; set; }
        public string GrowingRoom { get; set; }
        public DateTime? SowDate { get; set; }
        public DateTime? MetzayEnteringDate { get; set; }
        public DateTime? MetzayOutGoingDate { get; set; }
        public DateTime? AVGEnteringDate { get; set; }
        public string Hamama { get; set; }
        public string Gamlon { get; set; }
        public int? MagashAmount { get; set; }
        public decimal? PlantsAmount { get; set; }
        public bool? IsNeedToBeAudit { get; set; }
        public bool? IsNeedToBeChecked { get; set; }
        public bool? IsSavedForCx { get; set; }
        public bool? IsLowAVG { get; set; }
        public bool? IsNeedToCut { get; set; }
        public string PassportStatus { get; set; }
        public int? PassportStatusCode { get; set; }
        public int? PassportAvg { get; set; } = -1;
        public int? PassportStartingAVG { get; set; }
        public int? OriginalMagashAmount { get; set; }
        public string ItemCode { get; set; }
        
        public string UserName { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? GrowingDays { get; set; }
        public string Gidul { get; set; }
        public string Zan { get; set; }
        public string PassportCondition { get; set; }
        public DateTime? GrowingRoomExitDay { get; set; }
        public int CelsTray { get; set; }
        public string SaveForCxRemarks { get; set; }

        [ForeignKey("SapDocEntry")]
        public virtual Passport Passport { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual KPassportInsertAudit  KPassportInsertAudit { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual List<PassportAuditForm>  PassportAuditForms { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual List<UpdateK_PassportAudit> UpdateK_PassportAudit { get; set; }

        [ForeignKey("K_PassportId")]
        public virtual List<K_PassportAuditTblVer2> k_PassportAuditTblVer2s { get; set; }

    }
}