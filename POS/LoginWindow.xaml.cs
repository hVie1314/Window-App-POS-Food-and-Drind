using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;
//using Raven.Client.Documents.Operations.Backups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace POS
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            this.InitializeComponent();
            DecryptAndLoad();
        }

        private void DecryptAndLoad()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var localFolder = ApplicationData.Current.LocalFolder;

            if (localSettings.Values.ContainsKey("PasswordInBase64"))
            {
                var encryptedPasswordBase64 = (string)localSettings.Values["PasswordInBase64"];
                var entropyBase64 = (string)localSettings.Values["PasswordEntropyInBase64"];

                var encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordBase64);
                var entropyInBytes = Convert.FromBase64String(entropyBase64);

                var passwordInBytes = ProtectedData.Unprotect(
                    encryptedPasswordInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser);

                var passwordRaw = Encoding.UTF8.GetString(passwordInBytes);
                passwordBox.Password = passwordRaw;
            }

            if (localSettings.Values.ContainsKey("UsernameInBase64"))
            {
                var encryptedUsernameBase64 = (string)localSettings.Values["UsernameInBase64"];
                var usernameEntropyBase64 = (string)localSettings.Values["UsernameEntropyInBase64"];

                var encryptedUsernameInBytes = Convert.FromBase64String(encryptedUsernameBase64);
                var usernameEntropyInBytes = Convert.FromBase64String(usernameEntropyBase64);

                var usernameInBytes = ProtectedData.Unprotect(
                    encryptedUsernameInBytes,
                    usernameEntropyInBytes,
                    DataProtectionScope.CurrentUser);

                var usernameRaw = Encoding.UTF8.GetString(usernameInBytes);
                usernameTextBox.Text = usernameRaw;
            }
        }
        private void EncryptAndSave()
        {
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                var localFolder = ApplicationData.Current.LocalFolder;

                string usernameRaw = usernameTextBox.Text;
                string passwordRaw = passwordBox.Password;

                var passwordInBytes = Encoding.UTF8.GetBytes(passwordRaw);
                var entropyInBytes = new byte[20];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(entropyInBytes);
                }

                var encryptedPasswordInBytes = ProtectedData.Protect(
                    passwordInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser);

                var encryptedPasswordBase64 = Convert.ToBase64String(encryptedPasswordInBytes);
                var entropyInBase64 = Convert.ToBase64String(entropyInBytes);

                // Mã hóa username
                var usernameInBytes = Encoding.UTF8.GetBytes(usernameRaw);

                // Tạo entropy cho username
                var usernameEntropyInBytes = new byte[20];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(usernameEntropyInBytes);
                }

                var encryptedUsernameInBytes = ProtectedData.Protect(
                    usernameInBytes,
                    usernameEntropyInBytes,
                    DataProtectionScope.CurrentUser);

                var usernameEntropyBase64 = Convert.ToBase64String(usernameEntropyInBytes);
                var encryptedUsernameBase64 = Convert.ToBase64String(encryptedUsernameInBytes);

                localSettings.Values["PasswordInBase64"] = encryptedPasswordBase64;
                localSettings.Values["PasswordEntropyInBase64"] = entropyInBase64;
                localSettings.Values["UsernameInBase64"] = encryptedUsernameBase64;
                localSettings.Values["UsernameEntropyInBase64"] = usernameEntropyBase64;
            }

    }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị cửa sổ chính
            if(rememberMeCheckBox.IsChecked==true)
            {
                EncryptAndSave();
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Activate();
            this.Close();
        }
    }
}
