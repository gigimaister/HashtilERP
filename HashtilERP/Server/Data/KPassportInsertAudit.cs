﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HashtilERP.Data
{
    public partial class KPassportInsertAudit
    {
        [Key]
        public int KPassportId { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
        public int? PassportNum { get; set; }
    }
}