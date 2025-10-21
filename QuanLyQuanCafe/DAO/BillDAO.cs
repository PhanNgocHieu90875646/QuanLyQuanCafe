using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }

        }
        private BillDAO() { }
        public List<Bill> GetListbill()
        {
            List<Bill> list = new List<Bill>();
            string query = "  SELECT hd.id, hd.DataCheckIn, hd.DataCheckOnt, hd.inttable, hd.status, hd.discount, hd.totalPrice, ma.name AS TenMonAn FROM HoaDon hd JOIN ThongTinHoaDon tt ON hd.id = tt.idBill JOIN MonAn ma ON tt.idFood = ma.id;";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Bill category = new Bill(item);
                list.Add(category);
            }
            return list;
        }
        public int GetUnCheckBillIdByTableId(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select *from dbo.HoaDon where inttable= " + id + " and status = 0");
            if(data.Rows.Count > 0)
            {
                Bill bill =new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CheckOut(int id, int discount, double totalPrice, int idNhanVien)
        {
            string query = $"UPDATE HoaDon SET DataCheckOnt = GETDATE(), status = 1, discount = {discount} , totalPrice = {totalPrice}, IdNhanVien = {idNhanVien} WHERE id = {id}";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { discount, totalPrice, idNhanVien, id });

        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @inttable", new object[]{id});
        }


        public DataTable GetListBillByDateHD(DateTime checkIn, DateTime checkOut)
        {
            string query = @"SELECT b.id AS ID, 
                            b.DataCheckIn, 
                            b.DataCheckOnt, 
                            b.inttable AS IdBan, 
                            b.status AS TrangThai, 
                            b.discount AS GiamGia, 
                            b.totalPrice AS TongTien, 
                            b.IdNhanVien, 
                            nv.HoTen AS TenNhanVien
                     FROM HoaDon AS b 
                     LEFT JOIN NhanVien AS nv ON b.IdNhanVien = nv.Id
                     WHERE b.IsDeleted = 0 
                           AND b.DataCheckIn >= @checkIn 
                           AND (b.DataCheckOnt <= @checkOut OR b.DataCheckOnt IS NULL)
                     ORDER BY b.DataCheckOnt DESC";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }
        public int GetMaxIDBill()
        {
            try
            {
               return  (int)DataProvider.Instance.ExecuteScalar("select max(id) from dbo.HoaDon");
            }
            catch 
            {

                return 1;
            }
        }
        public int CreateBill()
        {
            string query = "INSERT INTO HoaDon DEFAULT VALUES; SELECT SCOPE_IDENTITY();";
            object result = DataProvider.Instance.ExecuteScalar(query);
            return Convert.ToInt32(result);
        }

        public Bill GetBillById(int id)
        {
            string query = "SELECT * FROM HoaDon WHERE Id = @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            if (data.Rows.Count > 0)
                return new Bill(data.Rows[0]);
            return null;
        }

        public bool UpdateBillTotal(int idBill, double total)
        {
            string query = "UPDATE HoaDon SET TongTien = @total WHERE Id = @idBill";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { total, idBill });
            return result > 0;
        }

    

        public List<Bill> GetAllBills()
        {
            List<Bill> list = new List<Bill>();
            string query = "SELECT * FROM HoaDon";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
                list.Add(new Bill(row));
            return list;
        }
        public bool DeleteBill(int idBill)
        {
            string query = "UPDATE HoaDon SET IsDeleted = 1 WHERE id = @idBill";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill });
            return result > 0;
        }
    }
}
