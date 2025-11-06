using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
   
        //public Menu(string foodName, string size, int count, float price, float totalPrice = 0)
        //{
        //    this.FoodName = foodName;
        //    this.Size = size;
        //    this.Count = count;
        //    this.Price = price;
        //    this.TotalPrice = totalPrice;
        //}
        //public Menu(DataRow row)
        //{
        //    this.FoodName = row["Name"].ToString();
        //    this.Size = row["Size"].ToString();
        //    this.Count = (int)row["count"];
        //    this.Price = (float)Convert.ToDouble(row["price"].ToString());
        //    this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        //}
        //private float totalPrice;
        //public float TotalPrice
        //{
        //    get { return totalPrice; }
        //    set { totalPrice = value; }
        //}
        //private float price;
        //public float Price
        //{
        //    get { return price; }
        //    set { price = value; }
        //}
        //private int count;
        //public int Count
        //{
        //    get { return count; }
        //    set { count = value; }
        //}
        //private string foodName;
        //public string FoodName
        //{
        //    get { return foodName; }
        //    set { foodName = value; }
        //}
        //public string Size { get; set; }
        public class Menu
        {
            public string TenMon { get; set; }
            public string SizeMon { get; set; }
            public int SoLuong { get; set; }
            public float DonGia { get; set; }
            public float ThanhTien { get; set; }

            public Menu(string tenMon, string sizeMon, int soLuong, float donGia, float thanhTien = 0)
            {
                TenMon = tenMon;
                SizeMon = sizeMon;
                SoLuong = soLuong;
                DonGia = donGia;
                ThanhTien = thanhTien;
            }

            public Menu(DataRow row)
            {
                TenMon = row["TenMon"].ToString();
                SizeMon = row["SizeMon"].ToString();
                SoLuong = (int)row["SoLuong"];
                //DonGia = Convert.ToDouble(row["DonGia"]);
            DonGia = (float)Convert.ToDouble(row["DonGia"].ToString());

            ThanhTien = (float)Convert.ToDouble(row["ThanhTien"].ToString());
        }
        }
    
}