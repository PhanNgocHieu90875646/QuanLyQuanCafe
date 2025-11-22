using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class KhachHang
    {
        public int Id { get; set; }
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }
        public int DiemTichLuy { get; set; }
        public DateTime NgayThamGia { get; set; }
        public int TrangThai { get; set; }

        public KhachHang(DataRow row)
        {
            Id = (int)row["Id"];
            TenKH = row["TenKH"].ToString();
            SoDienThoai = row["SoDienThoai"].ToString();
            DiemTichLuy = row["DiemTichLuy"] != DBNull.Value ? (int)row["DiemTichLuy"] : 0;
            NgayThamGia = row["NgayThamGia"] != DBNull.Value ? (DateTime)row["NgayThamGia"] : DateTime.Now;
            TrangThai = row["TrangThai"] != DBNull.Value ? (int)row["TrangThai"] : 1;
        }
        public KhachHang() { }
    }


}
