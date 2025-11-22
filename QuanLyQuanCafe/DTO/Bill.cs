using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dataCheckIn, DateTime? dataCheckOnt,int status, int? idNhanVien, string tenNhanVien,int? idkhuyenmai, string tenKhuyenMai, double giamgiadiem, int discount=0) {
            this.ID = id;
            this.DateCheckIn = dataCheckIn;
            this.DateCheckOnt = dataCheckOnt;
            this.Status = status;
            this.Discount=discount;
            this.IdNhanVien = idNhanVien;
            this.TenNhanVien = tenNhanVien;
            this.IdKhuyenMai = idkhuyenmai;
            this.TenKhuyenMai = tenKhuyenMai;
            this.GiamGiaDiem = giamgiadiem;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["DataCheckIn"];
            var DateCheckOntTemp= row["DataCheckOnt"];
            if(DateCheckOntTemp.ToString()!="") 
            this.DateCheckOnt = (DateTime?)DateCheckOntTemp;
            this.Status = (int)row["status"];
            if(row["discount"].ToString()!="")
                this.Discount = (int)row["discount"];
            if (row.Table.Columns.Contains("IdNhanVien") && row["IdNhanVien"] != DBNull.Value)
                this.IdNhanVien = (int)row["IdNhanVien"];
            if (row.Table.Columns.Contains("TenNhanVien") && row["TenNhanVien"] != DBNull.Value)
                this.TenNhanVien = row["TenNhanVien"].ToString();
            if (row.Table.Columns.Contains("IdKhuyenMai") && row["IdKhuyenMai"] != DBNull.Value)
                IdKhuyenMai = Convert.ToInt32(row["IdKhuyenMai"]);

            if (row.Table.Columns.Contains("TenKhuyenMai"))
                TenKhuyenMai = row["TenKhuyenMai"].ToString();
            this.GiamGiaDiem = row["GiamGiaDiem"] != DBNull.Value ? Convert.ToDouble(row["GiamGiaDiem"]) : 0;

        }


        private int discount;
        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime? dataCheckOnt;
        public DateTime? DateCheckOnt
        {
            get { return dataCheckOnt; }
            set { dataCheckOnt = value; }
        }
        private  DateTime? dataCheckIn;
        public DateTime? DateCheckIn
        {
            get { return dataCheckIn; }
            set { dataCheckIn = value; }
        }
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int? IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string TenKhuyenMai { get; set; }
        public int? IdKhuyenMai { get; set; }
        public double GiamGiaDiem { get; set; }

    }
}
