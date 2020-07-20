using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_NYSS
{
    class ShortandFullData
    {
        public static DataTable ShortDatatable()//подготовка данных для грида
        {
            DataTable table = new DataTable(); //неправильно но времени нет исправлять

            // Add columns.
            for (int i = 0; i < Epplus_Parser.columnNames.Count-6; i++)
            {
                table.Columns.Add();
            }


            for (int i = 0; i < UPI.UPIs.Count; i++)
            {
                DataRow row = table.NewRow();
                row[0] = UPI.UPIs[i].id;
                row[1] = UPI.UPIs[i].name;
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable FullDatatable()//подготовка данных для грида
        {
            DataTable table = new DataTable(); //неправильно но времени нет исправлять

            // Add columns.
            for (int i = 0; i < Epplus_Parser.columnNames.Count; i++)
            {
                table.Columns.Add();
            }


            for (int i = 0; i <UPI.UPIs.Count; i++)
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
