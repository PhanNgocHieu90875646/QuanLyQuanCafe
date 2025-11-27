using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;
        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return instance; }
            private set { instance = value; }
        }

        private KhachHangDAO() { }

        // Tìm khách theo SĐT
        public KhachHang GetKHByPhone(string sdt)
        {
            string query = "SELECT * FROM KhachHang WHERE SoDienThoai = @sdt";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { sdt });

            if (data.Rows.Count > 0)
                return new KhachHang(data.Rows[0]);

            return null;
        }

        // Thêm khách mới
        public int Insert(string ten, string sdt)
        {
            string query = @"
            INSERT INTO KhachHang(TenKH, SoDienThoai, DiemTichLuy, NgayThamGia, TrangThai)
            VALUES (@ten, @sdt, 0, GETDATE(), 1);
            SELECT SCOPE_IDENTITY();
        ";

            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { ten, sdt });
            return Convert.ToInt32(result);
        }
        public bool Inserrt(string ten, string sdt)
        {
            string query =
     $"INSERT INTO KhachHang (TenKH, SoDienThoai, DiemTichLuy, NgayThamGia, TrangThai) " +
     $"VALUES (N'{ten}', '{sdt}', 0, GETDATE(), 1)";


            int result = DataProvider.Instance.ExecuteNonQuery(
                query,
                new object[] { ten, sdt }
            );

            return result > 0;
        }


        public void UpdateDiem(int idKh, int diemMoi)
        {
            string query = $@"
        UPDATE KhachHang
        SET DiemTichLuy = {diemMoi}
        WHERE Id = {idKh}";

            DataProvider.Instance.ExecuteNonQuery(query);
        }

        // Cộng điểm
        public void CongDiem(int idKH, int tongTien)
        {
            int diem = (tongTien / 100000) * 10;

            string query = $"UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + {diem} WHERE Id = {idKH}";

            DataProvider.Instance.ExecuteNonQuery(query, new object[] { diem, idKH });
        }

        // Lấy điểm
        public int GetDiem(int idKH)
        {
            string query = "SELECT DiemTichLuy FROM KhachHang WHERE Id = @id";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idKH });
        }
        public KhachHang GetKhachById(int id)
        {
            string query = "SELECT * FROM KhachHang WHERE Id = @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            if (data.Rows.Count > 0)
            {
                return new KhachHang(data.Rows[0]);
            }

            return null;
        }
        public List<KhachHang> SearchKhachHang(string keyword)
        {
            List<KhachHang> list = new List<KhachHang>();

                string query = @"
                    SELECT *
                    FROM KhachHang
                    WHERE TenKH LIKE @kw OR SoDienThoai LIKE @kw
                ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { "%" + keyword + "%" });

            foreach (DataRow row in data.Rows)
                list.Add(new KhachHang(row));

            return list;
        }

        public List<KhachHang> GetAllKhachHang()
        {
            List<KhachHang> list = new List<KhachHang>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM KhachHang");

            foreach (DataRow row in data.Rows)
                list.Add(new KhachHang(row));

            return list;
        }
        public List<KhachHang> GetListKhachHang()
        {
            List<KhachHang> list = new List<KhachHang>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM KhachHang");

            foreach (DataRow row in data.Rows)
            {
                list.Add(new KhachHang(row));
            }

            return list;
        }
        public bool InsertKH(string ten, string sdt)
        {
            string query = $"INSERT INTO KhachHang (TenKH, SoDienThoai, DiemTichLuy, NgayThamGia, TrangThai) VALUES (N'{ten}', N'{sdt}', 0, GETDATE(), 1)";

            int result = DataProvider.Instance.ExecuteNonQuery(query,
                    new object[] { ten, sdt });

            return result > 0;
        }

        public bool Update(int id, string ten, string sdt, int diem, int trangThai)
        {
            string query = $"UPDATE KhachHang SET TenKH = N'{ten}', SoDienThoai = N'{sdt}', DiemTichLuy = {diem}, TrangThai = {trangThai} WHERE Id = {id}";

            int result = DataProvider.Instance.ExecuteNonQuery(query,
                    new object[] { ten, sdt, diem, trangThai, id });

            return result > 0;
        }


        public bool Delete(int id)
        {
            string query = "DELETE FROM KhachHang WHERE Id = @id";

            int result = DataProvider.Instance.ExecuteNonQuery(query,
                    new object[] { id });

            return result > 0;
        }
        public bool IsPhoneExist(string sdt, int? id = null)
        {
            string query = $"SELECT COUNT(*) FROM KhachHang WHERE SoDienThoai = N'{sdt}'";

            if (id != null)
            {
                query += " AND Id <> @id";
                return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { sdt, id }) > 0;
            }

            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { sdt }) > 0;
        }

    }

}
