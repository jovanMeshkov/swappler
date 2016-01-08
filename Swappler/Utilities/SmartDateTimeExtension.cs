using System;

namespace Swappler.Utilities
{
    public static class SmartDateTimeExtension
    {
        public static string ToSmartDate(this DateTime dateTime)
        {
            var dateTimeNow = DateTime.Now;
            int secondsDifference = (int)dateTimeNow.Subtract(dateTime).TotalSeconds;
            int minutesDifference = secondsDifference/60;
            int hoursDifference = minutesDifference/60;
            int daysDifference = hoursDifference/24;

            // Below 2 seconds mean 'Just now'
            if (secondsDifference <= 2)
            {
                return "Just now";
            }

            // Difference is in one minute (2 secs - 59 secs)
            if (secondsDifference >= 2 && secondsDifference <= 59)
            {
                return secondsDifference + " secs";
            }

            // Difference is in one hour (1 min - 59 mins)
            if (minutesDifference >= 1 && minutesDifference <= 59)
            {
                return minutesDifference + " mins";
            }

            // Difference is in one day (1 hr - 24 hrs)
            if (hoursDifference >= 1 && hoursDifference <= 24)
            {
                return hoursDifference + " hrs";
            }

            // Happend sometime yesterday
            if (daysDifference == 1)
            {
                return dateTime.ToString("'Yesterday' 'at' HH:mm");
            }

            // Happend this week 
            if (daysDifference >= 1 && daysDifference <= 7)
            {
                return dateTime.ToString("dddd 'at' HH:mm");
            }

            // Happend this year
            if (dateTimeNow.Year == dateTime.Year)
            {
                return dateTime.ToString("d MMMM 'at' HH:mm");
            }

            // Happend year/s ago
            if (dateTimeNow.Year != dateTime.Year)
            {
                return dateTime.ToString("d MMMM yyyy 'at' HH:mm");
            }

            return dateTime.ToString("d MMMM yyyy 'at' HH:mm");
        }
    }
}