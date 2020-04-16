using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starbound.Common.WinForms
{
    public class TimeLabel : Label
    {
        private DateTime time;

        public DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                this.Text = DetermineText(DateTime.Now - time);
                TimeText = String.Format("{0:h:mm:ss tt, dd MMMM yyyy}", time);
            }
        }

        public string TimeText
        {
            get;
            private set;
        }

        public static string DetermineText(TimeSpan ellapsedTime)
        {
            if (ellapsedTime.TotalMinutes < 1) { return "< 1 minute ago"; }
            if (ellapsedTime.TotalMinutes < 2) { return "1 minute ago"; }
            if (ellapsedTime.TotalMinutes < 5) { return (int)ellapsedTime.TotalMinutes + " minutes ago"; }
            if (ellapsedTime.TotalMinutes < 60) { return ((((int)ellapsedTime.TotalMinutes) / 5) * 5) + " minutes ago"; }
            if (ellapsedTime.TotalHours < 2) { return "1 hour ago"; }
            if (ellapsedTime.TotalHours < 24) { return (int)ellapsedTime.TotalHours + " hours ago"; }

            int totalDays = (int)ellapsedTime.TotalHours / 24;
            int totalWeeks = totalDays / 7;
            int totalMonths = totalDays / 30;
            int totalYears = (int)(totalDays / 365.25);

            if (totalDays == 1) { return "1 day ago"; }
            if (totalDays < 7) { return totalDays + " days ago"; }
            if (totalWeeks < 2) { return "1 week ago"; }
            if (totalWeeks < 9) { return totalWeeks + " weeks ago"; }
            if (totalMonths < 12) { return totalMonths + " months ago"; }
            if (totalYears == 1) { return "1 year ago"; }
            return totalYears + " years ago";
        }
    }
}
