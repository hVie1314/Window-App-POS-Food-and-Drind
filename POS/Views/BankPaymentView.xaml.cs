using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.Models;

namespace POS.Views
{
    /// <summary>
    /// BankPaymentView
    /// </summary>
    public sealed partial class BankPaymentView : Page
    {
        public BankPayment ViewModel { get; set; }
        public BankPaymentView()
        {
            this.InitializeComponent();

            // Placeholder data
            ViewModel = new BankPayment()
            {
                CardHolderName = "NGUYEN VAN A",
                CardNumber = "1234000056780000",
                ExpiryDate = "12/2030"
            };
        }
    }
}
