using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using POS.Helpers;
using POS.Interfaces;
using POS.Models;
using POS.Services.DAO;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

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
        private SettingsViewModel _settingsViewModel = new SettingsViewModel();
        private FeistelCipher _feistelCipher = new FeistelCipher(8, "winui3_discount_key");
        private static readonly HttpClient client = new HttpClient();
        public ObservableCollection<string> PaymentMethods { get; set; }
        public ObservableCollection<Order> Items { get; set; }

        public string SelectedPaymentMethod { get; set; }
        public float VAT { get; set; }
        public int TotalCost { get; set; }
        public int TotalPayable { get; set; }
        public int InvoiceId { get; set; }

        public DateTime PaymentDate { get; set; }
        public string accessKey { get; set; }
        public string secretKey { get; set; }
        public string ipnUrl { get; set; }

        private float _vat;

        private int _receivedAmount;
        private int _change;
        private string _discountCode;
        private int _discountValue = 0;
        private string _discountStatus = "";


        ////MoMo API config infomation
        //string accessKey = "F8BBA842ECF85"; // change your business access key here
        //string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz"; // change your business secret key here
        //string ipnUrl = "https://webhook.site/4cb43743-df24-494e-839d-c6cc184d872c"; // change your business ipnUrl here


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

        // Save invoice an detail to database
        public int SaveToDB()
        {
            Invoice invoice = new Invoice()
            {
                TotalAmount = TotalPayable,
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

        private static String getSignature(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public async Task<string> RequestMoMoPayment()
        {
            // Generate UUID for requestId and orderId
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            QuickPayResquest request = new QuickPayResquest();
            request.orderInfo = "Thanh_toan_hoa_don_POS_HCMUS";
            request.partnerCode = "MOMO";
            request.redirectUrl = "";
            request.ipnUrl = "https://webhook.site/4cb43743-df24-494e-839d-c6cc184d872c";
            request.amount = (long)TotalPayable;
            request.orderId = myuuidAsString;
            request.requestId = myuuidAsString;
            request.extraData = "";
            request.partnerName = "MOMO";
            request.storeId = "POS HCMUS";
            request.orderGroupId = "";
            request.autoCapture = true;
            request.lang = "vi"; // en or vi
            request.requestType = "captureWallet"; // captureWallet or captureMoMo

            var rawSignature = "accessKey=" + accessKey +
                               "&amount=" + request.amount +
                               "&extraData=" + request.extraData +
                               "&ipnUrl=" + request.ipnUrl +
                               "&orderId=" + request.orderId +
                               "&orderInfo=" + request.orderInfo +
                               "&partnerCode=" + request.partnerCode +
                               "&redirectUrl=" + request.redirectUrl +
                               "&requestId=" + request.requestId +
                               "&requestType=" + request.requestType;
            request.signature = getSignature(rawSignature, secretKey);

            // Call MoMo API
            StringContent httpContent = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
            var quickPayResponse = await client.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);

            // Read response
            var contents = await quickPayResponse.Content.ReadAsStringAsync();
            JObject jMessage = JObject.Parse(contents);
            //System.Diagnostics.Debug.WriteLine("Response: " + jMessage);
            //System.Diagnostics.Debug.WriteLine("payUrl: " + jMessage["payUrl"].ToString());
            
            // Return the payUrl
            return jMessage["payUrl"].ToString();
        }

        public void LoadLocalSettings()
        {
            VAT = _settingsViewModel.VAT;
            OnPropertyChanged(nameof(VAT));
            accessKey = _settingsViewModel.AccessKey;
            secretKey = _settingsViewModel.SecretKey;
            ipnUrl = _settingsViewModel.IpnUrl;
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
