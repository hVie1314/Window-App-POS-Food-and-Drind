using System;
using Microsoft.UI.Xaml;
using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace POS.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime date)
            {
                return date.ToString("dd/MM/yyyy"); 
            }
            return string.Empty;
        }

        // Convert string back to DateTime
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string dateString)
            {
                if (DateTime.TryParseExact(
                    dateString,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
                {
                    return date;
                }
            }
            return DependencyProperty.UnsetValue; // Indicate that conversion failed
        }
    }
}
