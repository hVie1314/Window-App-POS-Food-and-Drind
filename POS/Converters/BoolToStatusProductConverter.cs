using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Converters
{
    public class BoolToStatusProductConverter : IValueConverter
    {
        private string _trueString="Sẵn sàng phục vụ!";
        private string _falseString = "Món ăn đã hết!";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
            {
                // Trả về chuỗi dựa trên giá trị bool
                return boolValue ?_trueString : _falseString;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                // Chuyển đổi ngược chuỗi sang bool
                return string.Equals(stringValue, _trueString, StringComparison.OrdinalIgnoreCase);
            }
            return false; // Giá trị mặc định nếu chuyển đổi thất bại
        }
    }
}
