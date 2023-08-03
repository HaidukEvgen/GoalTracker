using GoalTracker.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace GoalTracker.Converters
{
    public class IsCompletedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UIGoal goal)
            {
                return goal.IsCompleted;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
