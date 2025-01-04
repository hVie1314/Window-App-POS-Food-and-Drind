using Microsoft.UI.Xaml;
using POS.Helpers;
using POS.Models;
using POS.Services.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Login
{
    internal class AccountCreator
    {
        public static void CreateAccount(string username, string password, int employeeID)
        {
            byte[] key = (Application.Current as App).aesKey;
            //Username encryption
            byte[] iv_username = KeyGenerator.GenerateRandomKey(16); // 16 bytes
            // Encrypt
            byte[] encryptedUsername = EncryptionHelper.EncryptString(username, key, iv_username);
            //=======
            //Password encryption
            byte[] iv_password = KeyGenerator.GenerateRandomKey(16); // 16 bytes
            // Encrypt
            byte[] encryptedPassword = EncryptionHelper.EncryptString(password, key, iv_password);
            //=======
            PostgresEmployeeDao.updateAccount(employeeID, encryptedUsername, iv_username, encryptedPassword, iv_password);
            //Decrypt
            //string decrypted = EncryptionHelper.DecryptString(encrypted, key, iv);
            //Debug.WriteLine("Decrypted: " + decrypted);
            //LocalSettingSaveAccount.SaveAccount();
            //(storedUsername, storedPassword) = LocalSettingSaveAccount.GetAccount();
        }
    }
}
