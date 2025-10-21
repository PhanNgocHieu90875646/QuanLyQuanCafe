using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }

        }
        private FoodDAO() { }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "select * from MonAn where idcategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<FoodView> GetListFood()
        {
            List<FoodView> list = new List<FoodView>();
            string query = @"SELECT f.ID, f.Name, f.Price, f.idCategory, 
                                    c.Name AS CategoryName, f.SoLuongTon, f.ImagePath
                             FROM MONAN f
                             JOIN DANHMUCMONAN c ON f.idCategory = c.ID";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                FoodView food = new FoodView(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string keyword = RemoveDiacritics(name).ToLower();

            string query = "SELECT f.Id, f.Name, f.IdCategory, f.Price, f.SoLuongTon, f.ImagePath " +
                           "FROM dbo.MonAn AS f " +
                           "JOIN dbo.DanhMucMonAn AS c ON f.IdCategory = c.Id";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                string foodName = RemoveDiacritics(item["Name"].ToString()).ToLower();
                if (foodName.Contains(keyword))
                {
                    Food food = new Food(item);
                    list.Add(food);
                }
            }

            return list;
        }
        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        public bool InsertFood(string name, int idCategory, float price, int soLuongTon, string imagePath)
        {
            string query = "INSERT INTO MonAn (name, idCategory, price, SoLuongTon, ImagePath) " +
                           "VALUES ( @name , @idCategory , @price , @soLuongTon , @imagePath )";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
        name, idCategory, price, soLuongTon, imagePath
            });

            return result > 0;
        }

        public bool UpdateFood(int id, string name, int idCategory, float price, int soLuongTon, string imagePath)
        {
            string query = "UPDATE MonAn SET name = @name , idCategory = @idCategory , " +
                           "price = @price , SoLuongTon = @soLuongTon , ImagePath = @imagePath " +
                           "WHERE id = @id";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
        name, idCategory, price, soLuongTon, imagePath, id
            });

            return result > 0;
        }


        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByIdFood(idFood);
            string query = string.Format("DELETE MonAn WHERE id = {0}", idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool IsFoodNameExists(string name)
        {
            string query = "SELECT COUNT(*) FROM MonAn WHERE name = @name";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { name });
            return Convert.ToInt32(result) > 0;
        }
        public int GetFoodStock(int foodID)
        {
            string query = "SELECT SoLuongTon FROM MonAn WHERE id = @id";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { foodID });
            return Convert.ToInt32(result);
        }

        public void UpdateFoodStock(int foodID, int newStock)
        {
            string query = "UPDATE MonAn SET SoLuongTon = @SoLuongTon WHERE id = @id";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { newStock, foodID });
        }

    }
}
