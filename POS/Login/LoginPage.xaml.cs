using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;

namespace POS
{
    public sealed partial class LoginPage : Page
    {
        private string storedUsername = "admin";
        private string storedPassword = "password";

        public LoginPage()
        {
            this.InitializeComponent();
            LoadStoredCredentials();
        }

        private void LoadStoredCredentials()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("username") && localSettings.Values.ContainsKey("password"))
            {
                // Giải mã username và password
                storedUsername = EncryptionHelper.Decrypt(localSettings.Values["username"].ToString());
                storedPassword = EncryptionHelper.Decrypt(localSettings.Values["password"].ToString());
            }
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            string inputUsername = UsernameTextBox.Text;
            string inputPassword = PasswordBox.Password;

            if (inputUsername == storedUsername && inputPassword == storedPassword)
            {
                if (Frame != null)
                {
                    // Mở cửa sổ MainWindow
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Activate(); // Hiển thị MainWindow

                    // Đóng trang đăng nhập
                    var currentWindow = Window.Current;
                    currentWindow?.Close();
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
