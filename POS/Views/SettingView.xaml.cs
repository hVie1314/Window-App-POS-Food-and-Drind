using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using POS.Models;
using POS.Services.DAO;

using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using POS.ViewModels;


namespace POS.Views
{
    public sealed partial class SettingView : Page
    {
        public SettingsViewModel ViewModel { get; set; }

        public SettingView()
        {
            this.InitializeComponent();

            ViewModel = new SettingsViewModel();

            // Gán ViewModel vào DataContext để binding
            DataContext = ViewModel;

            // Khởi tạo giao diện với giá trị từ ViewModel
            InitializeFields();
        }

        // Khởi tạo các trường trong giao diện từ ViewModel
        private void InitializeFields()
        {
            VatTextBox.Text = ViewModel.VAT.ToString();
            AccessKeyPasswordBox.Password = ViewModel.AccessKey;
            SecretKeyPasswordBox.Password = ViewModel.SecretKey;
            IpnUrlTextBox.Text = ViewModel.IpnUrl;
        }

        // Xử lý sự kiện khi nhấn nút Lưu
        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            // Cập nhật dữ liệu vào ViewModel
            if (float.TryParse(VatTextBox.Text, out float vat))
            {
                ViewModel.VAT = vat;
            }
            else
            {
                ShowError("Vui lòng nhập số hợp lệ cho Thuế VAT.");
                return;
            }
            ViewModel.AccessKey = AccessKeyPasswordBox.Password;
            ViewModel.SecretKey = SecretKeyPasswordBox.Password;
            ViewModel.IpnUrl = IpnUrlTextBox.Text;

            ViewModel.PaymentLoadLocalSettings();

            // Hiển thị thông báo lưu thành công
            ShowMessage("Vui lòng khởi động lại ứng dụng để cập nhật thông tin.");
        }

        // Hàm hiển thị thông báo thành công
        private async void ShowMessage(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Thông tin đã được lưu",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }

        // Hàm hiển thị thông báo lỗi
        private async void ShowError(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }

    }
}
