using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared
{
    public static class K_PassportFunctions
    {
        //set num of plants according to if limor avg is set or Sap 
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

        //return if limor avg is  and if not return string
        public static string GetKorderAvg(int? passportAvg, int? passportStartingAvg) 
        {
            var Text = "";
            if(passportAvg == -1)
            {
                Text = "ללא";
            }
            else
            {
                Text = passportAvg.ToString();
            }
            return Text;
        }

    }
}
