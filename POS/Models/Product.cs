using System.ComponentModel;

namespace POS.Models
{
    public class Product: INotifyPropertyChanged
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
