using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HashtilERP.Shared.Models
{
    public  class Passport
    {
        [Key]
        public int DocEntry { get; set; }
        public int? DocNum { get; set; }
        public int? Period { get; set; }
        public short? Instance { get; set; }
        public int? Series { get; set; }
        public string Handwrtten { get; set; }
        public string Canceled { get; set; }
        public string Object { get; set; }
        public int? LogInst { get; set; }
        public int? UserSign { get; set; }
        public string Transfered { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public short? CreateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public short? UpdateTime { get; set; }
        public string DataSource { get; set; }
        public string RequestStatus { get; set; }
        public string Creator { get; set; }
        public string Remark { get; set; }
        public string UPassNum { get; set; }
        public string UPassSirs { get; set; }
        public string UPassStat { get; set; }
        public DateTime? UDateCrea { get; set; }
        public string UItemCode { get; set; }
        public string UItemDesc { get; set; }
        public short? UTimeSow { get; set; }
        public DateTime? UDateSow { get; set; }
        public DateTime? UDateEnd { get; set; }
        public decimal? UQuanProd { get; set; }
        public decimal? UQuanOrdP { get; set; }
        public decimal? UQuanOrdI { get; set; }
        public decimal? UQuanSpar { get; set; }
        public decimal? UQuanElim { get; set; }
        public decimal? UQuanInvt { get; set; }
        public string UBatchNum { get; set; }
        public string USeedComp { get; set; }
        public decimal? UGermPrct { get; set; }
        public DateTime? UDateMxGr { get; set; }
        public decimal? UTrayAvrg { get; set; }
        public string USowrName { get; set; }
        public decimal? UTraySow { get; set; }
        public string UGrnHos { get; set; }
        public string UGamlon { get; set; }
        public decimal? UNotGrow { get; set; }
        public decimal? URecTray { get; set; }
        public decimal? UZraCoef { get; set; }
        public decimal? UPreSort { get; set; }
        public decimal? UBplntTr { get; set; }
        public decimal? USplntTr { get; set; }
        public decimal? UPostSort { get; set; }
        public decimal? UTrLoss { get; set; }
        public decimal? ULossPct { get; set; }
        public decimal? UBplntPct { get; set; }
        public decimal? USplntPct { get; set; }
        public string UNotes { get; set; }
        public decimal? UTrayGre { get; set; }
        public string UMachine { get; set; }
        public string UPeat { get; set; }
        public string UGroomTemp { get; set; }
        public string USowTemp { get; set; }
        public decimal? UBatchavg { get; set; }
        public decimal? UItemAvg { get; set; }
        public string UNights { get; set; }
        public string UNote1 { get; set; }
        public string UNote2 { get; set; }
        public string UNote3 { get; set; }
        public string UNote4 { get; set; }
        public string UZanZl { get; set; }
        public string UBatchCheck { get; set; }

        [ForeignKey("UItemCode")]
        public virtual Oitm Oitm { get; set; }

        [ForeignKey("DocEntry")]
        public virtual List<Passprod> Passprods { get; set; }
    }
}
