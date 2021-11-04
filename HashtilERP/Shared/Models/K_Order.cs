using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HashtilERP.Shared.Models.Drivers;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class K_Order
    {
        public int JobId { get; set; }
        public int? DocEntry { get; set; }
        public DateTime? PrepReportEnteringDate { get; set; }
        public DateTime? KOrderEnteringDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? MarketingDate { get; set; }
        public string SaleNum { get; set; } = "";
        public string CxName { get; set; } = "";
        public string CardCode { get; set; } = "";
        public string Gidul { get; set; } = "";
        public string Zan { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public int? JobPlantsNum { get; set; } = 0;
        public int? JobNumOfMagash { get; set; } = 0;
        public int? JobPlansAvarage { get; set; } = 0;
        public int? NumOfCarton { get; set; } = 0;
        public bool? IsCageSmall { get; set; } = false;
        public bool? IsCxComeToPickUp { get; set; } = false;
        public bool? IsPrinted { get; set; } = false;
        public bool? IsNeedToConfirmJob { get; set; } = false;
        public bool? IsTakeoutJobTomorrow { get; set; } = false;
        public bool? IsDeliveryNote { get; set; } = false;
        public bool? IsJobCancel { get; set; } = false;
        public bool? IsWasChangedAfterDeliveryReport { get; set; } = false;
        public bool? WasEditAfterAttachedPassports { get; set; } = false;
        public int? NumOfCages { get; set; } = 0;
        public int? NumOfBarTenderStickers { get; set; } = 0;

        public string HamamaRemarks { get; set; } = "";
        public string DeliveryRemarks { get; set; } = "";
        public string JobStatus { get; set; } = "";
        public string FixedCoordinationRemark { get; set; } = "";
        public string OpenCoordinationRemark { get; set; } = "";
        public string UserName { get; set; } = "";
        public string PassprodComments { get; set; } = "";
        public string HebrewDayName => KOrderAlgorithemShared.GetHebrewDayOfTheWeek(MarketingDate);
        public DateTime MarketingDateDate => Convert.ToDateTime(MarketingDate);                

        [ForeignKey("JobId")]
        public virtual List<K_OrderPassports> K_OrderPassports { get; set; }

        [ForeignKey("ItemCode")]
        public virtual Oitm Oitm { get; set; }

        public OCRD Ocrd { get; set; }

        [ForeignKey("JobId")]
        public List<K_OrderRemark> k_OrderRemarks { get; set; }

        [ForeignKey("JobId")]
        public List<K_OrderAuditTable> k_OrderAuditTables { get; set; }

        [ForeignKey("JobId")]
        public List<DriversToOrder> DriversToOrders { get; set; }

        public string AnotherFixedRemarks => KOrdersfunctions.BoolRemarksToString(IsCageSmall, IsCxComeToPickUp, IsNeedToConfirmJob);

        public string GetIsJobDeliveryOut => KOrdersfunctions.GetIsKorderDeliveryBillOut(IsDeliveryNote);
        public string DriverName => GetDriverName(DriversToOrders);
        public string GetDriverName(List<DriversToOrder> driversToOrders)
        {
            var name = "טרם נקבע";
            if (driversToOrders == null) { return name; }
            if (driversToOrders.Count == 1) { name = $"{driversToOrders.Select(x => x.FullName).FirstOrDefault()}"; }
            else if (driversToOrders.Count > 1) { name = "מפוצל"; }
            return name;
        }


    }


}
