using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }

        }
        private AccountDAO() { }
        public bool Login(string userNamr, string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result=DataProvider.Instance.ExecuteQuery(query,new object[] {userNamr,passWord});
            return result.Rows.Count>0;
        }
        public bool UpdateAccount(string userName, string displayName,string pass, string newPass )
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword",new object[]{userName,displayName,pass,newPass});
            return result > 0;
        }   
        public Account GetAccountByUseName(string userName)
        {
           DataTable data = DataProvider.Instance.ExecuteQuery("select * from TaiKhoan where useName = '" + userName+ "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
           public DataTable GetListAcount()
            {
                return DataProvider.Instance.ExecuteQuery("select UseName,DisplayName,Type from TaiKhoan");
            }
        public bool InsertAcount(string name, string displayName, int type)
        {
            string query = string.Format("insert dbo.TaiKhoan(UseName,DisplayName,Type) values(N'{0}', N'{1}', N'{2}')", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateAcountt( string userName, string displayName, int type)
        {
            string query = string.Format("update dbo.TaiKhoan set DisplayName = N'{1}', Type = {2} where UseName = N'{0}'", userName, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAcount(string name)
        {
            string query = string.Format("delete TaiKhoan where UseName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool ResetAccount(string name)
        {
            string query = string.Format("update TaiKhoan set password = N'0' where UseName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool IsAccountExist(string userName)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE UseName = @name";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { userName });
            return Convert.ToInt32(result) > 0;
        }

    }
}
