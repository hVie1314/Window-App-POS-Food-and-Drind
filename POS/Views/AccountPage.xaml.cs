using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;
using POS.Shells;
using POS.ViewModels;
using System;

namespace POS.Views
{
    public sealed partial class AccountPage : Page
    {
        public AccountViewModel AccountViewModel { get; set; }
        public AccountPage()
        {
            this.InitializeComponent();
            AccountViewModel = new AccountViewModel();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ShellWindow window = new ShellWindow();
            (Application.Current as App).m_window2 = window;
            window.Activate();
            window.Title = "POS HCMUS";
            (Application.Current as App).CurrentEmployee = null;
            (Application.Current as App).m_window.Close();
        }

        private void ChangeAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
        //private async void OnSaveAccountClick(object sender, RoutedEventArgs e)
        //{
        //    string newUsername = NewUsernameTextBox.Text;
        //    string newPassword = NewPasswordBox.Password;

        //    // Check if the username and password are valid

        //    if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
        //    {
        //        await new ContentDialog()
        //        {
        //            XamlRoot = this.XamlRoot,
        //            Content = "Đổi tài khoản không thành công",
        //            Title = "Thất bại",
        //            CloseButtonText = "Đóng"
        //        }.ShowAsync();
        //        return;
        //    }
        //    AccountCreator.CreateAccount(newUsername, newPassword, AccountViewModel.Employee.EmployeeID);
        //    await new ContentDialog()
        //    {
        //        XamlRoot = this.XamlRoot,
        //        Content = "Đổi tài khoản thành công",
        //        Title = "Thành công",
        //        CloseButtonText = "Đóng"
        //    }.ShowAsync();


    }

    }
