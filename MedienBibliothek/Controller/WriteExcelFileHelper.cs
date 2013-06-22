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
        readonly Microsoft.Office.Interop.Excel.Application _excelWriterApp = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Worksheet _wsheet;
        Microsoft.Office.Interop.Excel.Workbook _wbook;
        public void WriteVideoListToExcelFile(IEnumerable<Video> videoList)
        {
            
            if (!File.Exists(Properties.Settings.Default.excelFile))
            {
                var myExcelFile = File.Create(Properties.Settings.Default.excelFile);
                myExcelFile.Close();
            }
            _excelWriterApp.Visible = false;
            _excelWriterApp.DisplayAlerts = false;
            _wbook = _excelWriterApp.Workbooks.Add(true);
            _wsheet = (Worksheet)_wbook.ActiveSheet;
            WriteVideoList(videoList);
        }

        private void WriteVideoList(IEnumerable<Video> videoList)
        {
            _wsheet.Cells[1, 1] = "Video name";
            _wsheet.Cells[1, 2] = "Video quality";
            _wsheet.Cells[1, 3] = "Video path";
            int counter = 2;
            foreach (var video in videoList)
            {
                WriteValueInCell(1, counter, video.Name);
                WriteValueInCell(2, counter, video.Quality);
                WriteValueInCell(3, counter, video.Path);
                counter++;
            }
            _wbook.SaveAs(Properties.Settings.Default.excelFile);
            _wbook.Close();
        }

        private void WriteValueInCell(int column, int row, string value)
        {
            _wsheet.Cells[row, column] = value;
            
        }
    }
}
