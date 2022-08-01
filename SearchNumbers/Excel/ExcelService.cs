using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IronXL;
using SearchNumbers.MVVM.Models;
using ExcelMain = Microsoft.Office.Interop.Excel;

namespace SearchNumbers.Excel
{
    public class ExcelService
    {
        public static async Task<ObservableCollection<City>> GetAllCities(ObservableCollection<City> cities)
        {
            WorkSheet ws = InitializeWorksheet();

            return cities;
        }

        public static ObservableCollection<Advertisement> GetAdvertisements(int minPrice, int maxPrice)
        {
            WorkSheet ws = InitializeWorksheet();

            ObservableCollection<Advertisement> advertisements = new ObservableCollection<Advertisement>();

            for (int y = 2; y <= 306; y++)
            {
                var cells = ws[$"A{y}:F{y}"].ToList();
                if (Int32.Parse(cells[2].Value.ToString()) < maxPrice && Int32.Parse(cells[2].Value.ToString()) > minPrice)
                {
                    advertisements.Add(new Advertisement
                    {
                        Price = cells[2].Value.ToString(),
                        Title = cells[1].Value.ToString(),
                        DatePublished = cells[5].Value.ToString()
                    });
                }
            }
            return advertisements;
        }

        private static WorkSheet InitializeWorksheet()
        {
            var workbook = WorkBook.LoadCSV(@"C:/Users/fordd/Desktop/result [14-06-2022-16-25-37].csv", fileFormat: ExcelFileFormat.XLSX, ListDelimiter: ";");
            WorkSheet ws = workbook.DefaultWorkSheet;
            return ws;
        }

        public static void WritePhoneNumber(string phoneNumber)
        {
            ExcelMain.Application application = new ExcelMain.Application
            {
                Visible = false,
                SheetsInNewWorkbook = 1
            };

            ExcelMain.Workbook workbook = application.Workbooks.Add(Type.Missing);
            application.DisplayAlerts = false;
            ExcelMain.Worksheet sheet = application.Worksheets.get_Item(1);
            sheet.Name = "Phones";

            sheet.Range["A1"].Value = phoneNumber;

            application.Application.ActiveWorkbook.SaveAs("MyPhones.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                                                            ExcelMain.XlSaveAsAccessMode.xlNoChange,
                                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            application.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
        }
    }
}
