using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class ORDR
    {
        [Key]
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; }

        [ForeignKey("CardCode")]
        public virtual OCRD OCRD { get; set; }
    }
}
