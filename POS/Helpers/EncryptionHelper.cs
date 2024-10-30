using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace POS
{
    public static class EncryptionHelper
    {
        //    private void Decrypt(string encryptedtext)
        //    {
        //        {
        //            var encryptedPasswordInBytes = Convert.FromBase64String(encryptedText);

        //            var passwordInBytes = ProtectedData.Unprotect(
        //                encryptedPasswordInBytes,
        //                entropyInBytes,
        //                DataProtectionScope.CurrentUser);

        //            var passwordRaw = Encoding.UTF8.GetString(passwordInBytes);
        //            passwordBox.Password = passwordRaw;
        //        }

        //        if (localSettings.Values.ContainsKey("UsernameInBase64"))
        //        {
        //            var encryptedUsernameBase64 = (string)localSettings.Values["UsernameInBase64"];
        //            var usernameEntropyBase64 = (string)localSettings.Values["UsernameEntropyInBase64"];

        //            var encryptedUsernameInBytes = Convert.FromBase64String(encryptedUsernameBase64);
        //            var usernameEntropyInBytes = Convert.FromBase64String(usernameEntropyBase64);

        //            var usernameInBytes = ProtectedData.Unprotect(
        //                encryptedUsernameInBytes,
        //                usernameEntropyInBytes,
        //                DataProtectionScope.CurrentUser);

        //            var usernameRaw = Encoding.UTF8.GetString(usernameInBytes);
        //            usernameTextBox.Text = usernameRaw;
        //        }
        //    }
        //    public static void Encrypt(string str)
        //    {
        //        {
        //            var stringInBytes = Encoding.UTF8.GetBytes(str);
        //            var entropyInBytes = new byte[20];

        //            using (var rng = RandomNumberGenerator.Create())
        //            {
        //                rng.GetBytes(entropyInBytes);
        //            }

        //            var encryptedStringInBytes = ProtectedData.Protect(
        //                stringInBytes,
        //                entropyInBytes,
        //                DataProtectionScope.CurrentUser);

        //            var encryptedPasswordBase64 = Convert.ToBase64String(encryptedStringInBytes);
        //            var entropyInBase64 = Convert.ToBase64String(entropyInBytes);

        //            localSettings.Values["PasswordInBase64"] = encryptedPasswordBase64;
        //            localSettings.Values["PasswordEntropyInBase64"] = entropyInBase64;
        //            localSettings.Values["UsernameInBase64"] = encryptedUsernameBase64;
        //            localSettings.Values["UsernameEntropyInBase64"] = usernameEntropyBase64;
        //        }

        //    }
        //}
    }
}
