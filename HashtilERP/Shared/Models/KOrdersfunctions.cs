using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    public static class KOrdersfunctions
    {
        //AVG for KOrder
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

        //Pick Passport Phase if magash, plants and avg was set
        public static bool IsKOrderCanPickPassIntAmountSet(K_Order k_Order)
        {
            if(k_Order.JobNumOfMagash > 0 && k_Order.JobPlansAvarage > 0 && k_Order.JobPlantsNum > 0)
            {
                return true;
            }

            return false;
        }

        //Pick Passport Phase if gidul && zan was set
        public static bool IsKOrderPickPassGidulZanSet(K_Order k_Order)
        {
            if(!String.IsNullOrEmpty(k_Order.Gidul) && !String.IsNullOrEmpty(k_Order.Zan))
            {
                return true;
            }
            return false;
        }

        //Check if list<T> is empty
        public static bool IsEmptyList<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return true;
            }

            return list.FirstOrDefault() != null;
        }

        //Return KPassport Counter/Sap AVG
        public static int? GetKpassportAvg(K_Passport k_Passport)
        {
           if(k_Passport.PassportAVG == -1)
            {
                return k_Passport.PassportStartingAVG;
            }
            return k_Passport.PassportAVG;
        }

        //Check if Obj exists in List<T>
        public static bool IsObjInsideList<T>(List<T> ts, T obj)
        {
            if (ts.Contains(obj))
            {
                return true;
            }
            return false;
        }

        //Check if passport Empty(==0)
        public static bool IsPassportEmpty(K_Passport k_Passport)
        {
            if(k_Passport.MagashAmount == 0)
            {
                return true;
            }
            return false;
        }

        //Sum Magash Amount Inside KOrderPasports List
        public static int? GetTotalMagashPassportList(List<K_OrderPassports> k_OrderPassports, int i = 0)
        {
            int? totalMagash;
            if (i == 1)
            {
                totalMagash = k_OrderPassports.Sum(x => x.Platns);
            }
            else
            {
                totalMagash = k_OrderPassports.Sum(x => x.PassportMagashAmpunt);
            }

            return totalMagash;
        }

        //Return KOrder.Magash - List.TotalMagash
        public static int? GetKorderMinusTotalMagashList(K_Order k_Order, List<K_OrderPassports> k_OrderPassports)
        {
            var difference = (k_Order.JobNumOfMagash - GetTotalMagashPassportList(k_OrderPassports));

            return difference;
        }

        //Check if Kpassport inside List<korderPassports>
        public static bool CheckIfKpassInsideKordPassports(List<K_OrderPassports> k_OrderPassports, K_Passport k_Passport)
        {
            foreach(var kordPass in k_OrderPassports)
            {
                if(kordPass.K_PassportId == k_Passport.K_PassportId)
                {                   
                    return true;                    
                }
            }
            return false;
        }

        //Get is small cage and cx pick up on fixed remark with check box
        public static string BoolRemarksToString(bool? isCagesmall, bool? isCometopickup, bool? isneedtoConfirmJob)
        {
            string Text = "";

            if(isCagesmall == true && isCometopickup == true && (isneedtoConfirmJob == null || isneedtoConfirmJob == false)) 
            {
                Text =  "כלובים קטנים ומגיע לקחת"; 
            }
            else if (isCagesmall == true && (isCometopickup == false || isCometopickup == null) && (isneedtoConfirmJob == null || isneedtoConfirmJob==false))
            {
                Text = "כלובים קטנים";
            }
            else if (isCometopickup == true && (isCagesmall == false  || isCagesmall == null) && (isneedtoConfirmJob == null || isneedtoConfirmJob == false))
            {
                Text = "מגיע לקחת";
            }
            else if (isneedtoConfirmJob == true)
            {
                Text = "לוודא הוצאה";
            }
            else
            {
                Text = "רגיל";
            }
            return Text;
        }

        //set is delivery in fix remark  with check box
        public static string GetIsKorderDeliveryBillOut(bool? isDeliveryout)
        {
            string Text = "";
            if (isDeliveryout == true)
            {
                Text = "יצאה";
            }
            return Text;
        }

        //Get Num Of Bartender Stickers to print
        public static int? GetNumOfBarTenderStickers(K_Order order)
        {
            int? stickNum = 0;
            var stickerforPassport = 1;
            var jobnumMagash = order.JobNumOfMagash;
            var orderPasslist = order.K_OrderPassports.Count();
            var isSmallcage = order.IsCageSmall;
            var isTreywide = K_PassportFunctions.IsTreyWide(order.K_OrderPassports.FirstOrDefault().K_Passport);

            var MaxWideTreyInBigcage = 33;
            var MaxWideTreyInSmallcage = 15;

            var MaxNarrowTreyInBigcage = 44;
            var MaxNarrowTreyInSmallcage = 20;

            var numStickersPercage = 4;

            var reminderNumofstickers = 0;
            int? reminder, roundnumOfCages;

            //Small cage
            if (isSmallcage == true)
            {
                //if trey wide
                if (isTreywide == true)
                {
                    roundnumOfCages = jobnumMagash / MaxWideTreyInSmallcage;

                    reminder = jobnumMagash % MaxWideTreyInSmallcage;
                }
                //trey small
                else
                {
                    roundnumOfCages = jobnumMagash / MaxNarrowTreyInSmallcage;

                    reminder = jobnumMagash % MaxNarrowTreyInSmallcage;
                }               
            }
            //if Big cage
            else
            {
               
                //if trey wide
                if (isTreywide == true)
                {
                     roundnumOfCages = jobnumMagash / MaxWideTreyInBigcage;

                     reminder = jobnumMagash % MaxWideTreyInBigcage;                 
                }
                //trey small
                else
                {
                     roundnumOfCages = jobnumMagash / MaxNarrowTreyInBigcage;

                     reminder = jobnumMagash % MaxNarrowTreyInBigcage;
                }            
            }
            //if the reminder is less than 7 magash
            if (reminder <= 7)
            {
                reminderNumofstickers += Convert.ToInt32(reminder);
            }
            //if bigger, we're gonna use 5 stickers
            else
            {
                reminderNumofstickers += numStickersPercage;
            }

            var totalStickers = roundnumOfCages*numStickersPercage + reminderNumofstickers + stickerforPassport;
            stickNum = totalStickers;
            if(stickNum < 6) { stickNum = 5; }
            return stickNum;
        }

    }
}
