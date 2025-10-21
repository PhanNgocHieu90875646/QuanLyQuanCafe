using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        public Account(string useName, string displayName ,int type, int idNhanVien, string password = null)
        {
            this.UseName = useName;
            this.DisplayName = displayName;
            this.Password = password;
            this.Type = type;
            IdNhanVien = idNhanVien;
        }
        public Account(DataRow row)
        {
            this.UseName =row["useName"].ToString();
            this.DisplayName = row["displayName"].ToString();           
            this.Type = (int)row["type"];
            this.Password = row["password"].ToString();
            if (row.Table.Columns.Contains("IdNhanVien") && row["IdNhanVien"] != DBNull.Value)
            {
                this.IdNhanVien = Convert.ToInt32(row["IdNhanVien"]);
            }
            else
            {
                this.IdNhanVien = 0;
            }
        }
        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        private string useName;
        public string UseName
        {
            get { return useName; }
            set { useName = value; }
        }
        public int IdNhanVien { get; set; }
    }
}
