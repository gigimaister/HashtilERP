﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HashtilERP.DBTestVol1
{
    public partial class Passprod
    {
        public int DocEntry { get; set; }
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

    }
}