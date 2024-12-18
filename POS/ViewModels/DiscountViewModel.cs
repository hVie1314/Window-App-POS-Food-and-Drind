using System.Collections.ObjectModel;
using System.ComponentModel;
using POS.Models;
using POS.Services.DAO;
using POS.Interfaces;
using System;
using POS.Helpers;


namespace POS.ViewModels
{
    public sealed partial class DiscountViewModel : INotifyPropertyChanged
    {
        private IDiscountDao _discountDao = new PostgresDiscountDao();
        public ObservableCollection<Discount> Discounts { get; private set; }
        private FeistelCipher _feistelCipher = new FeistelCipher(8, "winui3_discount_key");


        public int CurrentPage { get; set; } = 1;
        public int RowsPerPage { get; set; } = 10;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;


        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private Discount _selectedDiscount;
        public Discount SelectedDiscount
        {
            get { return _selectedDiscount; }
            set
            {
                if (_selectedDiscount != value)
                {
                    _selectedDiscount = value;
                    OnPropertyChanged(nameof(SelectedDiscount));
                }
            }
        }

        public DiscountViewModel()
        {
            Discounts = new ObservableCollection<Discount>();
            GetAllDiscount();
        }

        public void GetAllDiscount()
        {
            var (totalItems, discounts) = _discountDao.GetAllDiscount(CurrentPage, RowsPerPage);

            Discounts.Clear();
            foreach (var discount in discounts)
            {
                Discounts.Add(discount);
            }

            TotalItems = totalItems;
            TotalPages = (TotalItems / RowsPerPage) + ((TotalItems % RowsPerPage == 0) ? 0 : 1);

            OnPropertyChanged(nameof(Discounts));
            OnPropertyChanged(nameof(TotalPages));
        }

        public void LoadDiscounts(int page)
        {
            if (page >= 1 && page <= TotalPages)
            {
                CurrentPage = page;
                GetAllDiscount();
                OnPropertyChanged(nameof(CurrentPage));
            }
        }


        public bool CreateDiscounts(int value, int quantity)
        {
            bool result = true;

            for (int i = 0; i < quantity; i++)
            {
                string discountCode = _feistelCipher.Encrypt(Value);
                if (_discountDao.InsertDiscount(discountCode, value) < 0)
                {
                    result = false;
                }
            }
            GetAllDiscount();
            OnPropertyChanged(nameof(Discounts));

            return result;
        }

        public bool DeleteDiscount(Discount discount)
        {
            try
            {
                _discountDao.RemoveDiscountByCode(discount.DiscountCode);
                Discounts.Remove(discount);
                OnPropertyChanged(nameof(Discounts));

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting discount: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sự kiện khi thuộc tính thay đổi.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Phát sự kiện khi thuộc tính thay đổi.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}