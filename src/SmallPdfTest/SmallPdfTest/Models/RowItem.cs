using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SmallPdfTest.Annotations;

namespace SmallPdfTest.Models
{
    public class RowItem : INotifyPropertyChanged
    {
        private decimal? amount;
        private decimal? convertedValue;
        private string from;
        private string to;

        public string From
        {
            get => from;
            set
            {
                if (value == from) return;
                from = value;
                OnPropertyChanged();
            }
        }

        public string To
        {
            get => to;
            set
            {
                if (value == to) return;
                to = value;
                OnPropertyChanged();
            }
        }

        public decimal? Amount
        {
            get => amount;
            set
            {
                if (value == amount) return;
                amount = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public decimal? ConvertedValue
        {
            get => convertedValue;
            set
            {
                if (value == convertedValue) return;
                convertedValue = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}