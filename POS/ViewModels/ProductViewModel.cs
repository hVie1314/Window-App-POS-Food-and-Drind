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

        // Cache all products and the filtered products
        private ObservableCollection<Product> _allProducts;
        public ObservableCollection<Product> Products { get; private set; }

        public string _searchText;
        public string _selectedCategory = "";
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
                CurrentPage, RowsPerPage, Keyword,_selectedCategory, _selectedSortOrder);
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

        //public void LoadProducts()
        //{
        //    Tuple<int, List<Product>> productsFromDb = _productDao.GetAllProducts();
        //    _allProducts = new ObservableCollection<Product>(productsFromDb.Item2);
        //    Products = new ObservableCollection<Product>(_allProducts);
        //    FilterAndSortProducts();
        //    TotalItems = totalItems;
        //    TotalPages = (TotalItems / RowsPerPage)
        //        + ((TotalItems % RowsPerPage == 0)
        //                ? 0 : 1);
        //}

        // Property for search text
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText)); 
                    LoadProducts(1);
                    // Notify UI to update
                    //FilterAndSortProducts();
                }
            }
        }

        // Property for selected category
        //public string SelectedCategory
        //{
        //    get => _selectedCategory;
        //    set
        //    {
        //        if (_selectedCategory != value)
        //        {
        //            _selectedCategory = value;
        //            //OnPropertyChanged(nameof(SelectedCategory));
        //            LoadProducts(1);
        //            //FilterAndSortProducts();
        //        }
        //    }
        //}

        // Property for sort order
        //public string SelectedSortOrder
        //{
        //    get => _selectedSortOrder;
        //    set
        //    {
        //        if (_selectedSortOrder != value)
        //        {
        //            _selectedSortOrder = value;
        //            OnPropertyChanged(nameof(SelectedSortOrder));
        //            FilterAndSortProducts();
        //        }
        //    }
        //}

        // Filter and sort products based on search text, category, and sort order
        //private void FilterAndSortProducts()
        //{
        //    var filteredProducts = _allProducts.AsEnumerable();

        //    // Category
        //    if (SelectedCategory != "Tất cả")
        //    {
        //        filteredProducts = filteredProducts.Where(p => p.Category == SelectedCategory);
        //    }

        //    // Search text
        //    if (!string.IsNullOrWhiteSpace(SearchText))
        //    {
        //        filteredProducts = filteredProducts
        //            .Where(p => p.Name.ToLower().Contains(SearchText.ToLower()));
        //    }

        //    // Sort order
        //    switch (SelectedSortOrder)
        //    {
        //        case "price_ascending":
        //            filteredProducts = filteredProducts.OrderBy(p => p.Price);
        //            break;
        //        case "price_descending":
        //            filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
        //            break;
        //        default:
        //            // Do nothing
        //            break;
        //    }

        //    // Clear and add filtered products to Products
        //    Products.Clear();
        //    foreach (var product in filteredProducts)
        //    {
        //        Products.Add(product);
        //    }
        //}

        // ICommand for changing category
        //public ICommand SetCategoryCommand => new RelayCommand<string>(category => SelectedCategory = category);

        // ICommand for changing sort order
        //public ICommand SetSortOrderCommand => new RelayCommand<string>(sortOrder => SelectedSortOrder = sortOrder);

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}