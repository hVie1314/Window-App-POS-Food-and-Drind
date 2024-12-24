using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace POS.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private PaymentViewModel _paymentViewModel;
        private readonly ApplicationDataContainer _localSettings;

        // Giá trị mặc định
        private readonly float defaultVAT = 10.0f;
        private readonly string defaultAccessKey = "F8BBA842ECF85";
        private readonly string defaultSecretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
        private readonly string defaultIpnUrl = "https://webhook.site/4cb43743-df24-494e-839d-c6cc184d872c";

        public SettingsViewModel()
        {
            _localSettings = ApplicationData.Current.LocalSettings;

            // Khởi tạo giá trị từ LocalSettings hoặc dùng mặc định
            VAT = GetSetting(nameof(VAT), defaultVAT);
            AccessKey = GetSetting(nameof(AccessKey), defaultAccessKey);
            SecretKey = GetSetting(nameof(SecretKey), defaultSecretKey);
            IpnUrl = GetSetting(nameof(IpnUrl), defaultIpnUrl);
        }

        // Thuộc tính có thông báo thay đổi
        private float _vat;
        public float VAT
        {
            get => _vat;
            set
            {
                _vat = value;
                OnPropertyChanged();
                SaveToLocalSetting(nameof(VAT), _vat);
            }
        }

        private string _accessKey;
        public string AccessKey
        {
            get => _accessKey;
            set
            {
                _accessKey = value;
                OnPropertyChanged();
                SaveToLocalSetting(nameof(AccessKey), _accessKey);
            }
        }

        private string _secretKey;
        public string SecretKey
        {
            get => _secretKey;
            set
            {
                _secretKey = value;
                OnPropertyChanged();
                SaveToLocalSetting(nameof(SecretKey), _secretKey);
            }
        }

        private string _ipnUrl;
        public string IpnUrl
        {
            get => _ipnUrl;
            set
            {
                _ipnUrl = value;
                OnPropertyChanged();
                SaveToLocalSetting(nameof(IpnUrl), _ipnUrl);
            }
        }

        // Hàm hỗ trợ
        private T GetSetting<T>(string key, T defaultValue)
        {
            if (_localSettings.Values[key] is T value)
                return value;

            return defaultValue;
        }

        private void SaveToLocalSetting(string key, object value)
        {
            _localSettings.Values[key] = value;
        }

        public void PaymentLoadLocalSettings()
        {
            _paymentViewModel = new PaymentViewModel();
            _paymentViewModel.LoadLocalSettings();
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
