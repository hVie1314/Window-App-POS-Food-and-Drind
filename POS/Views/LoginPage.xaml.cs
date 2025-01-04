using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Helpers;
using POS.Login;
using POS.Views;
using System.Text;
using System;
using POS.Shells;
using Windows.ApplicationModel.Store;
using System.Diagnostics;
using System.IO;

namespace POS.Views
{
    public sealed partial class LoginPage : Page
    {
        private string storedUsername; 
        private string storedPassword;
       

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string inputUsername = UsernameTextBox.Text;
            string inputPassword = PasswordBox.Password;

            if (inputUsername == storedUsername && inputPassword == storedPassword)
            {
                Shell window = new Shell();
                (Application.Current as App).navigate = window;
                window.Activate();
                // Set the title for the app
                window.Title = "POS HCMUS";
                (Application.Current as App).m_window2.Close();

            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Đăng nhập thất bại",
                    Content = "Tên đăng nhập hoặc mật khẩu không đúng.",
                    CloseButtonText = "Đóng",
                    XamlRoot = this.XamlRoot // Ensure XamlRoot is set
                };
                _ = dialog.ShowAsync();
            }
        }
    }
}
