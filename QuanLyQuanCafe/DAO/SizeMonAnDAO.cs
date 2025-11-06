using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyQuanCafe.DAO
{
    public class SizeMonAnDAO
    {
        private static SizeMonAnDAO instance;
        public static SizeMonAnDAO Instance
        {
            get { if (instance == null) instance = new SizeMonAnDAO(); return instance; }
            private set { instance = value; }
        }

        private SizeMonAnDAO() { }

        public List<SizeMonAn> GetListSizeByFoodId(int idMonAn)
        {
            List<SizeMonAn> list = new List<SizeMonAn>();
            string query = "SELECT * FROM SizeMonAn WHERE IdMonAn = @idMonAn";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idMonAn });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new SizeMonAn(row));
            }
            return list;
        }

        public int GetSizeIdByFoodAndSize(int idMonAn, string size)
        {
            string query = "SELECT TOP 1 Id FROM SizeMonAn WHERE IdMonAn = @idMonAn AND LOWER(LTRIM(RTRIM(Size))) = LOWER(LTRIM(RTRIM(@size)))";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { idMonAn, size });

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["Id"]);

            return -1;
        }
    }
}
