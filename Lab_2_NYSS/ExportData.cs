using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_NYSS
{
    public class ExportData
    {
        public static void OutputData()
        {
            string dummyFileName = "Нажмите просто Сохранить, не выбирая файл, но открыв путь к файлу!";

            SaveFileDialog sf = new SaveFileDialog();
            // Feed the dummy name to the save dialog
            sf.FileName = dummyFileName;
            string savePath = "";
            if (sf.ShowDialog() == true)
            {
                // Now here's our save folder
                savePath = Path.GetDirectoryName(sf.FileName);
                // Do whatever
            }
            string file_path = savePath + "/newFile.xlsx";

            FileInfo newFile = new FileInfo(file_path);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(file_path);
            }
            using (var package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet0");

                for (int i = 0; i < Epplus_Parser.columnNames.Count; i++)//заголовок
                {
                    worksheet.Cells[2, i + 1, 2, Epplus_Parser.columnNames.Count].Value = Epplus_Parser.columnNames[i];
                }


                for (int i = 0; i < UPI.UPIs.Count; i++)//строки 
                {
                    worksheet.Cells[i + 3, 1].Value = int.Parse(UPI.UPIs[i].id);//i+3 - с 3 строки
                    worksheet.Cells[i + 3, 2].Value = UPI.UPIs[i].name;
                    worksheet.Cells[i + 3, 3].Value = UPI.UPIs[i].description;
                    worksheet.Cells[i + 3, 4].Value = UPI.UPIs[i].danger;
                    worksheet.Cells[i + 3, 5].Value = UPI.UPIs[i].target;
                    worksheet.Cells[i + 3, 6].Value = UPI.UPIs[i].confidential;
                    worksheet.Cells[i + 3, 7].Value = UPI.UPIs[i].integriti;
                    worksheet.Cells[i + 3, 8].Value = UPI.UPIs[i].access;
                }
                for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
                {
                    worksheet.Column(i).Width = 20;
                }
                
                package.Save();
            }
        }
    }
}
