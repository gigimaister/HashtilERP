﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashtilERP.Shared.Models
{
    //GET BEGIN AND END PREPREPORT DATE RANGE
    public static class KOrderAlgorithemShared
    {
        public static List<DateTime> GetPrepReportWeekRange(DateTime Datetime, int earlyRepo = 0)
        {
            List<DateTime> prepRepo = new List<DateTime>();
            switch (Datetime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    prepRepo.Add(DateTime.Today);
                    prepRepo.Add(DateTime.Today.AddDays(6));
                    break;
                case DayOfWeek.Monday:
                    prepRepo.Add(DateTime.Today.AddDays(-1));
                    prepRepo.Add(DateTime.Today.AddDays(5));
                    break;
                case DayOfWeek.Tuesday:
                    prepRepo.Add(DateTime.Today.AddDays(-2));
                    prepRepo.Add(DateTime.Today.AddDays(4));
                    break;
                case DayOfWeek.Wednesday:
                    prepRepo.Add(DateTime.Today.AddDays(-3));
                    prepRepo.Add(DateTime.Today.AddDays(3));
                    break;
                case DayOfWeek.Thursday:
                    prepRepo.Add(DateTime.Today.AddDays(3));
                    prepRepo.Add(DateTime.Today.AddDays(9));
                    break;
                case DayOfWeek.Friday:
                    prepRepo.Add(DateTime.Today.AddDays(2));
                    prepRepo.Add(DateTime.Today.AddDays(8));
                    break;
                case DayOfWeek.Saturday:
                    prepRepo.Add(DateTime.Today.AddDays(1));
                    prepRepo.Add(DateTime.Today.AddDays(7));
                    break;
            }
            return prepRepo;
        }

        public static string GetHebrewDayOfTheWeek(DateTime? dateTime)
        {
            string HebrewDay = "";
            DateTime NewDate = Convert.ToDateTime(dateTime);

            switch (NewDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    HebrewDay = "יום ראשון";
                    break;
                case DayOfWeek.Monday:
                    HebrewDay = "יום שני";
                    break;
                case DayOfWeek.Tuesday:
                    HebrewDay = "יום שלישי";
                    break;
                case DayOfWeek.Wednesday:
                    HebrewDay = "יום רביעי";
                    break;
                case DayOfWeek.Thursday:
                    HebrewDay = "יום חמישי";
                    break;
                case DayOfWeek.Friday:
                    HebrewDay = "יום שישי";
                    break;
                case DayOfWeek.Saturday:
                    HebrewDay = "יום שבת";
                    break;
               

            }
            return HebrewDay;
        }
    }
}
