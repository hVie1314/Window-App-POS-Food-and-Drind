using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Interfaces;
using POS.Models;
using POS.Services.DAO;

namespace POS.ViewModels
{

    /// <summary>
    /// View model for Payment
    /// </summary>
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private IInvoiceDao _invoiceDao = new PostgresInvoiceDao();
        private IInvoiceDetailDao _invoiceDetailDao = new PostgresInvoiceDetailDao();

        public ObservableCollection<string> PaymentMethods { get; set; }
        public ObservableCollection<Order> Items { get; set; }
        public string SelectedPaymentMethod { get; set; }
        public float VAT { get; set; } = 10.0f;
        public int TotalCost { get; set; }
        public int TotalPayable { get; private set; }
        public int Discount { get; set; } = 0;
        public int InvoiceId { get; set; }

        private int _receivedAmount;
        private int _change;

        public DateTime PaymentDate { get; set; }
        public string MomoAcountInfo { get; set; } = "HCMUS";
        public string MomoQRCodeImagePath { get; set; } = "ms-appx:///Assets/Image/MomoQR.jpg";
        public string Address { get; set; } = "227 Nguyễn Văn Cừ, Quận 5, TP.HCM";
        public string Email { get; set; } = "pos@gmail.com";
        public string PhoneNumber { get; set; } = "078.491.6454";


        public PaymentViewModel()
        {
            PaymentMethods = new ObservableCollection<string>
                {
                    "Tiền mặt",
                    "Momo"
                };
            SelectedPaymentMethod = "Tiền mặt";

            PaymentDate = DateTime.Now; // Ngày thanh toán hiện tại

        }

        public void SetItems(ObservableCollection<Order> items, double total)
        {
            Items = new ObservableCollection<Order>(items);
            OnPropertyChanged(nameof(Items));
            TotalCost = (int)total;
            OnPropertyChanged(nameof(TotalCost));
            CalculateTotalPayment();
            OnPropertyChanged(nameof(TotalPayable));
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


        // Save invoice an detail to database
        public int SaveToDB()
        {
            Invoice invoice = new Invoice()
            {
                TotalAmount = TotalCost,
                Tax = 10.00,
                InvoiceDate = PaymentDate,
                PaymentMethod = SelectedPaymentMethod

            };
            int newInvoiceId = _invoiceDao.InsertInvoice(invoice);

            foreach (var item in Items)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    InvoiceID = newInvoiceId,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                _invoiceDetailDao.InsertInvoiceDetail(invoiceDetail);
            }

            return newInvoiceId;
        }


        /// <summary>
        /// Sự kiện khi thuộc tính của ViewModel thay đổi.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Phương thức gọi sự kiện khi thuộc tính thay đổi.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
