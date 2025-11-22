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
        public BillInfo(int id, int billID, int foodID, int count, string sizeMon, double dongia, string tenmon,double thanhtien) { 
             this.ID = id;
            this.IdHoaDon = billID;
            this.IdMonAn = foodID;
            this.SoLuong = count;
            this.SizeMon = sizeMon;
            this.DonGia = dongia;
            this.TenMon = tenmon;
            this.ThanhTien = thanhtien;
        }
        public BillInfo() { }
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IdHoaDon = (int)row["idHoadon"];
            this.IdMonAn = (int)row["idMon"];
            this.SoLuong = (int)row["SoLuong"];
            this.SizeMon = row["SizeMon"].ToString();
            if (row.Table.Columns.Contains("DonGia"))
                this.DonGia = float.Parse(row["DonGia"].ToString());
            if (row.Table.Columns.Contains("TenMon"))
                this.TenMon = row["TenMon"].ToString();
            if (row.Table.Columns.Contains("ThanhTien"))
                ThanhTien = Convert.ToDouble(row["ThanhTien"]);
        }
        private int soLuong;
        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        private int foodID;
        public int IdMonAn
        {
            get { return foodID; }
            set { foodID = value; }
        }
        private int billID;
        public int IdHoaDon
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
        public string SizeMon { get; set; }

    }
}
