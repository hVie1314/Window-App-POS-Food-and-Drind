using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace POS
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Order page
            Frame.Navigate(typeof(OrderPage));
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Table Management page
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Payment page
        }
    }
}
