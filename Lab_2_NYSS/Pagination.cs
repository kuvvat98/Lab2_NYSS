using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_2_NYSS
{
    class Pagination
    {
        public static List<List<UPI>> PagiUPIs = SplitList(UPI.UPIs, 20);//лист с листами по 20 upi

        private static List<List<UPI>> SplitList(List<UPI> locations, int nSize = 20)
        {
            var list = new List<List<UPI>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));//типо последние 17 тоже заберуться
            }
            return list;
        }

        public static DataTable ForwardOrPrevDatatable(int numberOfElementInPagi)//подготовка данных для грида
        {
            DataTable table = new DataTable(); //неправильно но времени нет исправлять

            // Add columns.
            for (int i = 0; i < Epplus_Parser.columnNames.Count; i++)
            {
                table.Columns.Add();
            }

            
                for (int i = 0; i < PagiUPIs[numberOfElementInPagi].Count; i++)
                {
                    DataRow row = table.NewRow();
                    row[0] = PagiUPIs[numberOfElementInPagi][i].id;
                    row[1] = PagiUPIs[numberOfElementInPagi][i].name;
                    row[2] = PagiUPIs[numberOfElementInPagi][i].description;
                    row[3] = PagiUPIs[numberOfElementInPagi][i].danger;
                    row[4] = PagiUPIs[numberOfElementInPagi][i].target;
                    row[5] = PagiUPIs[numberOfElementInPagi][i].confidential;
                    row[6] = PagiUPIs[numberOfElementInPagi][i].integriti;
                    row[7] = PagiUPIs[numberOfElementInPagi][i].access;
                    table.Rows.Add(row);
                }
            
            return table;
        }

    }
}
