using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using POS.Models;
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
            if (menuItem != null && menuItem.Tag != null)
            {
                ViewModel.selectedCategory = menuItem.Tag.ToString();
                
            }
            else
            {
                ViewModel.selectedCategory = "";
            }
            ViewModel.LoadProducts(1);
            UpdatePagingInfo_bootstrap();
        }
       
        private void Sort_Item_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuFlyoutItem;
            ViewModel.selectedSortOrder = int.Parse(menuItem.Tag.ToString());
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
        //Next and Previous button
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex < ViewModel.TotalPages-1)
            {
                pagesComboBox.SelectedIndex++;
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentPage > 1)
            {
                pagesComboBox.SelectedIndex--;
            }
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

        private void itemListBox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null)
            {
                if (listBox.SelectedItem != null)
                {
                    var product = listBox.SelectedItem as Product;
                    if (product != null)
                    {
                        //var dialog = new ContentDialog();
                        //dialog.XamlRoot = this.XamlRoot;
                        //await dialog.ShowAsync();
                        FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
                        OrdersUserControl.AddToOrder(product);
                    }
                }
            }
        }
        //Dialog
        //private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var dialog = new ContentDialog();
        //    dialog.XamlRoot = this.XamlRoot;
        //    await dialog.ShowAsync();
        //}
    }
}
