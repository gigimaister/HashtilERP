﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public  class Passprod
    {
        [Key]
        public int DocEntry { get; set; }

        [Key]
        public int LineId { get; set; }
        public int? VisOrder { get; set; }
        public string Object { get; set; }
        public int? LogInst { get; set; }
        public string USaleLine { get; set; }
        public string UProdEntr { get; set; }
        public string UProdNum { get; set; }
        public string USaleNum { get; set; }
        public string UCustName { get; set; }
        public decimal? UQuantity { get; set; }
        public DateTime? UDateSupp { get; set; }
        public string UComments { get; set; }
        public decimal? UPqtyOpn { get; set; }
        public decimal? UQuaOpen { get; set; }
        public string UCustSeed { get; set; }

        [ForeignKey("USaleNum")]
        public virtual ORDR ORDR { get; set; }


    }
}
