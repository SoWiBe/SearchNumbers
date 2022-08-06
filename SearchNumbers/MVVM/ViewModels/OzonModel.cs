using SearchNumbers.Core;
using SearchNumbers.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SearchNumbers.MVVM.ViewModels
{
    internal class OzonModel : ObservableObject
    {
        private string _info;
        public string Info
        {
            get { return _info; }
            set { _info = value; OnPropertyChanged(); }
        }
        public ICommand CommandGetInfo { get; set; }
        public OzonModel()
        {
            CommandGetInfo = new RelayCommand(r => GetInfo());
        }

        private async void GetInfo()
        {
            Info = await ParserOzon.GetInfo();
        }
    }
}
