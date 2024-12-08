using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.Models;
using POS.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace POS.Views.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrdersUserConstrol : UserControl
    {
        public OrderDetailViewModel ViewModel { get; set; } = new OrderDetailViewModel();

        public void AddToOrder(Product info,int quanlity)
        {
            ViewModel.Add(info,quanlity);
        }
        public OrdersUserConstrol()
        {
            this.InitializeComponent();
        }
        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveToDatabase();
            ViewModel.Items.Clear();
            ViewModel.Total = 0;
            ViewModel.SubTotal = 0;
            ViewModel.Tax = 0;
        }
    }
}
