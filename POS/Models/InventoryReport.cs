using System;

namespace POS.Models
{
    public class InventoryReport
    {
        public int ReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public int IngredientID { get; set; }
        public int RemainingQuantity { get; set; }
    }
}
