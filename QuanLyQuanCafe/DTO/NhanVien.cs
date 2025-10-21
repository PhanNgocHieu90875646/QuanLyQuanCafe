using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class NhanVien
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Role { get; set; }

        public NhanVien() { }

        public NhanVien(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.HoTen = row["HoTen"].ToString();
            this.NgaySinh = (DateTime)row["NgaySinh"];
            this.GioiTinh = row["GioiTinh"].ToString();
            this.SoDienThoai = row["SoDienThoai"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.Role = row["Role"].ToString();
        }
    }
}
