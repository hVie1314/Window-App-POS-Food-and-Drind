using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;
using POS.Login;
using POS.Shells;
using POS.ViewModels;
using System;
using System.Threading.Tasks;

namespace POS.Views
{
    public sealed partial class AccountPage : Page
    {
        public AccountViewModel AccountViewModel { get; set; }
        public AccountPage()
        {
            this.InitializeComponent();
            AccountViewModel = new AccountViewModel();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ShellWindow window = new ShellWindow();
            (Application.Current as App).m_window2 = window;
            window.Activate();
            window.Title = "POS HCMUS";
            (Application.Current as App).CurrentEmployee = null;
            (Application.Current as App).m_window.Close();
        }

        private void ChangeAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountVerificationWindow window = new AccountVerificationWindow();
            window.Activate();
        }

    }

}
