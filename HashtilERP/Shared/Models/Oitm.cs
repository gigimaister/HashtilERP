﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class Oitm
    {
        [Key]
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string FrgnName { get; set; }
        public short? ItmsGrpCod { get; set; }
        public short? CstGrpCode { get; set; }
        public string VatGourpSa { get; set; }
        public string CodeBars { get; set; }
        public string Vatliable { get; set; }
        public string PrchseItem { get; set; }
        public string SellItem { get; set; }
        public string InvntItem { get; set; }
        public decimal? OnHand { get; set; }
        public decimal? IsCommited { get; set; }
        public decimal? OnOrder { get; set; }
        public string IncomeAcct { get; set; }
        public string ExmptIncom { get; set; }
        public decimal? MaxLevel { get; set; }
        public string DfltWh { get; set; }
        public string CardCode { get; set; }
        public string SuppCatNum { get; set; }
        public string BuyUnitMsr { get; set; }
        public decimal? NumInBuy { get; set; }
        public decimal? ReorderQty { get; set; }
        public decimal? MinLevel { get; set; }
        public decimal? LstEvlPric { get; set; }
        public DateTime? LstEvlDate { get; set; }
        public decimal? CustomPer { get; set; }
        public string Canceled { get; set; }
        public int? MnufctTime { get; set; }
        public string WholSlsTax { get; set; }
        public string RetilrTax { get; set; }
        public decimal? SpcialDisc { get; set; }
        public short? DscountCod { get; set; }
        public string TrackSales { get; set; }
        public string SalUnitMsr { get; set; }
        public decimal? NumInSale { get; set; }
        public decimal? Consig { get; set; }
        public int? QueryGroup { get; set; }
        public decimal? Counted { get; set; }
        public decimal? OpenBlnc { get; set; }
        public string EvalSystem { get; set; }
        public short? UserSign { get; set; }
        public string Free { get; set; }
        public string PicturName { get; set; }
        public string Transfered { get; set; }
        public string BlncTrnsfr { get; set; }
        public string UserText { get; set; }
        public string SerialNum { get; set; }
        public decimal? CommisPcnt { get; set; }
        public decimal? CommisSum { get; set; }
        public short? CommisGrp { get; set; }
        public string TreeType { get; set; }
        public decimal? TreeQty { get; set; }
        public decimal? LastPurPrc { get; set; }
        public string LastPurCur { get; set; }
        public DateTime? LastPurDat { get; set; }
        public string ExitCur { get; set; }
        public decimal? ExitPrice { get; set; }
        public string ExitWh { get; set; }
        public string AssetItem { get; set; }
        public string WasCounted { get; set; }
        public string ManSerNum { get; set; }
        public decimal? Sheight1 { get; set; }
        public short? Shght1Unit { get; set; }
        public decimal? Sheight2 { get; set; }
        public short? Shght2Unit { get; set; }
        public decimal? Swidth1 { get; set; }
        public short? Swdth1Unit { get; set; }
        public decimal? Swidth2 { get; set; }
        public short? Swdth2Unit { get; set; }
        public decimal? Slength1 { get; set; }
        public short? Slen1Unit { get; set; }
        public decimal? Slength2 { get; set; }
        public short? Slen2Unit { get; set; }
        public decimal? Svolume { get; set; }
        public short? SvolUnit { get; set; }
        public decimal? Sweight1 { get; set; }
        public short? Swght1Unit { get; set; }
        public decimal? Sweight2 { get; set; }
        public short? Swght2Unit { get; set; }
        public decimal? Bheight1 { get; set; }
        public short? Bhght1Unit { get; set; }
        public decimal? Bheight2 { get; set; }
        public short? Bhght2Unit { get; set; }
        public decimal? Bwidth1 { get; set; }
        public short? Bwdth1Unit { get; set; }
        public decimal? Bwidth2 { get; set; }
        public short? Bwdth2Unit { get; set; }
        public decimal? Blength1 { get; set; }
        public short? Blen1Unit { get; set; }
        public decimal? Blength2 { get; set; }
        public short? Blen2Unit { get; set; }
        public decimal? Bvolume { get; set; }
        public short? BvolUnit { get; set; }
        public decimal? Bweight1 { get; set; }
        public short? Bwght1Unit { get; set; }
        public decimal? Bweight2 { get; set; }
        public short? Bwght2Unit { get; set; }
        public string FixCurrCms { get; set; }
        public short? FirmCode { get; set; }
        public DateTime? LstSalDate { get; set; }
        public string QryGroup1 { get; set; }
        public string QryGroup2 { get; set; }
        public string QryGroup3 { get; set; }
        public string QryGroup4 { get; set; }
        public string QryGroup5 { get; set; }
        public string QryGroup6 { get; set; }
        public string QryGroup7 { get; set; }
        public string QryGroup8 { get; set; }
        public string QryGroup9 { get; set; }
        public string QryGroup10 { get; set; }
        public string QryGroup11 { get; set; }
        public string QryGroup12 { get; set; }
        public string QryGroup13 { get; set; }
        public string QryGroup14 { get; set; }
        public string QryGroup15 { get; set; }
        public string QryGroup16 { get; set; }
        public string QryGroup17 { get; set; }
        public string QryGroup18 { get; set; }
        public string QryGroup19 { get; set; }
        public string QryGroup20 { get; set; }
        public string QryGroup21 { get; set; }
        public string QryGroup22 { get; set; }
        public string QryGroup23 { get; set; }
        public string QryGroup24 { get; set; }
        public string QryGroup25 { get; set; }
        public string QryGroup26 { get; set; }
        public string QryGroup27 { get; set; }
        public string QryGroup28 { get; set; }
        public string QryGroup29 { get; set; }
        public string QryGroup30 { get; set; }
        public string QryGroup31 { get; set; }
        public string QryGroup32 { get; set; }
        public string QryGroup33 { get; set; }
        public string QryGroup34 { get; set; }
        public string QryGroup35 { get; set; }
        public string QryGroup36 { get; set; }
        public string QryGroup37 { get; set; }
        public string QryGroup38 { get; set; }
        public string QryGroup39 { get; set; }
        public string QryGroup40 { get; set; }
        public string QryGroup41 { get; set; }
        public string QryGroup42 { get; set; }
        public string QryGroup43 { get; set; }
        public string QryGroup44 { get; set; }
        public string QryGroup45 { get; set; }
        public string QryGroup46 { get; set; }
        public string QryGroup47 { get; set; }
        public string QryGroup48 { get; set; }
        public string QryGroup49 { get; set; }
        public string QryGroup50 { get; set; }
        public string QryGroup51 { get; set; }
        public string QryGroup52 { get; set; }
        public string QryGroup53 { get; set; }
        public string QryGroup54 { get; set; }
        public string QryGroup55 { get; set; }
        public string QryGroup56 { get; set; }
        public string QryGroup57 { get; set; }
        public string QryGroup58 { get; set; }
        public string QryGroup59 { get; set; }
        public string QryGroup60 { get; set; }
        public string QryGroup61 { get; set; }
        public string QryGroup62 { get; set; }
        public string QryGroup63 { get; set; }
        public string QryGroup64 { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ExportCode { get; set; }
        public decimal? SalFactor1 { get; set; }
        public decimal? SalFactor2 { get; set; }
        public decimal? SalFactor3 { get; set; }
        public decimal? SalFactor4 { get; set; }
        public decimal? PurFactor1 { get; set; }
        public decimal? PurFactor2 { get; set; }
        public decimal? PurFactor3 { get; set; }
        public decimal? PurFactor4 { get; set; }
        public string SalFormula { get; set; }
        public string PurFormula { get; set; }
        public string VatGroupPu { get; set; }
        public decimal? AvgPrice { get; set; }
        public string PurPackMsr { get; set; }
        public decimal? PurPackUn { get; set; }
        public string SalPackMsr { get; set; }
        public decimal? SalPackUn { get; set; }
        public short? Scncounter { get; set; }
        public string ManBtchNum { get; set; }
        public string ManOutOnly { get; set; }
        public string DataSource { get; set; }
        public string ValidFor { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string FrozenFor { get; set; }
        public DateTime? FrozenFrom { get; set; }
        public DateTime? FrozenTo { get; set; }
        public string BlockOut { get; set; }
        public string ValidComm { get; set; }
        public string FrozenComm { get; set; }
        public int? LogInstanc { get; set; }
        public string ObjType { get; set; }
        public string Sww { get; set; }
        public string Deleted { get; set; }
        public int? DocEntry { get; set; }
        public string ExpensAcct { get; set; }
        public string FrgnInAcct { get; set; }
        public short? ShipType { get; set; }
        public string Glmethod { get; set; }
        public string EcinAcct { get; set; }
        public string FrgnExpAcc { get; set; }
        public string EcexpAcc { get; set; }
        public string TaxType { get; set; }
        public string ByWh { get; set; }
        public string Wtliable { get; set; }
        public string ItemType { get; set; }
        public string WarrntTmpl { get; set; }
        public string BaseUnit { get; set; }
        public string CountryOrg { get; set; }
        public decimal? StockValue { get; set; }
        public string Phantom { get; set; }
        public string IssueMthd { get; set; }
        public string Free1 { get; set; }
        public decimal? PricingPrc { get; set; }
        public string MngMethod { get; set; }
        public decimal? ReorderPnt { get; set; }
        public string InvntryUom { get; set; }
        public string PlaningSys { get; set; }
        public string PrcrmntMtd { get; set; }
        public short? OrdrIntrvl { get; set; }
        public decimal? OrdrMulti { get; set; }
        public decimal? MinOrdrQty { get; set; }
        public int? LeadTime { get; set; }
        public string IndirctTax { get; set; }
        public string TaxCodeAr { get; set; }
        public string TaxCodeAp { get; set; }
        public int? OsvcCode { get; set; }
        public int? IsvcCode { get; set; }
        public int? ServiceGrp { get; set; }
        public int? Ncmcode { get; set; }
        public string MatType { get; set; }
        public int? MatGrp { get; set; }
        public string ProductSrc { get; set; }
        public int? ServiceCtg { get; set; }
        public string ItemClass { get; set; }
        public string Excisable { get; set; }
        public int? ChapterId { get; set; }
        public string NotifyAsn { get; set; }
        public string ProAssNum { get; set; }
        public decimal? AssblValue { get; set; }
        public int? Dnfentry { get; set; }
        public short? UserSign2 { get; set; }
        public string Spec { get; set; }
        public string TaxCtg { get; set; }
        public short? Series { get; set; }
        public int? Number { get; set; }
        public int? FuelCode { get; set; }
        public string BeverTblC { get; set; }
        public string BeverGrpC { get; set; }
        public int? BeverTm { get; set; }
        public string Attachment { get; set; }
        public int? AtcEntry { get; set; }
        public int? ToleranDay { get; set; }
        public int? UgpEntry { get; set; }
        public int? PuoMentry { get; set; }
        public int? SuoMentry { get; set; }
        public int? IuoMentry { get; set; }
        public short? IssuePriBy { get; set; }
        public string AssetClass { get; set; }
        public string AssetGroup { get; set; }
        public string InventryNo { get; set; }
        public int? Technician { get; set; }
        public int? Employee { get; set; }
        public int? Location { get; set; }
        public string StatAsset { get; set; }
        public string Cession { get; set; }
        public string DeacAftUl { get; set; }
        public string AsstStatus { get; set; }
        public DateTime? CapDate { get; set; }
        public DateTime? AcqDate { get; set; }
        public DateTime? RetDate { get; set; }
        public string GlpickMeth { get; set; }
        public string NoDiscount { get; set; }
        public string MgrByQty { get; set; }
        public string AssetRmk1 { get; set; }
        public string AssetRmk2 { get; set; }
        public decimal? AssetAmnt1 { get; set; }
        public decimal? AssetAmnt2 { get; set; }
        public string DeprGroup { get; set; }
        public string AssetSerNo { get; set; }
        public string CntUnitMsr { get; set; }
        public decimal? NumInCnt { get; set; }
        public int? InuoMentry { get; set; }
        public string OneBoneRec { get; set; }
        public string RuleCode { get; set; }
        public string ScsCode { get; set; }
        public string SpProdType { get; set; }
        public decimal? Iweight1 { get; set; }
        public short? Iwght1Unit { get; set; }
        public decimal? Iweight2 { get; set; }
        public short? Iwght2Unit { get; set; }
        public string CompoWh { get; set; }
        public decimal? UZraCoef { get; set; }
        public decimal? UCelsTray { get; set; }
        public DateTime? USeedTillDate { get; set; }
        public string UUpdater { get; set; }
        public string UBsyProp1 { get; set; }
        public string UBsyProp2 { get; set; }
        public decimal? UBsyShipp { get; set; }
        public string UBsyShipId { get; set; }
        public string UBsyInVis { get; set; }
        public string UProp { get; set; }
        public string UBsyImage1 { get; set; }
        public string UBsyAlt1 { get; set; }
        public string UBsyImage2 { get; set; }
        public string UBsyAlt2 { get; set; }
        public string UBsyImage3 { get; set; }
        public string UBsyAlt3 { get; set; }
        public string UBsyImage4 { get; set; }
        public string UBsyAlt4 { get; set; }
        public string UBsyImage5 { get; set; }
        public string UBsyAlt5 { get; set; }
        public string UBsyImage6 { get; set; }
        public string UBsyAlt6 { get; set; }
        public string UBsyImage7 { get; set; }
        public string UBsyAlt7 { get; set; }
        public string UBsyImage8 { get; set; }
        public string UBsyAlt8 { get; set; }
        public string UBsyImage9 { get; set; }
        public string UBsyAlt9 { get; set; }
        public string UBsyImage10 { get; set; }
        public string UBsyAlt10 { get; set; }
        public string UBsyImage11 { get; set; }
        public string UBsyAlt11 { get; set; }
        public string UBsyImage12 { get; set; }
        public string UBsyAlt12 { get; set; }
        public string UBsyFirmLink { get; set; }
        public string UBsyAltProducts { get; set; }
        public string UBsyLnkProducts { get; set; }
        public string UBsyPage { get; set; }
        public short? UBsyMpoints { get; set; }
        public string UBsySrch { get; set; }
        public string UAltPath { get; set; }
        public string UHebGidul { get; set; }
        public string UHebZan { get; set; }
    }
}