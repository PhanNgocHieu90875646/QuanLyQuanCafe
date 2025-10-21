using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }

        }

        private BillInfoDAO() { }
        public void DeleteBillInfoByIdFood(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.ThongTinHoaDon where idFood = " +id);
        }
        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBI = new List<BillInfo>();
            string query = @"SELECT bi.id, bi.idBill, bi.idFood, bi.count, f.price AS DonGia, f.name AS TenMon FROM ThongTinHoaDon AS bi JOIN MonAn AS f ON bi.idFood = f.id WHERE bi.idBill = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BillInfo billInfo = new BillInfo(item);
                listBI.Add(billInfo);
            }
            return listBI;
        }
        public void InsertBillInfo(int idBill,int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
        public bool InsertBillInfo(int idBill, int idFood, int count, double donGia)
        {
            string query = "INSERT INTO ThongTinHoaDon (idBill, idFood, count, DonGia) VALUES (@idBill, @idFood, @count, @donGia)";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood, count, donGia });
            return result > 0;
        }

        public List<BillInfo> GetBillInfoByBillId(int idBill)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            string query = @"SELECT c.id, c.IdBill as IdHoaDon, c.idFood as IdMon, f.name as TenMon, c.Count as SoLuong , f.price AS DonGia, (c.Count * f.price) AS ThanhTien FROM ThongTinHoaDon AS c INNER JOIN MonAn AS f ON c.idFood = f.ID WHERE c.idBill = @idBill";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idBill });

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;

        }

        public bool DeleteBillInfoByFood(int idBill, int idFood)
        {
            string query = "DELETE FROM ThongTinHoaDon WHERE IdBill = @idBill AND IdFood = @idFood";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood });
            return result > 0;
        }
    }
}
