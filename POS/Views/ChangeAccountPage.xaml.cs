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
using POS.Login;
using POS.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace POS.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangeAccountPage : Page
    {
        public ChangeAccountViewModel ChangeAccountViewModel { get; set; }
        public ChangeAccountPage()
        {
            this.InitializeComponent();
            ChangeAccountViewModel = new ChangeAccountViewModel();
        }
        private async void OnSaveAccountClick(object sender, RoutedEventArgs e)
        {
            string newUsername = NewUsernameTextBox.Text;
            string newPassword = NewPasswordBox.Password;

            // Check if the username and password are valid

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                await new ContentDialog()
                {
                    XamlRoot = this.XamlRoot,
                    Content = "Đổi tài khoản không thành công, dữ liệu không hợp lệ",
                    Title = "Thất bại",
                    CloseButtonText = "Đóng"
                }.ShowAsync();
                return;
            }
            if (NewPasswordBox.Password != SecondPasswordBox.Password)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.XamlRoot,
                    Content = "Mật khẩu không khớp",
                    Title = "Thất bại",
                    CloseButtonText = "Đóng"
                }.ShowAsync();
                return;
            }
            foreach (var account in ChangeAccountViewModel.Accounts)
            {
                if (newUsername == account.UsernameString)
                {
                    await new ContentDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Content = "Tên tài khoản đã tồn tại, vui lòng chọn một tên tài khoản khác",
                        Title = "Thất bại",
                        CloseButtonText = "Đóng"
                    }.ShowAsync();
                    return;
                }
            }
                AccountCreator.CreateAccount(newUsername, newPassword, (Application.Current as App).CurrentEmployee.EmployeeID);
                await new ContentDialog()
                {
                    XamlRoot = this.XamlRoot,
                    Content = "Đổi tài khoản thành công",
                    Title = "Thành công",
                    CloseButtonText = "Đóng"
                }.ShowAsync();
            }
        }
    }