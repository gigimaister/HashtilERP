using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HashtilERP.Data
{
    public partial class PassportAuditForm
    {
        [Key]
        public long PassportAuditFormId { get; set; }
        public int? K_PassportId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "חייב לבחור סטאטוס")]
        public string AuditStatus { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "חייב להזין תיאור")]
        public string Remark { get; set; }
    }
}
