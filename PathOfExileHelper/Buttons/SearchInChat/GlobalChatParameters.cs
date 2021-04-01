using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathOfExileHelper.Buttons.SearchInChat
{
    public class GlobalChatParameters : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _globalChannel = "820";
        public string GlobalChannel
        {
            get
            {
                return _globalChannel;
            }
            set
            {
                _globalChannel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GlobalChannel"));
            }
        }

        private bool _autoJoin = true;
        public bool AutoJoin
        {
            get
            {
                return _autoJoin;
            }
            set
            {
                _autoJoin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AutoJoin"));
            }
        }
    }
}
