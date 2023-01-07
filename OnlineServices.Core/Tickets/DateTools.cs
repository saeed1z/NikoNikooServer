using System;
using System.Globalization;

namespace OnlineServices.Api.Helpers
{
    /// <summary>
    /// Date Time Convertor 
    /// </summary>
    public static class DateConvertor
    {

        /// <summary>
        /// Change Gregorian To Shamsi
        /// </summary>
        /// <param name="date">Gregorian Date Time</param>
        /// <returns>
        /// Shamsi Date Time 
        /// </returns>
        public static string ToShamsi(this DateTime? date)
        {
            PersianCalendar pc = new PersianCalendar();
            date = (date != null) ? date : DateTime.Now;
            return pc.GetYear(date.Value).ToString() + "/" + pc.GetMonth(date.Value).ToString("00") + "/" + pc.GetDayOfMonth(date.Value).ToString("00");


        }

        /// <summary>
        /// Change Gregorian To Shamsi
        /// </summary>
        /// <param name="date">Gregorian Date Time</param>
        /// <returns>
        /// Shamsi Date Time 
        /// </returns>
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            date = (date != null) ? date : DateTime.Now;
            return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00") + "-" +
                pc.GetHour(date) + ":" + pc.GetMinute(date);
        }

        /// <summary>
        /// Change Gregorian To Shamsi 
        /// </summary>
        /// <param name="date">Shamsi Date Time</param>
        /// <returns>
        /// Gregorian Date Time
        /// </returns>
        public static DateTime ToGregorian(this string date)
        {
            if (date != null)
            {
                string[] valueDates = date.Split('-');
                PersianCalendar pc = new PersianCalendar();
                DateTime dateTime = new DateTime(int.Parse(valueDates[0]), int.Parse(valueDates[1]), int.Parse(valueDates[2]), pc);
                return DateTime.Parse(dateTime.ToString(CultureInfo.InvariantCulture));
            }
            return DateTime.Now;
        }
    }
}
