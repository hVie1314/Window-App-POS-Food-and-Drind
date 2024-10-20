using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using POS.Models;
using POS.Services;
using POS.Helpers;
using System.ComponentModel;

namespace POS.ViewModels
{
    public sealed partial class ProductViewModel : INotifyPropertyChanged
    {
        private MockDao _productDao = new MockDao(); // Use MockDao for testing
        private string _searchText;

        // Cache for all products from the database
        private ObservableCollection<Product> _allProducts;

        public ObservableCollection<Product> Products { get; private set; }

        // The ICommand property for the search button
        public ICommand SearchCommand { get; }

        public ProductViewModel()
        {
            // Initialize command
            SearchCommand = new RelayCommand(SearchProducts, CanSearchProducts);
            LoadProducts();
        }

        private void LoadProducts()
        {
            var productsFromDb = _productDao.GetAll();
            _allProducts = new ObservableCollection<Product>(productsFromDb); // Cache the original list
            Products = new ObservableCollection<Product>(_allProducts); // Display all products initially
        }

        // Property to bind to the TextBox for search input
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText)); // Notify UI
                ((RelayCommand)SearchCommand).RaiseCanExecuteChanged(); // Raise CanExecuteChanged when the search text is updated
            }
        }

        // Command execution logic
        private void SearchProducts(object parameter)
        {
            var filteredProducts = _allProducts
                .Where(p => p.Name.ToLower().Contains(SearchText.ToLower()))
                .ToList();

            // Clear the current list and add the filtered products
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }

        // CanExecute logic to enable or disable the search button based on SearchText
        private bool CanSearchProducts(object parameter)
        {
            // The line below has bug cause crash program. Will be fixed later (if have enough time :v)
            //return !string.IsNullOrEmpty(SearchText); 

            return true;
        }

        // OnPropertyChanged implementation for notifying the UI about property changes
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
