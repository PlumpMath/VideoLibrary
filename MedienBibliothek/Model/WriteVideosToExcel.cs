using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace MedienBibliothek.Model
{
    class WriteVideosToExcel
    {
        public void WriteVideoListToExcel()
        {
            if (!File.Exists(Properties.Settings.Default.excelPath))
            {
                File.Create(Properties.Settings.Default.excelPath);
            }
            var excelWriter = new Excel.Application();
            excelWriter.Visible = true;
            excelWriter.Workbooks.Open(Properties.Settings.Default.excelPath);
            excelWriter.Cells[1, 1] = "test";
        }
        

    }
}
