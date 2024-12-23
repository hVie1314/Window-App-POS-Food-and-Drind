using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace POS.Converters
{
    public class PaymentMethodToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string paymentMethod)
            {

                return new SolidColorBrush(Color.FromArgb(100,248, 249, 183));

            }
            return new SolidColorBrush(Color.FromArgb(255,255, 189, 175));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
