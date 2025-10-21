using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;
        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
            private set { instance = value; }
        }

        private NhanVienDAO() { }

        public List<NhanVien> GetListNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            string query = "SELECT * FROM NhanVien";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVien nv = new NhanVien(item);
                list.Add(nv);
            }
            return list;
        }
        public NhanVien GetStaffByAccount(string userName)
        {
            string query = @"
                SELECT nv.*
                FROM NhanVien AS nv
                INNER JOIN TaiKhoan AS tk ON nv.Id = tk.IdNhanVien
                WHERE tk.UseName = @userName";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

            if (data.Rows.Count > 0)
                return new NhanVien(data.Rows[0]);

            return null;
        }
        public bool InsertNhanVien(string hoTen, DateTime ngaySinh, string gioiTinh, string soDienThoai, string diaChi, string role)
        {
            string query = "INSERT INTO NhanVien(HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, Role) " +
                           "VALUES ( @hoten , @ngaysinh , @gioitinh , @sdt , @diachi , @role )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { hoTen, ngaySinh, gioiTinh, soDienThoai, diaChi, role });
            return result > 0;
        }

        public bool UpdateNhanVien(int id, string hoTen, DateTime ngaySinh, string gioiTinh, string soDienThoai, string diaChi, string role)
        {
            string query = "UPDATE NhanVien SET HoTen = @hoten , NgaySinh = @ngaysinh , GioiTinh = @gioitinh , SoDienThoai = @sdt , DiaChi = @diachi , Role = @role " +
                           "WHERE Id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { hoTen, ngaySinh, gioiTinh, soDienThoai, diaChi, role, id });
            return result > 0;
        }

        public bool DeleteNhanVien(int id)
        {
            string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE IdNhanVien = @id";
            int count = (int)DataProvider.Instance.ExecuteScalar(checkQuery, new object[] { id });

            if (count > 0)
            {
                // Nếu có tài khoản thì không cho xóa
                System.Windows.Forms.MessageBox.Show("Nhân viên này đang có tài khoản, vui lòng xóa tài khoản trước!");
                return false;
            }

            // Nếu không có tài khoản, thì cho phép xóa nhân viên
            string query = "DELETE FROM NhanVien WHERE Id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });

            return result > 0;
        }
    }
}
