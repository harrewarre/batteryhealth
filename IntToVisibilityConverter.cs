using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BatteryHealth
{
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentValue = int.Parse(value.ToString());
            var parameterValue = int.Parse(parameter.ToString());

            return currentValue >= parameterValue
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
