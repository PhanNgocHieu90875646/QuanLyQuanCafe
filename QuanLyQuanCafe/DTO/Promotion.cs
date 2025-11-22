using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Promotion
    {
        public int Id { get; set; }
        public string TenKM { get; set; }
        public string MoTa { get; set; }
        public string LoaiKM { get; set; }
        public double GiaTri { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public double DieuKienToiThieu { get; set; }
        public bool TrangThai { get; set; }

        public Promotion() { }

        public Promotion(System.Data.DataRow row)
        {
            Id = (int)row["Id"];
            TenKM = row["TenKM"].ToString().Trim(); 
            MoTa = row["MoTa"].ToString().Trim();
            LoaiKM = row["LoaiKM"].ToString().Trim();
            GiaTri = Convert.ToDouble(row["GiaTri"]);
            NgayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
            NgayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
            DieuKienToiThieu = Convert.ToDouble(row["DieuKienToiThieu"]);
            TrangThai = Convert.ToBoolean(row["TrangThai"]);
        }
    }
}
