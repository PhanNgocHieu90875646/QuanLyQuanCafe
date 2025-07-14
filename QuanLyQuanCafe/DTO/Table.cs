using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
     public class Table
    {
        public Table(string status, string name, int id)
        {
            this.Status = status;
            this.Name = name;
            this.iD = id;
        }
        public Table(DataRow row)
        {
            this.iD = (int)row["id"];
            this.Name=row["name"].ToString();
            this.Status = row["status"].ToString();
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string name;
        public string Name
        { 
            get { return name; } 
            set { name = value; }
        }
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

    
    }
}
