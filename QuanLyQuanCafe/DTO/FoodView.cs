using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class FoodView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int IdCategory { get; set; }      // ID danh mục (dùng để binding combobox)
        public string CategoryName { get; set; } // Tên danh mục
        public int SoLuongTon { get; set; }      // Số lượng tồn
        public string ImagePath { get; set; }    // Đường dẫn ảnh

        public FoodView() { }

        public FoodView(DataRow row)
        {
            if (row.Table.Columns.Contains("ID") && row["ID"] != DBNull.Value)
                ID = (int)row["ID"];

            if (row.Table.Columns.Contains("Name") && row["Name"] != DBNull.Value)
                Name = row["Name"].ToString();

            if (row.Table.Columns.Contains("Price") && row["Price"] != DBNull.Value)
                Price = (float)Convert.ToDouble(row["Price"]);

            if (row.Table.Columns.Contains("idCategory") && row["idCategory"] != DBNull.Value)
                IdCategory = (int)row["idCategory"];

            if (row.Table.Columns.Contains("CategoryName") && row["CategoryName"] != DBNull.Value)
                CategoryName = row["CategoryName"].ToString();

            if (row.Table.Columns.Contains("SoLuongTon") && row["SoLuongTon"] != DBNull.Value)
                SoLuongTon = (int)row["SoLuongTon"];

            if (row.Table.Columns.Contains("ImagePath") && row["ImagePath"] != DBNull.Value)
                ImagePath = row["ImagePath"].ToString();
        }
    }
    }
