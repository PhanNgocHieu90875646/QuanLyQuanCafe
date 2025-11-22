using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

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
        public bool KiemTraMonAnCoSize(int idMon)
        {
            string query = "SELECT COUNT(*) FROM SizeMonAn WHERE IdMonAn = @idMon";
            int count = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idMon });
            return count > 0;
        }
        public List<SizeMonAn> GetListSizeByFoodIdd(int id)
        {
            List<SizeMonAn> list = new List<SizeMonAn>();

            string query = "SELECT * FROM SizeMonAn WHERE IdMonAn = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                SizeMonAn size = new SizeMonAn(item);
                list.Add(size);
            }

            return list;
        }
        public bool IsSizeInUse(int idSize)
        {
            string query = "SELECT COUNT(*) FROM ThongTinHoaDon WHERE IdSizeMonAn = @idSize";
            int count = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idSize });
            return count > 0;
        }

        public int GetSizeIdByFoodAndSize(int idMonAn, string size)
        {
            string query = "SELECT TOP 1 Id FROM SizeMonAn WHERE IdMonAn = @idMonAn AND LOWER(LTRIM(RTRIM(Size))) = LOWER(LTRIM(RTRIM(@size)))";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { idMonAn, size });

            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["Id"]);

            return -1;
        }
        public List<SizeMonAn> GetListSize()
        {
            List<SizeMonAn> list = new List<SizeMonAn>();
            string query = @"SELECT sma.Id, sma.IdMonAn, f.name, sma.Size, sma.Gia
                             FROM SizeMonAn sma
                             JOIN MonAn f ON sma.IdMonAn = f.Id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                list.Add(new SizeMonAn(row));
            }
            return list;
        }

        public bool InsertSize(int idMonAn, string size, decimal gia)
        {
            //string query = "INSERT INTO SizeMonAn (IdMonAn, Size, Gia) VALUES (@idMonAn, @size, @gia)";
            string query = $"INSERT INTO SizeMonAn (IdMonAn, Size, Gia) VALUES ({idMonAn},N'{size}',{gia})";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
     

        public bool UpdateSize(int id, int idMonAn, string size, decimal gia)
        {
            string query = $"UPDATE SizeMonAn SET IdMonAn = {idMonAn}, Size = N'{size}', Gia = {gia} WHERE Id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteSize(int id)
        {
            string query = $"DELETE FROM SizeMonAn WHERE Id = {id}";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
    }
}
