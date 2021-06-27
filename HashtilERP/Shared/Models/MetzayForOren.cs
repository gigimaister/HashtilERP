

namespace HashtilERP.Shared.Models
{
    public class MetzayForOren : K_Passport
    {
        public string Cx { get; set; }
        public int TotalPLants { get; set; }
        public int TotalOrder  { get; set; }
        public decimal? CxPlantsOrder { get; set; }
        public int SumOfCxsPlants { get; set; }
    }

    
}
