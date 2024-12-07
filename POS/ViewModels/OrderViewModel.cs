using POS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModels
{
    
    public class OrderDetailViewModel
    {
        public FullObservableCollection<Order> Items { get; set; }
            = new FullObservableCollection<Order>();

        public void Add(Product info)
        {
            var foundItem = Items.FirstOrDefault(item => item.Name == info.Name);
            if (foundItem != null)
            {
                foundItem.Quantity += 1;
            }
            else
            {
                Items.Add(new Order(info));
            }
        }
    }
}
