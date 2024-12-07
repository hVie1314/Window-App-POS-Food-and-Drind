using System.ComponentModel;

namespace POS.Models
{
    public class PaymentModel : INotifyPropertyChanged
    {
        public int TotalBill { get; set; }
        public double VAT { get; set; }
        public int Discount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
