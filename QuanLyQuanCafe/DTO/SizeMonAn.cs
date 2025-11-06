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
        public string Size { get; set; }
        public decimal Gia { get; set; }

        public SizeMonAn(int id, int idMonAn, string size, decimal gia)
        {
            Id = id;
            IdMonAn = idMonAn;
            Size = size;
            Gia = gia;
        }

        public SizeMonAn(System.Data.DataRow row)
        {
            Id = (int)row["Id"];
            IdMonAn = (int)row["IdMonAn"];
            Size = row["Size"].ToString();
            Gia = (decimal)row["Gia"];
        }
        
    }
}
