using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace POS.Converters
{
    class NumberToVnCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int number = (int)value;
            CultureInfo vietnamCulture = new CultureInfo("vi-VN");
            string formattedCurrency = number.ToString("C", vietnamCulture);

            return formattedCurrency;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string strValue)
            {
                strValue = strValue.Replace("₫", "").Trim();
                if (int.TryParse(strValue, NumberStyles.Currency, new CultureInfo("vi-VN"), out int result))
                {
                    return result;
                }
            }
            throw new NotImplementedException();
        }
    }
}
