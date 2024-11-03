using System;

namespace POS.Models
{
    public class Warehouse
    {
        public int WarehouseID { get; set; }
        public string IngredientName { get; set; }
        public int StockQuantity { get; set; }
        public string Unit { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
