using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace POS
{
    public sealed partial class OrderPage : Page
    {
        public ObservableCollection<string> MenuItems { get; set; }

        public OrderPage()
        {
            this.InitializeComponent();
            MenuItems = new ObservableCollection<string>
            {
                "Pizza", "Burger", "Pasta", "Salad", "Sushi"
            };
            this.DataContext = this;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            // Thêm chức năng để thêm món vào giỏ hàng
            string selectedItem = MenuListView.SelectedItem as string;
            int quantity = (int)QuantityNumberBox.Value;
            if (selectedItem != null && quantity > 0)
            {
                // Xử lý thêm món và số lượng vào giỏ hàng
                OrderSummaryTextBlock.Text += $"{quantity} x {selectedItem}\n";
            }
        }
    }
}
