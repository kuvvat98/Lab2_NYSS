using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Lab_2_NYSS
{
    class Downloader
    {
        public static void DownloadFile(string fileName)
        {
            try
            {
                using (WebClient client = new WebClient())
                    client.DownloadFile(new Uri("https://bdu.fstec.ru/files/documents/thrlist.xlsx"), fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  
    }
}
