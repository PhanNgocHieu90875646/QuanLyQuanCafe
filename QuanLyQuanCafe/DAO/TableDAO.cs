using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static int TableWidth = 105;
        public static int TableHeight = 105;
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
            string query = $"INSERT INTO ThucAnTrenBan (name, status) VALUES (N'{name}', N'Trống ')";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateTable(int id, string name )
        {
            string query = $"UPDATE ThucAnTrenBan SET name = N'{name}' WHERE id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteTable(int id)
        {
            // Kiểm tra xem bàn này có hóa đơn nào không
            string queryCheck = "SELECT COUNT(*) FROM HoaDon WHERE inttable = " + id;
            int count = (int)DataProvider.Instance.ExecuteScalar(queryCheck);

            if (count > 0)
            {
                MessageBox.Show("Không thể xóa bàn vì đã có hóa đơn liên quan!");
                return false;
            }

            string query = "DELETE FROM ThucAnTrenBan WHERE id = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
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
     

        internal bool IsTableNameExists(string name)
        {
            string query = "SELECT COUNT(*) FROM ThucAnTrenBan WHERE name = @name";
            int count = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { name });
            return count > 0;
        }
        public Table GetTableById(int id)
        {
            string query = "SELECT * FROM ThucAnTrenBan WHERE id = @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            if (data.Rows.Count > 0)
            {
                return new Table(data.Rows[0]);
            }
            return null;
        }
    }
}
