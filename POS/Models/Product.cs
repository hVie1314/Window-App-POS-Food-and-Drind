using POS.Interfaces;
using System.Collections.Generic;

namespace POS.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public string IsAvailable { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; }

        //public Product(int productId, string name, int price, string category, int stockQuantity, string isAvailable, string imagePath, string type)
        //{
        //    ProductID = productId;
        //    Name = name;
        //    Price = price;
        //    Category = category;
        //    StockQuantity = stockQuantity;
        //    IsAvailable = isAvailable;
        //    ImagePath = imagePath;
        //    Type = type;
        //}
    }
}
