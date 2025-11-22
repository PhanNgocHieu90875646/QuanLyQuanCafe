using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyQuanCafe.DTO
{
    public class SizeMonAn
    {
        public int Id { get; set; }
        public int IdMonAn { get; set; }
        public string TenMon { get; set; }
        public string Size { get; set; }
        public decimal Gia { get; set; }

        public SizeMonAn(int id, int idMonAn, string tenMon, string size, decimal gia)
        {
            Id = id;
            IdMonAn = idMonAn;
            TenMon = tenMon;
            Size = size;
            Gia = gia;
        }

        public SizeMonAn(System.Data.DataRow row)
        {
            Id = (int)row["Id"];
            IdMonAn = (int)row["IdMonAn"];

            // an toàn: kiểm tra nếu DataTable có column TenMon
            if (row.Table.Columns.Contains("TenMon"))
                TenMon = row["TenMon"].ToString();
            else if (row.Table.Columns.Contains("name"))
                TenMon = row["name"].ToString();
            else
                TenMon = string.Empty;

            Size = row["Size"].ToString();

            // Nếu Gia có thể là decimal trong DB
            if (row.Table.Columns.Contains("Gia") && row["Gia"] != DBNull.Value)
                Gia = (decimal)row["Gia"];
            else
                Gia = 0m;
        }
        
    }
}
