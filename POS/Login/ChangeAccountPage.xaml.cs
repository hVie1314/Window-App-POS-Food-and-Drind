using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;

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

            LocalSettingSaveAccount.ClearAccount();
            LocalSettingSaveAccount.SaveAccount(newUsername, newPassword);

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
