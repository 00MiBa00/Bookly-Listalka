using System;
using UnityEngine;

namespace Values
{
    public static class Subscription
    {
        private const string PeriodKey = "Subscription.Period";

        public static bool HasSubscription()
        {
            return true;
            /*if(!PlayerPrefs.HasKey(PeriodKey))
            {
                return false;
            }

            long temp = Convert.ToInt64(PlayerPrefs.GetString(PeriodKey));
            DateTime expirePeriod = DateTime.FromBinary(temp);

            return expirePeriod > DateTime.UtcNow;*/
        }

        public static void SetWeekPeriod()
        {
            SetPeriod(DateTime.UtcNow.AddDays(7));
        }

        public static void SetMonthPeriod()
        {
            SetPeriod(DateTime.UtcNow.AddMonths(1));
        }
        
        public static void SetYearPeriod()
        {
            SetPeriod(DateTime.UtcNow.AddYears(1));
        }

        private static void SetPeriod(DateTime targetExpirePeriod)
        {
            PlayerPrefs.SetString(PeriodKey ,targetExpirePeriod.ToBinary().ToString());
        }
    }
}