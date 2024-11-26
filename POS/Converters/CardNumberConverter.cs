using Microsoft.UI.Xaml.Data;
using System;
using System.Text;

namespace POS.Converters
{
    public class CardNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string cardNumber = value as string;
            string separator = " - ";

            if (cardNumber == null)
            {
                return "undefined";
            }

            // example convert 1234567887654321 => 1234 - 5678 - 8765 - 4321
            string result = cardNumber.Substring(0, 4) + separator + cardNumber.Substring(4, 4) + separator + cardNumber.Substring(8, 4) + separator + cardNumber.Substring(12, 4);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
