using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int foodID, int count,double dongia, string tenmon,double thanhtien) { 
             this.ID = id;
            this.BillID = billID;
            this.FoddID = foodID;
            this.Count = count;
            this.DonGia = dongia;
            this.TenMon = tenmon;
            this.ThanhTien = thanhtien;
        }
        public BillInfo() { }
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idHoadon"];
            this.FoddID = (int)row["idMon"];
            this.Count = (int)row["SoLuong"];
            if (row.Table.Columns.Contains("DonGia"))
                this.DonGia = float.Parse(row["DonGia"].ToString());
            if (row.Table.Columns.Contains("TenMon"))
                this.TenMon = row["TenMon"].ToString();
            if (row.Table.Columns.Contains("ThanhTien"))
                ThanhTien = Convert.ToDouble(row["ThanhTien"]);
        }
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private int foodID;
        public int FoddID
        {
            get { return foodID; }
            set { foodID = value; }
        }
        private int billID;
        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public double DonGia { get; set; }
        public string TenMon { get; set; }
        public double ThanhTien { get; set; }
    }
}
