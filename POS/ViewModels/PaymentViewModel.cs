using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Helpers;
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
        private IDiscountDao _discountDao = new PostgresDiscountDao();
        private FeistelCipher _feistelCipher = new FeistelCipher(8, "winui3_discount_key");

        public ObservableCollection<string> PaymentMethods { get; set; }
        public ObservableCollection<Order> Items { get; set; }
        public string SelectedPaymentMethod { get; set; }
        public float VAT { get; set; } = 10.0f;
        public int TotalCost { get; set; }
        public int TotalPayable { get; set; }
        public int InvoiceId { get; set; }

        private int _receivedAmount;
        private int _change;
        private string _discountCode;
        private int _discountValue = 0;
        private string _discountStatus = "";

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

        public string DiscountCode
        {
            get => _discountCode;
            set
            {
                if (_discountCode != value)
                {
                    _discountCode = value;
                    OnPropertyChanged(nameof(DiscountCode));
                    // Gọi hàm CheckDiscountCode khi DiscountCode thay đổi
                    CheckDiscountCode(_discountCode);
                }
            }
        }

        public int DiscountValue
        {
            get => _discountValue;
            set
            {
                if (_discountValue != value)
                {
                    _discountValue = value;
                    OnPropertyChanged(nameof(DiscountValue));
                    // Tính toán lại TotalPayable khi DiscountValue thay đổi
                    CalculateTotalPayment();
                }
            }
        }

        public string DiscountStatus
        {
            get => _discountStatus;
            set
            {
                if (_discountStatus != value)
                {
                    _discountStatus = value;
                    OnPropertyChanged(nameof(DiscountStatus));
                }
            }
        }

        public void CalculateTotalPayment()
        {
            TotalPayable = (int)(TotalCost + (TotalCost * (VAT / 100)) - DiscountValue);

            if (TotalPayable < 0)
            {
                TotalPayable = 0;
            }
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
                Tax = VAT,
                InvoiceDate = PaymentDate,
                PaymentMethod = SelectedPaymentMethod,
                Discount = DiscountValue
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

        public void ResetData()
        {
            Items.Clear();
            OnPropertyChanged(nameof(Items));
            TotalCost = 0;
            OnPropertyChanged(nameof(TotalCost));
            TotalPayable = 0;
            OnPropertyChanged(nameof(TotalPayable));
            ReceivedAmount = 0;
            OnPropertyChanged(nameof(ReceivedAmount));
            Change = 0;
            OnPropertyChanged(nameof(Change));
            DiscountValue = 0;
            OnPropertyChanged(nameof(DiscountValue));
            DiscountCode = "";
            OnPropertyChanged(nameof(DiscountCode));
            DiscountStatus = "";
            OnPropertyChanged(nameof(DiscountStatus));
        }

        public bool CheckDiscountCode(string discountCode)
        {
            List<int> discountValues = new List<int> { 10000, 20000, 50000, 100000, 200000, 500000 };
            try
            {
                int discountValue = _feistelCipher.Decrypt(discountCode);

                if (discountValues.Contains(discountValue))
                {
                    DiscountValue = discountValue;
                    DiscountStatus = "Đã áp dụng mã giảm giá.";
                    CalculateTotalPayment();
                    OnPropertyChanged(nameof(TotalPayable));
                    return true;
                }
                else
                {
                    DiscountValue = 0;
                    DiscountStatus = "Mã giảm giá không hợp lệ.";
                    CalculateTotalPayment();
                    return false;
                }
            }
            catch (Exception ex)
            {
                DiscountValue = 0;
                CalculateTotalPayment();
                System.Diagnostics.Debug.WriteLine($"Error decrypting code: {ex.Message}");
                return false;
            }
        }

        public void DeleteUsedDiscountCode()
        {
            _discountDao.RemoveDiscountByCode(DiscountCode);
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
