using HashtilERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HashtilERP.Shared.Models
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

        //check if Trey is wide or narrow 
        public static bool IsTreyWide(K_Passport k_Passport)
        {
            if(k_Passport.CelsTray == 442 || k_Passport.CelsTray == 187 || k_Passport.CelsTray == 250)
            {
                return true;
            }
            return false;
        }

        public static List<decimal?> CxPlantsOrder(K_Passport k_Passport, string cxName)
        {
            var plantsMagash = new List<decimal?>();
            try
            {
               

                var cx = k_Passport.Passport.Passprods.Where(x => x.UCustName == cxName).FirstOrDefault();
                decimal? temp;
                int startAvg;
                if (k_Passport.Passport.UQuanOrdP > 5555554)
                {
                    temp = k_Passport.Passport.UQuanProd * 1000;
                    startAvg = Convert.ToInt32((k_Passport.Passport.UQuanProd * 1000) / k_Passport.Passport.UTraySow);
                }
                else
                {
                    temp = (cx.UQuantity * 1000);
                    startAvg = Convert.ToInt32((k_Passport.Passport.UQuanOrdP * 1000) / k_Passport.Passport.UTraySow);
                }

                var magashPercx = temp / startAvg;
                plantsMagash.Add(temp);
                plantsMagash.Add(magashPercx);

                
            }
           
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return plantsMagash;
        }

    }
}
