using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using POS.Models;
using POS.Services;

namespace POS.ViewModels
{
    public sealed partial class ProductViewModel : Page
    {
        public ObservableCollection<Product> Products { get; set; }
        //private ProductDao productDao;
        private MockDao productDao = new MockDao(); // Use MockDao for testing

        // Constructor
        public ProductViewModel()
        {
            //productDao = new ProductDao(); 
            productDao = new MockDao(); // Use MockDao for testing
            LoadProducts();
        }

        private void LoadProducts()
        {
            // Get all products from database
            var productsFromDb = productDao.GetAll();
            Products = new ObservableCollection<Product>(productsFromDb); // Constructor of ObservableCollection will convert IEnumerable to ObservableCollection
        }
    }
}