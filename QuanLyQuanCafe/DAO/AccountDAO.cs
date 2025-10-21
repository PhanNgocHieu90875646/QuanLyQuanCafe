using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                return DataProvider.Instance.ExecuteQuery("SELECT UseName, DisplayName, Type, IdNhanVien FROM TaiKhoan");
            }
        public bool InsertAccount(string userName, string displayName, int type, int idNhanVien)
        {
            string query = "INSERT INTO TaiKhoan(UseName, DisplayName, Type, IdNhanVien) VALUES ( @userName , @displayName , @type , @idNhanVien )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, type, idNhanVien });
            return result > 0;
        }

        // Sửa tài khoản
        public bool UpdateAccount(string userName, string displayName,  int type, int idNhanVien)
        {
            try
            {
                string query = "UPDATE TaiKhoan SET DisplayName = @displayName , Type = @type , IdNhanVien = @idNhanVien WHERE UseName = @userName";
                int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { displayName, type, idNhanVien, userName });

                return result > 0;
            }
            catch (SqlException ex)
            {
                // 🔴 Nếu lỗi là do khóa ngoại FK_TaiKhoan_NhanVien
                if (ex.Message.Contains("FK_TaiKhoan_NhanVien"))
                {
                    MessageBox.Show("Id nhân viên không tồn tại trong bảng Nhân viên!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
