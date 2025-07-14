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
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }

        }
        private CategoryDAO() { }
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from DanhMucMonAn";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }
            return list;
        }
        public Category GetCategoryByID(int id)
        {
            Category category=null;
            string query = "select * from DanhMucMonAn where id = " +id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                 category = new Category(item);
                return category;
            }
            return category;
        }
        public bool InsertCategory(string name)
        {
            string query = $"INSERT INTO DanhMucMonAn (name) VALUES (N'{name}')";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateCategory(int id, string name)
        {
            string query = $"UPDATE DanhMucMonAn SET name = N'{name}' WHERE id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteCategory(int id)
        {
            string query = $"DELETE FROM DanhMucMonAn WHERE id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
    }
}
