using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using POS.Models;
using POS.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace POS.ViewModels
{
    public sealed partial class ProductViewModel : INotifyPropertyChanged
    {
        private MockDao _productDao = new MockDao(); // Sử dụng MockDao cho testing

        // Bộ nhớ đệm cho tất cả sản phẩm từ cơ sở dữ liệu
        private ObservableCollection<Product> _allProducts;
        public ObservableCollection<Product> Products { get; private set; }

        private string _searchText;
        private string _selectedCategory = "Tất cả";
        private string _selectedSortOrder = "default";

        public ProductViewModel()
        {
            // Khởi tạo lệnh
            LoadProducts();
        }

        private void LoadProducts()
        {
            var productsFromDb = _productDao.GetAll();
            _allProducts = new ObservableCollection<Product>(productsFromDb); // Bộ nhớ đệm ban đầu
            Products = new ObservableCollection<Product>(_allProducts); // Hiển thị tất cả sản phẩm ban đầu
            FilterAndSortProducts(); // Lọc và sắp xếp sản phẩm
        }

        // Thuộc tính để liên kết với AutoSuggestBox cho tìm kiếm
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText)); // Thông báo UI
                    FilterAndSortProducts(); // Gọi lọc và sắp xếp khi thay đổi văn bản
                }
            }
        }

        // Thuộc tính cho category
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

        // Thuộc tính cho sort order
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

        // Lọc và sắp xếp sản phẩm
        private void FilterAndSortProducts()
        {
            var filteredProducts = _allProducts.AsEnumerable();

            // Lọc theo category
            if (SelectedCategory != "Tất cả")
            {
                filteredProducts = filteredProducts.Where(p => p.Category == SelectedCategory);
            }

            // Lọc theo văn bản tìm kiếm
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Name.ToLower().Contains(SearchText.ToLower()));
            }

            // Sắp xếp sản phẩm
            switch (SelectedSortOrder)
            {
                case "price_ascending":
                    filteredProducts = filteredProducts.OrderBy(p => p.Price);
                    break;
                case "price_descending":
                    filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
                    break;
                default:
                    // Mặc định không sắp xếp
                    break;
            }

            // Cập nhật danh sách sản phẩm hiển thị
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }

        // ICommand cho việc thay đổi category
        public ICommand SetCategoryCommand => new RelayCommand<string>(category => SelectedCategory = category);

        // ICommand cho việc thay đổi thứ tự sắp xếp
        public ICommand SetSortOrderCommand => new RelayCommand<string>(sortOrder => SelectedSortOrder = sortOrder);

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
