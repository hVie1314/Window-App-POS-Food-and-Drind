using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Converters
{
    internal class BoolToEmployeeStatus: IValueConverter
    {
    private const string _trueString = "Chính thức";
    private const string _falseString = "Không chính thức";
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                if (value is bool boolValue)
                {

                    return boolValue ? _trueString : _falseString;
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                if (value is string stringValue)
                {

                    return string.Equals(stringValue, _trueString, StringComparison.OrdinalIgnoreCase);
                }
                return false; 
            }
        }

}
