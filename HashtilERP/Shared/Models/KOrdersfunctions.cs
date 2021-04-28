using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public static class KOrdersfunctions
    {
        public static void SetAvgForKorder(double avg, K_Order k_Order)
        {
            var reminder = k_Order.JobPlantsNum % avg;
           
           
            if((reminder/avg) >= 0.5)
            {
               while(reminder != 0)
                {
                    k_Order.JobPlantsNum++;
                    reminder = k_Order.JobPlantsNum % avg;
                }              
            }
            else
            {
                while (reminder != 0)
                {
                    k_Order.JobPlantsNum--;
                    reminder = k_Order.JobPlantsNum % avg;
                }
            }

            k_Order.JobNumOfMagash = Convert.ToInt32(k_Order.JobPlantsNum/avg) ;
        }
    }
}
