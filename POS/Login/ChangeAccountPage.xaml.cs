using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;

namespace POS
{
    public sealed partial class ChangeAccountPage : Page
    {
        public ChangeAccountPage()
        {
            this.InitializeComponent();
        }

        private void OnSaveAccountClick(object sender, RoutedEventArgs e)
        {
            string newUsername = NewUsernameTextBox.Text;
            string newPassword = NewPasswordBox.Password;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Mã hóa và lưu username và password
            localSettings.Values["username"] = EncryptionHelper.Encrypt(newUsername);
            localSettings.Values["password"] = EncryptionHelper.Encrypt(newPassword);

            ContentDialog dialog = new ContentDialog
            {
                Title = "Success",
                Content = "Account has been updated.",
                CloseButtonText = "OK"
            };
            _ = dialog.ShowAsync();

            Frame.Navigate(typeof(LoginPage));
        }
    }
}
