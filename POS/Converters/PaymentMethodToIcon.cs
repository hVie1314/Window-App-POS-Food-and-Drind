using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Converters
{
    public class PaymentMethodToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var icon = new FontIcon();
            icon.FontFamily = new FontFamily("Segoe Fluent Icons");
            icon.FontSize = 14;
            icon.Margin = new Thickness(4);
            if (value is string paymentMethod)
            {
                icon.Glyph = "\xf78c";
            }
            else
            {
                icon.Glyph = "\xF78A";
            }
            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
