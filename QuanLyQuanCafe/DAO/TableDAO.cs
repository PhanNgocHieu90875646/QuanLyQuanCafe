using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }

        }
        public static int TableWidth = 100;
        public static int TableHeight = 100;
        private TableDAO() { }
        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1 , @idTable2",new object[] {id1,id2});
        }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table=new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }
        public bool InsertTable(string name)
        {
            string query = $"INSERT INTO ThucAnTrenBan (name, status) VALUES (N'{name}', N'Trống')";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateTable(int id, string name )
        {
            string query = $"UPDATE ThucAnTrenBan SET name = N'{name}' WHERE id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteTable(int id)
        {
            // Xoá thông tin hóa đơn liên quan nếu cần
            string query = $"DELETE FROM ThucAnTrenBan WHERE id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        public List<Table> GetTableListByCategoryID(int categoryId)
        {
            List<Table> list = new List<Table>();
            string query = "SELECT * FROM ThucAnTrenBan WHERE CategoryID = @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { categoryId });

            foreach (DataRow row in data.Rows)
            {
                Table table = new Table(row);
                list.Add(table);
            }

            return list;
        }

        internal bool UpdateTableStatus(int id, string status)
        {
            string query = string.Format("update dbo.ThucAnTrenBan set status = N'{0}' where id = {1}", status, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        internal bool UpdateTable(int id, string name, string status)
        {
      
            string query = string.Format("update dbo.ThucAnTrenBan set name = N'{0}', status = N'{1}' where id = {2}", name, status, id);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
        }
    }
}
