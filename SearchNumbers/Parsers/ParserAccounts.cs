using SearchNumbers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchNumbers.Parsers
{
    public class ParserAccounts
    {
        public static async Task<ObservableCollection<Account>> GetAccountsAsync(string path, ObservableCollection<Account> accounts)
        {
            string line = "";
            using (StreamReader reader = new StreamReader(path))
            {
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] data = line.Split(' ');
                    accounts.Add(new Account { Name = data[0], Password = data[1] });
                }
            }
            return accounts;
        }
    }
}
