using HashtilERP.Shared;

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

    }
}
