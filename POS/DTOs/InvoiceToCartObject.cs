using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DTOs
{
    public class InvoiceDetailToCartItemObject
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
