using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class PassportAuditForm
    {
        [Key]
        public long PassportAuditFormId { get; set; }
        public int K_PassportId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "חייב לבחור סטאטוס")]     
        public string AuditStatus { get; set; }

        [Required(ErrorMessage ="חייב לרשום הערה")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "מינ הזנה-2 תווים")]
        public string Remark { get; set; }
    }

    public class AuditFormStatus
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public static List<AuditFormStatus> Statuses =
        new List<AuditFormStatus>() {
             new AuditFormStatus(){ ID= "1", Text= "תקין"},
             new AuditFormStatus(){ ID= "2", Text= "בעיית נביטה"},
             new AuditFormStatus(){ ID= "3", Text= "חולה"},
             new AuditFormStatus(){ ID= "4", Text= "שרוף"},
             new AuditFormStatus(){ ID= "4", Text= "אחר"}
        };
    }
}
