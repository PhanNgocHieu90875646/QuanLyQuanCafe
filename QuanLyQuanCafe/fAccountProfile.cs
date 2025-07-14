using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAcount(loginAccount); }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }
        void ChangeAcount(Account acc)
        {
            txbUseName.Text=LoginAccount.UseName;
            txbDisplayName.Text=LoginAccount.DisplayName;
        }
        void UpdateAccountInfo()
        {
            string displayName=txbDisplayName.Text;
            string password=txbPassword.Text;
            string newPassword=txbNewPass.Text;
            string reenterPass=txbReEnterPass.Text;
            string userName=txbUseName.Text;
            if(!newPassword.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mặt khẩu mới");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(userName,displayName,password,newPassword))
                {
                    MessageBox.Show("Cập nhập thành công");
                    if(updateAccount != null)
                        updateAccount(this,new AccountEvent(AccountDAO.Instance.GetAccountByUseName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; } 
        }
        private void btnExti_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;
        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }
        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
        
}
