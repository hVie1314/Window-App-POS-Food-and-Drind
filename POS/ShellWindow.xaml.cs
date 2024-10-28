using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using POS;

namespace POS
{
    public sealed partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(LoginPage));
        }
    }
}
