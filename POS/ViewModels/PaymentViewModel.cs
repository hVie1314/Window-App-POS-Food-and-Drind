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

    /// <summary>
    /// ViewModel quản lý logic và dữ liệu của giao diện thanh toán.
    /// </summary>
    public class PaymentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Danh sách các phương thức thanh toán.
        /// </summary>
        public ObservableCollection<string> PaymentMethods { get; set; }

        /// <summary>
        /// Phương thức thanh toán được chọn.
        /// </summary>
        public string SelectedPaymentMethod { get; set; }

        /// <summary>
        /// Tổng tiền cần thanh toán.
        /// </summary>
        public int TotalCost { get; set; }

        /// <summary>
        /// Thuế VAT.
        /// </summary>
        public float VAT { get; set; } = 10.0f;

        /// <summary>
        /// Giảm giá.
        /// </summary>
        public int Discount { get; set; } = 0;

        /// <summary>
        /// Tổng tiền cần thanh toán sau khi đã tính thuế và giảm giá.
        /// </summary>
        public int TotalPayable { get; private set; }

        /// <summary>
        /// Thông tin tài khoản Momo.
        /// </summary>
        public string MomoAcountInfo { get; set; } = "HCMUS";

        /// <summary>
        /// Đường dẫn ảnh mã QR của Momo.
        /// </summary>
        public string MomoQRCodeImagePath { get; set; } = "ms-appx:///Assets/Image/MomoQR.jpg";

        /// <summary>
        /// Số tiền khách hàng trả.
        /// </summary>
        private int _receivedAmount;

        /// <summary>
        /// Số tiền thừa hoặc thiếu sau khi đã thanh toán.
        /// </summary>
        private int _change;

        /// <summary>
        /// ID hóa đơn.
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// Ngày thanh toán.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Địa chỉ cửa hàng.
        /// </summary>
        public string Address { get; set; } = "227 Nguyễn Văn Cừ, Quận 5, TP.HCM";

        /// <summary>
        /// Email cửa hàng.
        /// </summary>
        public string Email { get; set; } = "pos@gmail.com";

        /// <summary>
        /// Số điện thoại cửa hàng.
        /// </summary>
        public string PhoneNumber { get; set; } = "078.491.6454";

        /// <summary>
        /// Danh sách sản phẩm trong hóa đơn.
        /// </summary>
        public ObservableCollection<InvoiceItem> InvoiceItems { get; set; }

        /// <summary>
        /// Khởi tạo ViewModel với dữ liệu mẫu.
        /// </summary>
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

        /// <summary>
        /// Tính tổng tiền cần thanh toán sau khi đã tính thuế và giảm giá.
        /// </summary>
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

        /// <summary>
        /// Tính số tiền thừa hoặc thiếu sau khi đã thanh toán.
        /// </summary>
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

        /// <summary>
        /// Tính tổng tiền cần thanh toán sau khi đã tính thuế và giảm giá.
        /// </summary>
        public void CalculateTotalPayment()
        {
            TotalPayable = (int)(TotalCost + (TotalCost * (VAT / 100)) - Discount);
        }

        /// <summary>
        /// Tính số tiền thừa hoặc thiếu sau khi đã thanh toán.
        /// </summary>
        public void CalculateChange()
        {
            if (ReceivedAmount < TotalPayable)
            {
                Change = 0;
                return;
            }
            Change = ReceivedAmount - TotalPayable;
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
