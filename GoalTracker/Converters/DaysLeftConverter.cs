using System;
using System.Globalization;
using System.Windows.Data;

namespace GoalTracker.Converters
{
    public class DaysLeftConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime deadline)
            {
                DateTime today = DateTime.Today;
                if (deadline < today)
                {
                    return "Deadline has passed";
                }
                TimeSpan timeLeft = deadline - today;

                int yearsLeft = deadline.Year - today.Year;
                int monthsLeft = deadline.Month - today.Month;
                int daysLeft = deadline.Day - today.Day;

                if (daysLeft < 0)
                {
                    monthsLeft--;
                    daysLeft += DateTime.DaysInMonth(today.Year, today.Month);
                }

                if (monthsLeft < 0)
                {
                    yearsLeft--;
                    monthsLeft += 12;
                }

                string remainingTime = string.Empty;

                if (yearsLeft > 0)
                    remainingTime += $"{yearsLeft} year{(yearsLeft > 1 ? "s" : "")}, ";

                if (monthsLeft > 0)
                    remainingTime += $"{monthsLeft} month{(monthsLeft > 1 ? "s" : "")}, ";

                remainingTime += $"{daysLeft} day{(daysLeft > 1 ? "s" : "")} left";

                return remainingTime;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
