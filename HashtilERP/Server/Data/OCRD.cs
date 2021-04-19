using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Data
{
    public partial class OCRD
    {
        [Key]
        public string CardCode { get; set; }

        
        public string CardName { get; set; }
    }
}
