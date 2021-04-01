using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathOfExileHelper.Buttons.SearchInChat
{
    public class SearchParameters : INotifyPropertyChanged
    {
        private string _orText = string.Empty;
        public string OrText {
            get
            {
                return _orText;
            }
            set
            {
                _orText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrText"));
            }
        }

        private string _andOrText = string.Empty;
        public string AndOrText
        {
            get
            {
                return _andOrText;
            }
            set
            {
                _andOrText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AndOrText"));
            }
        }

        private string _notText = string.Empty;

        public string NotText
        {
            get
            {
                return _notText;
            }
            set
            {
                _notText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NotText"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchParameters()
        {

        }

        public void Clear()
        {
            OrText = string.Empty;
            AndOrText = string.Empty;
            NotText = string.Empty;
        }
    }
}
