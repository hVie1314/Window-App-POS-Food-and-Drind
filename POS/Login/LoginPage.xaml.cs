using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;
using POS.Views;

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
                if (Frame != null)
                {
                    Frame.Navigate(typeof(Menu));
                }
                else
                {
                    // Handle the case where Frame is null
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Navigation Error",
                        Content = "Unable to navigate to the main window.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot // Ensure XamlRoot is set
                    };
                    _ = dialog.ShowAsync();
                }
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
