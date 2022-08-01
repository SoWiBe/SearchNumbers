using Microsoft.Win32;
using SearchNumbers.Core;
using SearchNumbers.Excel;
using SearchNumbers.Models;
using SearchNumbers.MVVM.Models;
using SearchNumbers.Parsers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SearchNumbers.ViewModels
{
    public class SearchNumbersModel : ObservableObject
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<Account> AdverTitles { get; set; }
        public ObservableCollection<Advertisement> Advertisements { get; set; }

        private OpenFileDialog openFileDialog;

        public ICommand ChooseFileCommand { get; set; }
        public ICommand SearchPhoneNumbersCommand { get; set; }

        private string _searchText;
        public string SearchText { get { return _searchText; } set { _searchText = value; OnPropertyChanged(); } }

        private string _minPrice;
        public string MinPrice { get { return _minPrice; } set { _minPrice = value; OnPropertyChanged(); } }

        private string _maxPrice;
        public string MaxPrice { get { return _maxPrice; } set { _maxPrice = value; OnPropertyChanged(); } }

        public SearchNumbersModel()
        {
            Accounts = new ObservableCollection<Account>();
            Cities = new ObservableCollection<City>();
            Advertisements = new ObservableCollection<Advertisement>();

            openFileDialog = new OpenFileDialog();

            ChooseFileCommand = new RelayCommand(o => ChooseFile());
            SearchPhoneNumbersCommand = new RelayCommand(o => SearchPhoneNumbers());
        }
        
        private async void ChooseFile()
        {
            string path = "";

            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;

            SearchText = path;

            await ParserAccounts.GetAccountsAsync(path, Accounts);
        }

        private void SearchPhoneNumbers()
        {
            //ExcelService.WritePhoneNumber("+78005553535");
            if (ValidatePrice())
            {
                ObservableCollection<Advertisement> advertisements = ExcelService.GetAdvertisements(Int32.Parse(MinPrice), Int32.Parse(MaxPrice));
                for (int i = 0; i < advertisements.Count(); i++)
                {
                    Advertisements.Add(advertisements[i]);
                }
            }
            
            else MessageBox.Show("Максимальная сумма должна быть больше минимальной!");
        }

        private bool ValidatePrice()
        {
            int min = Int32.Parse(MinPrice);
            int max = Int32.Parse(MaxPrice);

            if (max < min) return false;

            return true;
        }
    }
}
