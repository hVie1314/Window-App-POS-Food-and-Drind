using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModels
{
    // temporary class
    public class InvoiceItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
    public class PaymentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> PaymentMethods { get; set; }
        public string SelectedPaymentMethod { get; set; }
        public int TotalCost { get; set; }
        public float VAT { get; set; } = 10.0f;
        public int Discount { get; set; } = 0;
        public int TotalPayable { get; private set; }
        public string MomoAcountInfo { get; set; } = "HCMUS";
        public string MomoQRCodeImagePath { get; set; } = "ms-appx:///Assets/Image/MomoQR.jpg";

        private int _receivedAmount;
        private int _change;

        /// tmp code
        public string InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Address { get; set; } = "227 Nguyễn Văn Cừ, Quận 5, TP.HCM";
        public string Email { get; set; } = "pos@gmail.com";
        public string PhoneNumber { get; set; } = "078.491.6454";
        public ObservableCollection<InvoiceItem> InvoiceItems { get; set; }

        public PaymentViewModel()
        {
            PaymentMethods = new ObservableCollection<string>
                {
                    "Tiền mặt",
                    "Momo"
                };
            SelectedPaymentMethod = "Tiền mặt";

            // Sample values, will be replaced by actual values in database
            TotalCost = 300000;
            CalculateTotalPayment();


            /// temporary code
            InvoiceId = "HD001"; // ID mẫu
            PaymentDate = DateTime.Now; // Ngày thanh toán hiện tại

            // Danh sách sản phẩm mẫu
            InvoiceItems = new ObservableCollection<InvoiceItem>
            {
                new InvoiceItem { Name = "Sản phẩm 1", Quantity = 1, Price = 100000 },
                new InvoiceItem { Name = "Sản phẩm 2", Quantity = 2, Price = 200000 }
            };
        }

        public int ReceivedAmount
        {
            get => _receivedAmount;
            set
            {
                if (_receivedAmount != value)
                {
                    _receivedAmount = value;
                    OnPropertyChanged(nameof(ReceivedAmount));
                    CalculateChange();
                }
            }
        }

        public int Change
        {
            get => _change;
            private set
            {
                if (_change != value)
                {
                    _change = value;
                    OnPropertyChanged(nameof(Change));
                }
            }
        }

        public void CalculateTotalPayment()
        {
            TotalPayable = (int)(TotalCost + (TotalCost * (VAT / 100)) - Discount);
        }

        public void CalculateChange()
        {
            if (ReceivedAmount < TotalPayable)
            {
                Change = 0;
                return;
            }
            Change = ReceivedAmount - TotalPayable;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
