using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Data
{
    public partial class MetzayForOren : K_Passport
    {
        public string Cx { get; set; }
        public int TotalPLants { get; set; }
        public int TotalOrder { get; set; }
        public decimal? CxPlantsOrder { get; set; }
        public int SumOfCxsPlants { get; set; }
    }
}
