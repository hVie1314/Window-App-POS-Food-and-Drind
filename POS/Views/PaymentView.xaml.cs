using POS.ViewModels;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using POS.Interfaces;
using POS.Models;
using System.Linq;
using Microsoft.UI.Xaml.Input;
using System.Threading.Tasks;


namespace POS.Views
{
    /// <summary>
    /// Giao diện thanh toán.
    /// </summary>
    public sealed partial class PaymentView : Page
    {
        /// <summary>
        /// ViewModel quản lý logic và dữ liệu của giao diện thanh toán.
        /// </summary>
        public PaymentViewModel ViewModel { get; set; }

        /// <summary>
        /// Khởi tạo giao diện thanh toán.
        /// </summary>
        public PaymentView()
        {
            this.InitializeComponent();
            ViewModel = (Application.Current as App).PaymentViewModel;
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút thanh toán.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSubmitPayment(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedPaymentMethod == "Tiền mặt")
            {
                ViewModel.InvoiceId = ViewModel.SaveToDB();
                ViewModel.DeleteUsedDiscountCode();
                // Show invoice dialog
                ShowInvoiceDialog();
            }
            else if (ViewModel.SelectedPaymentMethod == "Momo")
            {
                // Show the MoMo payment dialog
                ShowMoMoPaymentDialog();
            }
        }

        /// <summary>
        /// Hiển thị dialog thanh toán qua MoMo.
        /// </summary>
        private async void ShowMoMoPaymentDialog()
        {
            ContentDialogResult result = await MoMoPaymentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ViewModel.InvoiceId = ViewModel.SaveToDB();
                ViewModel.DeleteUsedDiscountCode();

                // Show Invoice dialog
                ShowInvoiceDialog();
            }
            else
            {
                // Cancel, no action needed
            }
        }

        // Handle case text box is empty
        private void ReceivedAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReceivedAmountTextBox.Text))
            {
                ViewModel.ReceivedAmount = 0;
            }
        }

        /// <summary>
        /// Hiển thị dialog hóa đơn.
        /// </summary>
        private async void ShowInvoiceDialog()
        {
            // Hiển thị dialog hóa đơn
            ContentDialogResult result = await InvoiceDialog.ShowAsync();
        }


        /// <summary>
        /// Xử lý sự kiện khi nhấn nút đóng dialog hóa đơn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCloseInvoiceDialog(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Reset lại dữ liệu
            ViewModel.ResetData();
            // Về trang Menu
            var navigation = (Application.Current as App).navigate;
            var festivalItem = navigation.GetNavigationViewItems(typeof(Menu)).First();
            navigation.SetCurrentNavigationViewItem(festivalItem);
        }

        private void AccountItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Lấy NavigationViewItem từ sender
            var menuItem = sender as NavigationViewItem;

            // Kiểm tra xem menuItem có hợp lệ không
            if (menuItem != null)
            {
                var accountWindow = new ShellWindow();
                accountWindow.Activate();
            }
        }
    }
}
