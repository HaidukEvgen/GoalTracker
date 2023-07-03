using System;
using System.Globalization;
using System.Windows.Data;

namespace GoalTracker.Converters
{
    public class AimAchievementConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 4 && values[0] is int currentAchievement && values[1] is int aim && values[2] is string unitOfMeasure && values[3] is double progress)
            {
                string formattedProgress = $"{(progress/100):P0}";
                return $"{currentAchievement} of {aim} {unitOfMeasure} ({formattedProgress})";
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
