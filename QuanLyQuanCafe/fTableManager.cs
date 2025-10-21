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
    public partial class fTableManager : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAcount(loginAccount.Type); }
        }
        private Account currentAccount;
        private NhanVien currentStaff;

        public fTableManager(Account acc, NhanVien staff)
        {
            InitializeComponent();
            this.currentAccount = acc;
            this.currentStaff = staff;
            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbSwitchTable);
        }

        #region Method
        void ChangeAcount(int type)
        {
            adminToolStripMenuItem.Enabled=type==1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "(" + LoginAccount.DisplayName + ")";
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void LoadCategoryListByCategoryID(int id)
        {
            Category category = CategoryDAO.Instance.GetCategoryByID(id);
            cbCategory.DataSource = new List<Category> { category };
            cbCategory.DisplayMember = "Name";
        }
        void LoadTableListByCategoryID(int id)
        {
            List<Table> tableList = TableDAO.Instance.GetTableListByCategoryID(id);
            flpTable.Controls.Clear(); // Xóa các control cũ

            foreach (Table item in tableList)
            {
                Button btn = new Button()
                {
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHeight,
                    Text = item.Name + Environment.NewLine + item.Status,
                    Tag = item
                };

                btn.Click += btn_Click;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Green;
                        break;
                    default:
                        btn.BackColor = Color.Orange;
                        break;
                }

                flpTable.Controls.Add(btn); // Thêm Button vào FlowLayoutPanel
            }
        }

        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name +Environment.NewLine+item.Status;
                btn.Click +=btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống ":
                        btn.BackColor = Color.Green;
                        break;
                    default:
                        btn.BackColor = Color.Orange;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvitem= new ListViewItem(item.FoodName.ToString());
                lsvitem.SubItems.Add(item.Count.ToString());
                lsvitem.SubItems.Add(item.Price.ToString());
                lsvitem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvitem);
            }
            txbTotalPrice.Text = totalPrice.ToString("c");
           
        }
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        #endregion

        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableId=((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableId);
        }
        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text="Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }
        void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
                 ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }
        void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertFood += f_InsertFood;
            f.UpdateFood += f_UpdateFood;
            f.DeleteFood += f_DeleteFood;

            f.InsertCategory += f_InsertCategory;
            f.UpdateCategory += f_UpdateCategory;
            f.DeleteCategory += f_DeleteCategory;

            f.InsertTable += f_InsertTable;
            f.UpdateTable += f_UpdateTable;
            f.DeleteTable += f_DeleteTable;
            f.ShowDialog();
        }

        private void f_DeleteTable(object sender, EventArgs e)
        {
            LoadCategoryListByCategoryID((cbCategory.SelectedItem as Category).ID);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_UpdateTable(object sender, EventArgs e)
        {
            LoadCategoryListByCategoryID((cbCategory.SelectedItem as Category).ID);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_InsertTable(object sender, EventArgs e)
        {
            LoadCategoryListByCategoryID((cbCategory.SelectedItem as Category).ID);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategoryListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void f_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategoryListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void f_InsertCategory(object sender, EventArgs e)
        {
            LoadCategoryListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb= sender as ComboBox;
            if(cb.SelectedItem==null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodListByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn!");
                return;
            }
            if (cbFood.SelectedItem == null)
            {
                MessageBox.Show("Món ăn không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)mnFoodCount.Value;

            // 🔎 Lấy số lượng tồn kho từ DB
            int currentStock = FoodDAO.Instance.GetFoodStock(foodID);

            if (count > currentStock)
            {
                MessageBox.Show("Số lượng bạn chọn vượt quá số lượng tồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ❌ Không cho thêm
            }

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }
            // ✅ Giảm số lượng tồn trong DB
            FoodDAO.Instance.UpdateFoodStock(foodID, currentStock - count);

            ShowBill(table.ID);
            LoadTable();
           
        }
      
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi thanh toán!");
                return;
            }

            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);

            if (idBill == -1)
            {
                MessageBox.Show("Bàn này chưa có món để thanh toán!");
                return;
            }


            int discount = (int)nmDisCount.Value;
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0].Replace(".", ""));
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            if (totalPrice <= 0)
            {
                MessageBox.Show("Tổng tiền hóa đơn phải lớn hơn 0 mới được thanh toán!", "Lỗi");
                return;
            }

            if (MessageBox.Show(
                string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}?\nTổng tiền = {1} - ({1} / 100) x {2} = {3}",
                table.Name, totalPrice, discount, finalTotalPrice),
                "Xác nhận thanh toán", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
             BillDAO.Instance.CheckOut(idBill, discount, totalPrice, currentStaff.Id);
             ShowBill(table.ID);                   LoadTable();
            }}
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;

            if (MessageBox.Show(
                    string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}?", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name),
                    "Thông báo",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);
                LoadTable();
            }

        }

        #endregion

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDisCount_Click(object sender, EventArgs e)
        {

        }

        private void nmDisCount_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
