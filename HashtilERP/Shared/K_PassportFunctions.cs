using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared
{
    public static class K_PassportFunctions
    {
        public static int? GetKPassportNumOfPlants(int? kpassAvg, int? passportStartingAvg, int? magashAmount )
        {
            int? plants = 0;
            if(kpassAvg == -1)
            {
                plants = magashAmount * passportStartingAvg;
            }
            else
            {
                plants = magashAmount * kpassAvg;
            }

            return plants;
        }

    }
}
