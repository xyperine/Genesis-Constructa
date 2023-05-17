using System;

namespace ColonizationMobileGame.UI
{
    public class TimeFormatter
    {
        public string GetFormattedTime(float seconds, TimePrecision precision)
        {
            string format = GetFormat(precision);
            string timeText = TimeSpan.FromSeconds(seconds).ToString(format);

            return timeText;
        }


        private string GetFormat(TimePrecision precision)
        {
            return precision switch
            {
                TimePrecision.Seconds => @"mm\:ss",
                TimePrecision.Milliseconds => @"mm\:ss\:fff",
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}