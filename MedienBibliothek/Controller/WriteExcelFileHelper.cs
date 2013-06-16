using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using MedienBibliothek.Model;
using Microsoft.Office.Interop.Excel;

namespace MedienBibliothek.Controller
{
    class WriteExcelFileHelper
    {
//        public Application ExcelWriter = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Application _excelWriterApp = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Worksheet _wsheet;
        Microsoft.Office.Interop.Excel.Workbook _wbook;
        public void WriteVideoListToExcelFile(IEnumerable<Video> videoList)
        {
            
            if (!File.Exists(Properties.Settings.Default.excelPath))
            {
                var myExcelFile = File.Create(Properties.Settings.Default.excelPath);
                myExcelFile.Close();
            }
            _excelWriterApp.Visible = false;
            _excelWriterApp.DisplayAlerts = false;
//            var workbook = ExcelWriter.Workbooks.Add(1);
//            var worksheet = (Worksheet) workbook.Sheets[1];
//            ExcelWriter.Worksheets.Add(1);
//            workbook.Open(Properties.Settings.Default.excelPath);
            _wbook = _excelWriterApp.Workbooks.Add(true);
            _wsheet = (Worksheet)_wbook.ActiveSheet;
//            _wbook.Open(Properties.Settings.Default.excelPath);

            WriteVideoList(videoList);
        }

        private void WriteVideoList(IEnumerable<Video> videoList)
        {
            int counter = 2;
            foreach (var video in videoList)
            {
                WriteValueInCell(1, counter, video.Name);
                WriteValueInCell(2, counter, video.Quality);
                WriteValueInCell(3, counter, video.Path);
                counter++;
            }
//            _excelWriterApp.Save(Properties.Settings.Default.excelPath);
            _wbook.SaveAs(Properties.Settings.Default.excelPath);
//            _wbook.Save();
            _wbook.Close();
//            _excelWriterApp.Workbooks.Close();
            
//            ExcelWriter.Workbooks.Close();
//            ExcelWriter.Save("VideoList.xls");
            
//            ExcelWriter.Workbooks.Close();
//            ExcelWriter.SaveAs("VideoList.xls");
//            ExcelWriter.
//            ExcelWriter.Save(Properties.Settings.Default.excelPath);
//            ExcelWriter.Sa
//            ExcelWriter.Workbooks.Close();
//            ExcelWriter.Quit();
        }

        private void WriteValueInCell(int column, int row, string value)
        {
            _wsheet.Cells[row, column] = value;
            
        }
    }
}
