using HashtilERP.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Data
{
    public partial class K_Order
    {
        [Key]
        public int JobId { get; set; }
        public int? DocEntry { get; set; }
        //when entered to prepreport
        public DateTime? PrepReportEnteringDate { get; set; }

        //when enter to outgoing KOrder table
        public DateTime? K_OrderEnteringDate { get; set; }

        //when enetring direct to KOrder table 
        public DateTime? CreationDate { get; set; }

        public DateTime? MarketingDate { get; set; }
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
        public bool? IsNeedToConfirmJob { get; set; }
        public bool? IsTakeoutJobTomorrow { get; set; }
        public int? NumOfCages { get; set; }
        public string HamamaRemarks { get; set; }
        public string DeliveryRemarks { get; set; }
        public string JobStatus { get; set; }

        //props for coordination Report(Miri)
        public string FixedCoordinationRemark { get; set; }
        public string OpenCoordinationRemark { get; set; }
        public string UserName { get; set; }
        public string PassprodComments { get; set; }

        [ForeignKey("JobId")]
        public virtual List<K_OrderPassports> K_OrderPassports { get; set; }

    }

    //GET BEGIN AND END PREPREPORT DATE RANGE
    public static class KOrderAlgorithem
    {
        public static List<DateTime> GetPrepReportWeekRange(DateTime Datetime,int earlyRepo=0)
        {
            List<DateTime> prepRepo = new List<DateTime>();
            switch (Datetime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    prepRepo.Add(DateTime.Today);
                    prepRepo.Add(DateTime.Today.AddDays(6));
                    break;                    
                case DayOfWeek.Monday:
                    prepRepo.Add(DateTime.Today.AddDays(-1));
                    prepRepo.Add(DateTime.Today.AddDays(5));
                    break;
                case DayOfWeek.Tuesday:
                    prepRepo.Add(DateTime.Today.AddDays(-2));
                    prepRepo.Add(DateTime.Today.AddDays(4));
                    break;
                case DayOfWeek.Wednesday:
                    prepRepo.Add(DateTime.Today.AddDays(-3));
                    prepRepo.Add(DateTime.Today.AddDays(3));
                    break;
                case DayOfWeek.Thursday:
                    prepRepo.Add(DateTime.Today.AddDays(3));
                    prepRepo.Add(DateTime.Today.AddDays(9));
                    break;             
                case DayOfWeek.Friday:
                    prepRepo.Add(DateTime.Today.AddDays(2));
                    prepRepo.Add(DateTime.Today.AddDays(8));
                    break;
                case DayOfWeek.Saturday:
                    prepRepo.Add(DateTime.Today.AddDays(1));
                    prepRepo.Add(DateTime.Today.AddDays(7));
                    break;               
            }
            return prepRepo;
        }
    }
}
