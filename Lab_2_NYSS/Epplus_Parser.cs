using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using OfficeOpenXml;

namespace Lab_2_NYSS
{
    class Epplus_Parser
    {
        public static List<string> columnNames = new List<string>();//для заголовка
        //public static int countOfRows;
        private static void FillDataTable()
        {
            //countOfRows = 0;
            DataTable dt = new DataTable();
            columnNames.Clear();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Resourse.path = openFileDialog.FileName;
            }


            FileInfo file = new FileInfo(Resourse.path);
            List<string> excelData = new List<string>();
            //read the Excel file as byte array

            byte[] bin = File.ReadAllBytes(Resourse.path);

            //create a new Excel package in a memorystream
            MemoryStream stream = new MemoryStream(bin);
            ExcelPackage excelPackage = new ExcelPackage(stream);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            foreach (var firstRowCell in worksheet.Cells[2, 1, 2, worksheet.Dimension.End.Column])
                columnNames.Add(firstRowCell.Text);

            //if(columnNames.Count != 8)
            //{
                //columnNames.RemoveAt(columnNames.Count - 1);
                //columnNames.RemoveAt(columnNames.Count - 1);
            //}
            foreach (var item in columnNames)
            {
                dt.Columns.Add(item);
            }

            for (int i = 3; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                //loop all cells in the row
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                UPI upi = new UPI();//создаем новую запись в коллекции

                upi.id = newRow[0].ToString();
                upi.name = newRow[1].ToString();
                upi.description = newRow[2].ToString();
                upi.danger = newRow[3].ToString();
                upi.target = newRow[4].ToString();

                if (newRow[5].ToString() == "1")
                    newRow[5] = "да";
                else
                    newRow[5] = "нет";
                upi.confidential = newRow[5].ToString();
                if (newRow[6].ToString() == "1")
                    newRow[6] = "да";
                else
                    newRow[6] = "нет";
                upi.integriti = newRow[6].ToString();
                if (newRow[7].ToString() == "1")
                    newRow[7] = "да";
                else
                    newRow[7] = "нет";
                upi.access = newRow[7].ToString();
                dt.Rows.Add(newRow);
                UPI.UPIs.Add(upi);//добавляем запись в коллекцию
            }
            if(columnNames.Count>8)
            {
                columnNames.RemoveAt(columnNames.Count-1);
                columnNames.RemoveAt(columnNames.Count-1);
            }
        }

        public static DataTable DatatableForGrid()//подготовка данных для грида
        {
            FillDataTable();
            DataTable table = new DataTable(); //неправильно но времени нет исправлять

            // Add columns.
            for (int i = 0; i < columnNames.Count; i++)
            {
                table.Columns.Add();
            }


            for (int i = 0; i < UPI.UPIs.Count; i++)
            {
                DataRow row = table.NewRow();
                row[0] = UPI.UPIs[i].id;
                row[1] = UPI.UPIs[i].name;
                row[2] = UPI.UPIs[i].description;
                row[3] = UPI.UPIs[i].danger;
                row[4] = UPI.UPIs[i].target;
                row[5] = UPI.UPIs[i].confidential;
                row[6] = UPI.UPIs[i].integriti;
                row[7] = UPI.UPIs[i].access;
                table.Rows.Add(row);
            }
            return table;
            
            
        }
    }
}
