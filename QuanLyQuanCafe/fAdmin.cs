using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource foodlist=new BindingSource();
        BindingSource acountlist=new BindingSource();
        public Account loginAccount;
        public fAdmin()
        {
          
            InitializeComponent();
            dtgvFood.DataSource = foodlist;
            dtgvAccount.DataSource = acountlist;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBinding();
            LoadListCategory();
            LoadListTable();
            AddCategoryBinding();
            AddTableBinding();
            AddAcountBinding();
            LoadAcount();
            LoadTableStatus();

        }
        #region methods
        void AddAcountBinding()
        {
            txbUseName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UseName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAcount()
        {
            acountlist.DataSource = AccountDAO.Instance.GetListAcount();
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listfood =FoodDAO.Instance.SearchFoodByName(name);
            return listfood;
        }
        void LoadDateTimePickerBill()
        {
            DateTime today=DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value=dtpkToDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource= BillDAO.Instance.GetListBillByDate(checkIn,checkOut);
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "name",true,DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        
        }
        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbNameCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinding()
        {
            txbTableId.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));

        }
        private void LoadTableStatus()
        {
            cbTableStatus.Items.Clear();
            cbTableStatus.Items.Add("Trống ");
            cbTableStatus.Items.Add("Có người");
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodlist .DataSource= FoodDAO.Instance.GetListFood();
        }
        void LoadListCategory()
        {
           dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadListTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.IsAccountExist(userName))
            {
                MessageBox.Show("Tên tài khoản đã tồn tại, vui lòng chọn tên khác!");
                return;
            }

            if (AccountDAO.Instance.InsertAcount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
            }

            LoadAcount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAcountt(userName, displayName, type))
            {
                MessageBox.Show("Cập nhập khoản thành công!");

                //if (insertAccount != null)
                //    insertAccount(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhập khoản thất bại");
            }
            LoadAcount();
        }
        void DeleteAccount(string userName)
        {
            if (loginAccount.UseName.Equals(userName))
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!");
                return;
            }
            {

            }
            if (AccountDAO.Instance.DeleteAcount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công!");

                //if (insertAccount != null)
                //    insertAccount(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
            LoadAcount();
        }
        void resetPass(string userName)
        {
            if (AccountDAO.Instance.ResetAccount(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");

                //if (insertAccount != null)
                //    insertAccount(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        #endregion
        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value,dtpkToDate.Value);
            TinhTongDoanhThu();
        }
        private void fAdmin_Load(object sender, EventArgs e)
        {
          
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;

                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name=txbFoodName.Text;
            int categoryID=(cbFoodCategory.SelectedItem as Category).ID;
            float price=(float)nmFoodPrice.Value;
            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công ");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id=Convert.ToInt32(txbFoodID.Text);
            if (FoodDAO.Instance.UpdateFood(id,name, categoryID, price))

            {
                MessageBox.Show("Sửa món thành công ");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {   
            int id = Convert.ToInt32(txbFoodID.Text);
            if (FoodDAO.Instance.DeleteFood(id))

            {
                MessageBox.Show("Xóa món thành công ");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
         private event EventHandler deleteFood;
         public event EventHandler DeleteFood
         {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
         }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
          foodlist.DataSource = SearchFoodByName(txbSreachFoodName.Text);
        }
        #endregion

        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvFood.Rows[e.RowIndex];

                // Gán dữ liệu cho các ô bên phải
                txbFoodID.Text = row.Cells["ID"].Value.ToString();
                txbFoodName.Text = row.Cells["Name"].Value.ToString();
                nmFoodPrice.Text = row.Cells["Price"].Value.ToString();

                string categoryName = row.Cells["CategoryName"].Value.ToString();
                int index = cbFoodCategory.FindStringExact(categoryName);
                if (index != -1)
                {
                    cbFoodCategory.SelectedIndex = index;
                }
            }
        }
      
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbNameCategory.Text;
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadListCategory();
                if (insertCategory != null)
                    insertCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbCategoryID.Text);
            string name = txbNameCategory.Text;

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadListCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbCategoryID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa thành công!");
                LoadListCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        private void dtgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvCategory.Rows[e.RowIndex];
                txbCategoryID.Text = row.Cells["ID"].Value.ToString();
                txbNameCategory.Text = row.Cells["Name"].Value.ToString();
            }
        }
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }
        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn thành công!");
                LoadListTable();
                if (insertTable != null)
                    insertTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại!");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xoá bàn thành công!");
                LoadListTable();
                if (deleteTable != null)
                    deleteTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá bàn thất bại!");
            }
        }

        private void dtgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvTable.CurrentRow != null)
            {
                txbTableId.Text = dtgvTable.CurrentRow.Cells["ID"].Value.ToString();
                txbTableName.Text = dtgvTable.CurrentRow.Cells["Name"].Value.ToString();
               
            }
        }
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }
        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }
        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);
            string name = txbTableName.Text;
            

            if (TableDAO.Instance.UpdateTable(id, name))
            {
                MessageBox.Show("Cập nhật bàn thành công!");
                LoadListTable();
                if (updateTable != null)
                    updateTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cập nhật bàn thất bại!");
            }

        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAcount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string usename= txbUseName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;
            AddAccount(usename, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
        
            string usename = txbUseName.Text;
            DeleteAccount(usename);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string usename = txbUseName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;
            EditAccount(usename, displayName, type);
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string usename = txbUseName.Text;
            resetPass(usename);
        }
        private void TinhTongDoanhThu()
        {
            decimal tongDoanhThu = 0;

            foreach (DataGridViewRow row in dtgvBill.Rows)
            {
                // Bỏ qua hàng cuối cùng (hàng trống để nhập thêm)
                if (row.IsNewRow)
                    continue;

                // Lấy giá trị từ cột "Tổng tiền"
                object cellValue = row.Cells["Tổng tiền"].Value;

                if (cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal tien))
                {
                    tongDoanhThu += tien;
                }
            }

            // Hiển thị ra label
            lblTotalRevenue.Text = tongDoanhThu.ToString("N0") + " VNĐ";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void cbTableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (dtgvTable.SelectedCells.Count > 0)
            //{
            //    int index = dtgvTable.SelectedCells[0].RowIndex;
            //    DataGridViewRow selectedRow = dtgvTable.Rows[index];
            //    selectedRow.Cells["status"].Value = cbTableStatus.SelectedItem.ToString();
            //    int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            //    string status = cbTableStatus.SelectedItem.ToString();
            //    if (TableDAO.Instance.UpdateTableStatus(id, status))
            //    {
            //        MessageBox.Show("Cập nhật trạng thái bàn thành công!");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Cập nhật trạng thái bàn thất bại!");
            //    }
            //}
        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
