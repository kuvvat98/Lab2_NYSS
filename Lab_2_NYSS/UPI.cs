using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_2_NYSS
{
    class UPI
    {

        public string id = "";
        public string name = "";
        public string description = "";
        public string danger = "";
        public string target = "";
        public string confidential = "";
        public string integriti = "";
        public string access = "";

        public UPI(string id, string name, string description, string danger, string target, string confidential, string integriti, string access)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.danger = danger;
            this.target = target;
            this.confidential = confidential;
            this.integriti = integriti;
            this.access = access;
        }

        public UPI()
        {

        }


        public static List<UPI> UPIs = new List<UPI>();//записи

    }
}
