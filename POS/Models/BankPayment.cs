﻿using System.ComponentModel;

namespace POS.Models
{
    public class BankPayment : INotifyPropertyChanged
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
