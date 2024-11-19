using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using POS.Models;
using POS.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using POS.Interfaces;
using System.Collections.Generic;
using System;
using POS.Views;
using Microsoft.UI.Xaml.Controls;


namespace POS.ViewModels
{
    public sealed partial class ProductViewModel : INotifyPropertyChanged
    {
        private IProductDao _productDao = new PostgresProductDao();
        public ObservableCollection<Product> Products { get; private set; }

        public string _searchText;
        public string _selectedCategory = "Tất cả";
        public int _selectedSortOrder = 0;


        public string Keyword { get; set; } = "";
        public bool NameAcending { get; set; } = false;
        public int CurrentPage { get; set; } = 1;
        public int RowsPerPage { get; set; } = 5;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;

        //=======================================================================================================
        //Phân trang
        //void UpdatePagingInfo_bootstrap()
        //{
        //    var infoList = new List<object>();
        //    for (int i = 1; i <= TotalPages; i++)
        //    {
        //        infoList.Add(new
        //        {
        //            Page = i,                    // Giá trị của Page cho mỗi trang
        //            Total = TotalPages  // Tổng số trang
        //        });
        //    }

        //    pagesComboBox.ItemsSource = infoList; // Gán danh sách cho ComboBox
        //    pagesComboBox.SelectedIndex = 0;
        //}

        //=======================================================================================================
        public ProductViewModel()
        {
            _productDao = new PostgresProductDao();
            GetAllProducts();
        }

        public void GetAllProducts()
        {
            var (totalItems, products) = _productDao.GetAllProducts(
                CurrentPage, RowsPerPage, Keyword, _selectedCategory, _selectedSortOrder);
            Products = new FullObservableCollection<Product>(
                products
            );

            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage)
                + ((TotalItems % RowsPerPage == 0)
                        ? 0 : 1);
        }

        public void LoadProducts(int page)
        {
            CurrentPage = page;
            GetAllProducts();
        }


        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}