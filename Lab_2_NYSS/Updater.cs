using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_2_NYSS
{
    class Updater
    {
        public static ObservableCollection<UPI> newUPIs = new ObservableCollection<UPI>();//новые записи
        static ObservableCollection<ObservableCollection<string>> oldElements =
               new ObservableCollection<ObservableCollection<string>>();//новые элементы
        static ObservableCollection<ObservableCollection<string>> newElements =
                new ObservableCollection<ObservableCollection<string>>();
        static HashSet<string> ids = new HashSet<string>();
        public static ObservableCollection<string> stringsOut = new ObservableCollection<string>();//строки об изменении
        public static void Parse()
        {  
            DataTable dt = new DataTable();
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(@Resourse.path);
            List<string> excelData = new List<string>();
            //read the Excel file as byte array

            byte[] bin = File.ReadAllBytes(@Resourse.path);

            //create a new Excel package in a memorystream
            MemoryStream stream = new MemoryStream(bin);
            ExcelPackage excelPackage = new ExcelPackage(stream);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            foreach (var firstRowCell in worksheet.Cells[2, 1, 2, worksheet.Dimension.End.Column])
                dt.Columns.Add(firstRowCell.Text);


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
                newUPIs.Add(upi);//добавляем запись в коллекцию
            }
        }

        public static void Compare()
        {
            for (int i = 0; i < UPI.UPIs.Count; i++)
            {
                ObservableCollection<string> snewElems = new ObservableCollection<string>();//элементы кооторые не совпадают
                ObservableCollection<string> soldElems = new ObservableCollection<string>();//элементы кооторые не совпадают
                UPI olduPI = UPI.UPIs[i];
                UPI newupi = newUPIs[i];
                if (olduPI.id != newupi.id)
                {
                    snewElems.Add(newupi.id);
                    soldElems.Add(olduPI.id);
                    ids.Add(olduPI.id);
                }
                if (olduPI.name != newupi.name)
                {
                    snewElems.Add(newupi.name);
                    soldElems.Add(olduPI.name);
                    ids.Add(olduPI.id);
                }
                    
                if (olduPI.description != newupi.description)
                {
                    snewElems.Add(newupi.description);
                    soldElems.Add(olduPI.description);
                    ids.Add(olduPI.id);
                }
                if (olduPI.danger != newupi.danger)
                {
                    snewElems.Add(newupi.danger);
                    soldElems.Add(olduPI.danger);
                    ids.Add(olduPI.id);
                }
                if (olduPI.target != newupi.target)
                {
                    snewElems.Add(newupi.target);
                    soldElems.Add(olduPI.target);
                    ids.Add(olduPI.id);
                }
                if (olduPI.confidential != newupi.confidential)
                {
                    snewElems.Add(newupi.confidential);
                    soldElems.Add(olduPI.confidential);
                    ids.Add(olduPI.id);
                }
                if (olduPI.integriti != newupi.integriti)
                {
                    snewElems.Add(newupi.integriti);
                    soldElems.Add(olduPI.integriti);
                    ids.Add(olduPI.id);
                }
                if (olduPI.access != newupi.access)
                {
                    snewElems.Add(newupi.access);
                    soldElems.Add(olduPI.access);
                    ids.Add(olduPI.id);
                }
                if (snewElems.Count != 0)
                {
                    newElements.Add(snewElems);
                    oldElements.Add(soldElems);
                    ids.Add(olduPI.id);
                }
            }
        }
        public static void OutputResult()
        {
            string[] ids = Updater.ids.ToArray();
            if(newElements.Count!=0)
            {
                
                stringsOut.Add($"Количество измененных записей: {newElements.Count}");
                for (int i = 0; i < newElements.Count; i++)
                {
                    string s = $"Изменена запись {ids[i]}: ";
                    for (int j = 0; j < newElements[i].Count; j++)
                    {
                        s += $"{oldElements[i][j]} на {newElements[i][j]}";
                        stringsOut.Add(s);
                    }
                } 
            }
            else
            {
                MessageBox.Show("Нет обновленных записей!");
            }
        }
    }
}
