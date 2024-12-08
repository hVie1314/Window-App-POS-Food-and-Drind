using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Converters
{
    class DoubleToVnCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double doubleValue)
            {
                // Format số kiểu VNĐ với phân cách hàng nghìn và thêm "₫"
                return string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}", doubleValue).Replace("₫", " ₫");
            }

            // Trả về một chuỗi mặc định nếu giá trị không hợp lệ
            return "0 ₫";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                // Loại bỏ các ký tự không phải số và chuyển đổi lại thành double
                string sanitizedValue = stringValue.Replace("₫", "").Trim().Replace(",", "");
                if (double.TryParse(sanitizedValue, out double result))
                {
                    return result;
                }
            }

            // Trả về 0 nếu không thể chuyển đổi
            return 0.0;
        }
    }
}
