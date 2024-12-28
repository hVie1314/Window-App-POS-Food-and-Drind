using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.Models;
using POS.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace POS.Views.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrdersUserControl : UserControl
    {
        public OrderDetailViewModel ViewModel { get; set; } = new OrderDetailViewModel();

        public void AddToOrder(Product info, int quanlity, string note)
        {
            ViewModel.Add(info, quanlity, note);
        }
        public OrdersUserControl()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }
        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            int newID = ViewModel.SaveToDatabase(ViewModel.InvoiceID);
            ViewModel.Items.Clear();
            ViewModel.Total = 0;
            ViewModel.SubTotal = 0;
            ViewModel.Tax = 0;
            DisplayIDInvoiceDialog(newID);
        }
        private void PayInvoice_Click(object sender, RoutedEventArgs e)
        {
            int payFromMenuInvoiceId = -1; // flag to indicate that this payment is from menu page
            // Pass data to PaymentViewModel
            var paymentViewModel = (Application.Current as App).PaymentViewModel;
            paymentViewModel.SetItems(ViewModel.Items, ViewModel.SubTotal, payFromMenuInvoiceId);

            // Navigate to PaymentView
            var navigation = (Application.Current as App).navigate;
            var festivalItem = navigation.GetNavigationViewItems(typeof(PaymentView)).First();
            navigation.SetCurrentNavigationViewItem(festivalItem);
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as Order;
            ViewModel.Remove(item);
        }
        //================================================================================================
        //Notification
        private void ShowSaveSuccessTeachingTip()
        {
            SaveSuccessTeachingTip.IsOpen = true;

            // Auto close after 3s
            _ = Task.Delay(3000).ContinueWith(_ =>
            {
                DispatcherQueue.TryEnqueue(() => SaveSuccessTeachingTip.IsOpen = false);
            });
        }
        //Notification
        private async void DisplayIDInvoiceDialog(int newID)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "Lưu hóa đơn thành công!",
                Content = $"Mã hóa đơn: {newID}",
                CloseButtonText = "Đóng",
                XamlRoot = this.XamlRoot
            };
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        //================================================================================================

    }
}
