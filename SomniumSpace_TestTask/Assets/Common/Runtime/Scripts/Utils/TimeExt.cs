using System;

namespace Common.Utils
{
    public class TimeExt
    {
        public static double CurrentTimeInMilliseconds => DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        public static double CurrentTimeInSeconds => DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        public static TimeSpan CurrentTimeSpan => DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
        public static DateTime GetDateTime(double milliseconds)=>(new DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);
        public static DateTime GetDateTimeFromSeconds(double milliseconds)=>(new DateTime(1970, 1, 1)).AddSeconds(milliseconds);
    }
}