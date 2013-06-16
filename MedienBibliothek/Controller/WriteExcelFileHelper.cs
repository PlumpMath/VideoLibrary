using System.IO;

namespace MedienBibliothek.Controller
{
    class WriteExcelFileHelper
    {
        public void WriteVideoListToExcel()
        {
            if (!File.Exists(Properties.Settings.Default.excelPath))
            {
                File.Create(Properties.Settings.Default.excelPath);
            }
            var excelWriter = new Microsoft.Office.Interop.Excel.Application();
            excelWriter.Visible = true;
            excelWriter.Workbooks.Open(Properties.Settings.Default.excelPath);
            excelWriter.Cells[1, 1] = "test";
        }
        

    }
}
