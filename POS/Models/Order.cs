using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class Order : Product, INotifyPropertyChanged
    {
        private int _quantity;
        private int _total;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public int Total
        {
            get => Quantity * Price;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public Order(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Quantity = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
