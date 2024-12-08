using POS.ViewModels;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;


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
            ViewModel = new PaymentViewModel();
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút thanh toán.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OnPaymentButtonClicked(object sender, RoutedEventArgs e)
        {
            // set the max width of the dialog
            PaymentDialog.Resources["ContentDialogMaxWidth"] = 1500;
            _ = PaymentDialog.ShowAsync();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút hủy thanh toán.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCancelPayment(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            sender.Hide();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút xác nhận thanh toán.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSubmitPayment(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (ViewModel.SelectedPaymentMethod == "Tiền mặt")
            {
                sender.Hide();
                // Code logic for cash payment
                //
                //
                //

                // Show invoice dialog
                ShowInvoiceDialog();
            }
            else if (ViewModel.SelectedPaymentMethod == "Momo")
            {
                sender.Hide();
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
                // Code logic for MoMo payment
                //
                //
                //

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
            if (result == ContentDialogResult.Secondary)
            {
                // Handle Close button 
                // Reload screen behind, implement later
                //
                //
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút đóng dialog hóa đơn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCloseInvoiceDialog(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Đóng dialog
            sender.Hide();
        }


        // Bugs: Can not convert to png file
        //private async void OnPrintInvoice(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        //{
        //    try
        //    {
        //        // Kiểm tra kích thước control
        //        if (Invoice.ActualWidth == 0 || Invoice.ActualHeight == 0)
        //        {
        //            // Thông báo lỗi nếu control không được render đúng
        //            var errorDialog = new ContentDialog
        //            {
        //                Title = "Error",
        //                Content = "Invoice control size is invalid. Please ensure the dialog is fully visible.",
        //                CloseButtonText = "OK",
        //                XamlRoot = this.XamlRoot
        //            };
        //            await errorDialog.ShowAsync();
        //            return;
        //        }

        //        // Tạo FileSavePicker để chọn nơi lưu tệp
        //        var savePicker = new FileSavePicker
        //        {
        //            SuggestedStartLocation = PickerLocationId.PicturesLibrary,
        //            SuggestedFileName = "Invoice"
        //        };
        //        savePicker.FileTypeChoices.Add("PNG Image", new List<string> { ".png" });

        //        var shellWindow = (App.Current as App)?.m_window;
        //        var hWnd = WindowNative.GetWindowHandle(shellWindow);
        //        InitializeWithWindow.Initialize(savePicker, hWnd);

        //        StorageFile file = await savePicker.PickSaveFileAsync();
        //        if (file == null)
        //        {
        //            var noFileDialog = new ContentDialog
        //            {
        //                Title = "No file selected",
        //                Content = "Please select a location to save the invoice.",
        //                CloseButtonText = "OK",
        //                XamlRoot = this.XamlRoot
        //            };
        //            await noFileDialog.ShowAsync();
        //            return;
        //        }

        //        // Render giao diện InvoiceDialog thành một bitmap
        //        var renderTargetBitmap = new RenderTargetBitmap();
        //        Invoice.UpdateLayout(); // Đảm bảo control được đo và sắp xếp
        //        await renderTargetBitmap.RenderAsync(Invoice);

        //        var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();
        //        var pixels = pixelBuffer.ToArray();

        //        var displayInformation = DisplayInformation.GetForCurrentView();
        //        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
        //        {
        //            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
        //            encoder.SetPixelData(
        //                BitmapPixelFormat.Bgra8,
        //                BitmapAlphaMode.Premultiplied,
        //                (uint)renderTargetBitmap.PixelWidth,
        //                (uint)renderTargetBitmap.PixelHeight,
        //                displayInformation.RawDpiX > 0 ? displayInformation.RawDpiX : 96,
        //                displayInformation.RawDpiY > 0 ? displayInformation.RawDpiY : 96,
        //                pixels);
        //            await encoder.FlushAsync();
        //        }

        //        var successDialog = new ContentDialog
        //        {
        //            Title = "Invoice Saved",
        //            Content = "The invoice has been successfully saved as an image.",
        //            CloseButtonText = "OK",
        //            XamlRoot = this.XamlRoot
        //        };
        //        await successDialog.ShowAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorDialog = new ContentDialog
        //        {
        //            Title = "Error",
        //            Content = $"An error occurred while saving the invoice: {ex.Message}",
        //            CloseButtonText = "OK",
        //            XamlRoot = this.XamlRoot
        //        };
        //        await errorDialog.ShowAsync();
        //    }
        //}

    }
}
