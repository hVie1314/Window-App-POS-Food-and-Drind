﻿using Microsoft.UI;
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
    public class PaymentMethodToBackgroundColorHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string paymentMethod)
            {

                return new SolidColorBrush(Color.FromArgb(255,163, 255, 71));

            }
            return new SolidColorBrush(Color.FromArgb(255,229, 81, 70));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
