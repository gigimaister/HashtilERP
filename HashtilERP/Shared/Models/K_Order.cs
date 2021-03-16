using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class K_Order
    {
        public int JobId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime MarketingDate { get; set; }
        public string SaleNum { get; set; }
        public string CxName { get; set; }
        public string CardCode { get; set; }
        public string Gidul { get; set; }
        public string Zan { get; set; }
        public string ItemCode { get; set; }
        public int? JobPlantsNum { get; set; }
        public int? JobNumOfMagash { get; set; }

        //Maybe not in DB?
        public int? JobPlansAvarage { get; set; }

        //Maybe not in DB?
        public int? NumOfCarton { get; set; }

        public bool? IsCageSmall { get; set; }
        public bool? IsCxComeToPickUp { get; set; }
        public bool? IsPrinted { get; set; }
        public int? NumOfCages { get; set; }
        public string HamamaRemarks { get; set; }
        public string DeliveryRemarks { get; set; }
        public string JobStatus { get; set; }

        //props for coordination Report(Miri)
        public string FixedCoordinationRemark { get; set; }
        public string OpenCoordinationRemark { get; set; }

    }
}
