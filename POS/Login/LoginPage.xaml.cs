using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;
using POS.Views;
using Windows.ApplicationModel.Store;

namespace POS
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
                Window window = new Shell();
                window.Activate();
                
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
