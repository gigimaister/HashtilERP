using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public class OCRD
    {
        [Key]
        public string CardCode { get; set; }
        public string CardName { get; set; }
       
    }
}
