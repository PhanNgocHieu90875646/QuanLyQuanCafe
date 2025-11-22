using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class PromotionDAO
    {
        private static PromotionDAO instance;
        public static PromotionDAO Instance
        {
            get { if (instance == null) instance = new PromotionDAO(); return instance; }
            private set { instance = value; }
        }

        private PromotionDAO() { }

        public List<Promotion> GetAllPromotions()
        {
            string query = "SELECT * FROM KhuyenMai ORDER BY Id DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<Promotion> list = new List<Promotion>();
            foreach (DataRow item in data.Rows)
                list.Add(new Promotion(item));
            return list;
        }

        public bool InsertPromotion(string tenKM, string moTa, string loaiKM, double giaTri,
                                    DateTime ngayBD, DateTime ngayKT, double dieuKien)
        {
            string query = @"INSERT INTO KhuyenMai (TenKM, MoTa, LoaiKM, GiaTri, NgayBatDau, NgayKetThuc, DieuKienToiThieu)
                             VALUES (N'" + tenKM + "', N'" + moTa + "', N'" + loaiKM + "', " + giaTri + ", '" +
                             ngayBD.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + ngayKT.ToString("yyyy-MM-dd HH:mm:ss") + "', " + dieuKien + ")";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdatePromotion(int id, string tenKM, string moTa, string loaiKM, double giaTri,
                                    DateTime ngayBD, DateTime ngayKT, double dieuKien, bool trangThai)
        {
            string query = @"UPDATE KhuyenMai 
                             SET TenKM = N'" + tenKM + "', MoTa = N'" + moTa + "', LoaiKM = N'" + loaiKM + "', GiaTri = " + giaTri +
                             ", NgayBatDau = '" + ngayBD.ToString("yyyy-MM-dd HH:mm:ss") +
                             "', NgayKetThuc = '" + ngayKT.ToString("yyyy-MM-dd HH:mm:ss") +
                             "', DieuKienToiThieu = " + dieuKien +
                             ", TrangThai = " + (trangThai ? 1 : 0) + " WHERE Id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeletePromotion(int id)
        {
            string query = "DELETE FROM KhuyenMai WHERE Id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public List<Promotion> GetActivePromotions()
        {
            List<Promotion> list = new List<Promotion>();
            string query = "SELECT * FROM KhuyenMai WHERE TrangThai = 1 AND GETDATE() BETWEEN NgayBatDau AND NgayKetThuc";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                list.Add(new Promotion(item));
            }
            return list;
        }
        public List<Promotion> GetActivePromotionss()
        {
            string query = "SELECT * FROM KhuyenMai WHERE TrangThai = 1"; // chỉ lấy hoạt động
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<Promotion> list = new List<Promotion>();
            foreach (DataRow item in data.Rows)
            {
                list.Add(new Promotion(item));
            }
            return list;
        }

        public Promotion GetPromotionById(int id)
        {
            string query = "SELECT * FROM KhuyenMai WHERE Id = @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            if (data.Rows.Count > 0)
                return new Promotion(data.Rows[0]);
            return null;
        }
    }
}
