using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using POS.DTOs;
using POS.Models;
using POS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace POS.Views
{
    /// <summary>
    /// Menu
    /// 
    /// </summary>
    public sealed partial class Menu : Page
    {
        public ProductViewModel ViewModel { get; set; }
        public Product SelectedProduct { get; set; }
        public Menu()
        {
            this.InitializeComponent();
            this.ViewModel = new ProductViewModel();
            //this.DataContext = ViewModel;
            this.SelectedProduct = new Product();
            //this.DataContext = ViewModel;
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
        
        private void AccountItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
            var menuItem = sender as NavigationViewItem;


            if (menuItem != null)
            {
                var accountWindow = new ShellWindow();
                accountWindow.Activate();
            }
        }
        //================================================================
        //Pagination
        //Next and Previous button
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex < ViewModel.TotalPages - 1)
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
        //================================================================================================

        private void itemListBox_selectionChanged(object sender, TappedRoutedEventArgs e)
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
                        //SelectedProduct = product;
                        SelectedProduct.AssignFrom(product);
                        // Hiển thị hình ảnh tương ứng
                        var bitmap = new BitmapImage(new Uri(product.ImagePath, UriKind.RelativeOrAbsolute));
                        SelectedProductImage.Source = bitmap;
                        FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);


                        
                    }
                }
            }
        }
        private void AddToBillClick(object sender, RoutedEventArgs args)
        {
            OrdersUserControl.AddToOrder(SelectedProduct,((int)QuanlityBox.Value), NoteTextBox.Text);
            CenteredFlyout.Hide();
            QuanlityBox.Value = 1;
        }

       
        //Dialog
        //private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var dialog = new ContentDialog();
        //    dialog.XamlRoot = this.XamlRoot;
        //    await dialog.ShowAsync();
        //}
        //================================================================================================
        //Load data from invoice to order more dishes
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is InvoiceToOrderObject)
            {
                var cart = e.Parameter as InvoiceToOrderObject;
                OrdersUserControl.ViewModel.InvoiceID = cart.InvoiceId;
                OrdersUserControl.ViewModel.InvoiceDate = DateTime.Now;
                foreach (var item in cart.InvoiceDetailToCartItemObjects)
                {
                    OrdersUserControl.AddToOrder(item.Product, item.Quantity, item.Note);
                }
            }
        }
    }
}
