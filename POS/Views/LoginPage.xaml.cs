using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;
using POS.Views;
using Windows.ApplicationModel.Store;

namespace POS.Views
{
    public sealed partial class LoginPage : Page
    {
        private string storedUsername; 
        private string storedPassword;
       
        public LoginPage()
        {
            this.InitializeComponent();
            LocalSettingSaveAccount.SaveAccount();
            (storedUsername, storedPassword) = LocalSettingSaveAccount.GetAccount();
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
                (Application.Current as App).m_window2.Close();

            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Login Failed",
                    Content = "Incorrect username or password.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot // Ensure XamlRoot is set
                };
                _ = dialog.ShowAsync();
            }
        }
    }
}
