using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;
using System;

namespace POS
{
    public sealed partial class ChangeAccountPage : Page
    {
        public ChangeAccountPage()
        {
            this.InitializeComponent();
        }

        private async void OnSaveAccountClick(object sender, RoutedEventArgs e)
        {
            string newUsername = NewUsernameTextBox.Text;
            string newPassword = NewPasswordBox.Password;

            // Kiểm tra xem username và password có hợp lệ không
            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                await new ContentDialog()
                {
                    XamlRoot = this.XamlRoot,
                    Content = "Đồi tài khoản không thành công",
                    Title = "Fail",
                    CloseButtonText = "Ok"
                }.ShowAsync();
                return; // Kết thúc hàm nếu có lỗi
            }

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            LocalSettingSaveAccount.ClearAccount();
            LocalSettingSaveAccount.SaveAccount(newUsername, newPassword);

            await new ContentDialog()
            {
                XamlRoot = this.XamlRoot,
                Content = "Đồi tài khoản thành công",
                Title = "Success",
                CloseButtonText = "Ok"
            }.ShowAsync(); // Chờ cho đến khi dialog đóng
            
            
        }

    }
}
