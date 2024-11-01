//using System.Collections.Generic;
//using POS.Interfaces;
//using POS.Models;
//using System;
//using Windows.System;

//namespace POS.Services
//{
//    public class MockDao : IDao<Product>
//    {
//        public IEnumerable<Product> GetAll()
//        {
//            var products = new List<Product>
//            {
//                new Product { ProductID = 1, Name = "Gỏi cuốn", Price = 30000, Category = "Khai vị", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 2, Name = "Phở bò", Price = 50000, Category = "Món chính", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 3, Name = "Bánh mì", Price = 25000, Category = "Món chính", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 4, Name = "Cơm tấm", Price = 40000, Category = "Món chính", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 5, Name = "Bún chả", Price = 60000, Category = "Món chính", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 6, Name = "Mì quảng", Price = 55000, Category = "Món chính", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 7, Name = "Trái cây tươi", Price = 15000, Category = "Tráng miệng", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 8, Name = "Trái cây dầm", Price = 30000, Category = "Tráng miệng", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 9, Name = "Kem ốc quế", Price = 25000, Category = "Tráng miệng", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 10, Name = "Sinh tố bơ", Price = 20000, Category = "Nước giải khát", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 11, Name = "Nước mía", Price = 15000, Category = "Nước giải khát", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 12, Name = "Nước ép", Price = 20000, Category = "Nước giải khát", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 13, Name = "Trà sữa", Price = 30000, Category = "Nước giải khát", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"},
//                new Product { ProductID = 14, Name = "Bia", Price = 20000, Category = "Đồ uống có cồn", StockQuantity = 100, IsAvailable = "Y", ImagePath = "ms-appx:///Assets/Image/pho_bo.jpg"}
//            };

//            return products;
//        }

//        public Product GetById(int id)
//        {
//            Product product = null;

//            return product;
//        }

//        public void Add(Product product)
//        {

//        }

//        public void Update(Product product)
//        {

//        }

//        public void Delete(int id)
//        {

//        }
//    }
//}