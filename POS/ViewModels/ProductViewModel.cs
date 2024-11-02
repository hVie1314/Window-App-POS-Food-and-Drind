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

namespace POS.ViewModels
{
    public sealed partial class ProductViewModel : INotifyPropertyChanged
    {
        private IProductDao _productDao = new PostgresProductDao(); // Use MockDao for testing

        // Cache all products and the filtered products
        private ObservableCollection<Product> _allProducts;
        public ObservableCollection<Product> Products { get; private set; }

        private string _searchText;
        private string _selectedCategory = "Tất cả";
        private string _selectedSortOrder = "default";

        public ProductViewModel()
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            Tuple<int, List<Product>> productsFromDb = _productDao.GetAllProducts();
            _allProducts = new ObservableCollection<Product>(productsFromDb.Item2);
            Products = new ObservableCollection<Product>(_allProducts);
            FilterAndSortProducts();
        }

        // Property for search text
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText)); // Notify UI to update
                    FilterAndSortProducts();
                }
            }
        }

        // Property for selected category
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    FilterAndSortProducts();
                }
            }
        }

        // Property for sort order
        public string SelectedSortOrder
        {
            get => _selectedSortOrder;
            set
            {
                if (_selectedSortOrder != value)
                {
                    _selectedSortOrder = value;
                    OnPropertyChanged(nameof(SelectedSortOrder));
                    FilterAndSortProducts();
                }
            }
        }

        // Filter and sort products based on search text, category, and sort order
        private void FilterAndSortProducts()
        {
            var filteredProducts = _allProducts.AsEnumerable();

            // Category
            if (SelectedCategory != "Tất cả")
            {
                filteredProducts = filteredProducts.Where(p => p.Category == SelectedCategory);
            }

            // Search text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Name.ToLower().Contains(SearchText.ToLower()));
            }

            // Sort order
            switch (SelectedSortOrder)
            {
                case "price_ascending":
                    filteredProducts = filteredProducts.OrderBy(p => p.Price);
                    break;
                case "price_descending":
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
                    break;
                default:
                    // Do nothing
                    break;
            }

            // Clear and add filtered products to Products
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }

        // ICommand for changing category
        public ICommand SetCategoryCommand => new RelayCommand<string>(category => SelectedCategory = category);

        // ICommand for changing sort order
        public ICommand SetSortOrderCommand => new RelayCommand<string>(sortOrder => SelectedSortOrder = sortOrder);

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}