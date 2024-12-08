using System;

namespace POS.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public double Discount { get; set; } = 0;
        public double Tax { get; set; } = 10;
        public string Note { get; set; }
    }
}
