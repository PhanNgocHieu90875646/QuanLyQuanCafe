using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        public Food(int id, string name, int categoryID, float price,int soLuongTon,string imagePath)
        {
            this.ID = id;
            this.Name = name;
            this.CategoryID = categoryID;
            this.Price = price;
            this.SoLuongTon=soLuongTon;
            this.ImagePath=imagePath;
        }
        public Food(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.CategoryID=(int)row["idcategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            if (row.Table.Columns.Contains("SoLuongTon") && row["SoLuongTon"] != DBNull.Value)
                this.SoLuongTon = (int)row["SoLuongTon"];

            if (row.Table.Columns.Contains("ImagePath"))
                this.ImagePath = row["ImagePath"].ToString();
        }
        private float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        private int categoryID;
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
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
        private int soLuongTon;
        public int SoLuongTon
        {
            get { return soLuongTon; }
            set { soLuongTon = value; }
        }
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
    }
}
