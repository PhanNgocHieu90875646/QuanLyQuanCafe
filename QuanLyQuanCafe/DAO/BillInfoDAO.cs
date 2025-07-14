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
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.ThongTinHoaDon where idBill = " + id);
            foreach (DataRow item in data.Rows)
            {
                BillInfo billInfo=new BillInfo(item);
                listBI.Add(billInfo);
            }
            return listBI;
        }
        public void InsertBillInfo(int idBill,int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
    }
}
