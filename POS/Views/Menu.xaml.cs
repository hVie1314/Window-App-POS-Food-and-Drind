using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace POS.Views
{
    public sealed partial class Menu : Page
    {
        public ProductViewModel ViewModel { get; set; }
        public Menu()
        {
            this.InitializeComponent();
            ViewModel = new ProductViewModel();
            this.DataContext = ViewModel;
            UpdatePagingInfo_bootstrap();
        }
        //==========================================================
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuFlyoutItem;
            ViewModel._selectedCategory = menuItem.Tag.ToString();
            ViewModel.LoadProducts(1);
            UpdatePagingInfo_bootstrap();
        }
       
        private void Sort_Item_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuFlyoutItem;
            ViewModel._selectedSortOrder = int.Parse(menuItem.Tag.ToString());
            ViewModel.LoadProducts(1);
            UpdatePagingInfo_bootstrap();
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            
                ViewModel.Keyword = SearchBox.Text;
                ViewModel.LoadProducts(1);
                UpdatePagingInfo_bootstrap();
        }
        //================================================================

        private void AccountItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Lấy NavigationViewItem từ sender
            var menuItem = sender as NavigationViewItem;

            // Kiểm tra xem menuItem có hợp lệ không
            if (menuItem != null)
            {
                var accountWindow = new ShellWindow();
                accountWindow.Activate();
            }
        }

        void UpdatePagingInfo_bootstrap()
        {
            var infoList = new List<object>();
            for (int i = 1; i <= ViewModel.TotalPages; i++)
            {
                infoList.Add(new
                {
                    Page = i,
                    Total = ViewModel.TotalPages
                });
            };

            pagesComboBox.ItemsSource = infoList;
            pagesComboBox.SelectedIndex = 0;
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = pagesComboBox.SelectedItem;
            if (item != null)
            {
                ViewModel.LoadProducts(item.Page);
            }
        }
    }
}
