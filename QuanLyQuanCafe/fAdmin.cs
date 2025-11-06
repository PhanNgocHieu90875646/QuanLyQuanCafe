using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource foodlist = new BindingSource();
        BindingSource acountlist = new BindingSource();
        public Account loginAccount;
        public fAdmin()
        {

            InitializeComponent();
            LoadDateTimePickerBill();
            this.txbImagePath.TextChanged += txbImagePath_TextChanged;
            dtgvFood.DataSource = foodlist;
            dtgvAccount.DataSource = acountlist;
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListBillByDateHD(dtpkFromDate.Value, dtpkToDate.Value);

            LoadListFood();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBinding();
            LoadListCategory();
            LoadListTable();
            AddCategoryBinding();
            AddTableBinding();
            LoadNhanVienIntoComboBox();
            AddAcountBinding();
            LoadAcount();
            LoadTableStatus();
            LoadNhanVien();
            AddNhanVienBinding();
            LoadGioiTinh();
            LoadChucVu();

        }
        #region methods
        void LoadNhanVien()
        {
            dtgvNV.DataSource = NhanVienDAO.Instance.GetListNhanVien();
        }

        void AddAcountBinding()
        {
            txbUseName.DataBindings.Clear();
            txbDisplayName.DataBindings.Clear();
            numericUpDown1.DataBindings.Clear();
            cbNhanVien.DataBindings.Clear();

            // Binding các trường tài khoản
            txbUseName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UseName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));

            // Binding combobox nhân viên theo IdNhanVien (cột bạn đã thêm trong bảng TaiKhoan)
            cbNhanVien.DataBindings.Add(new Binding("SelectedValue", dtgvAccount.DataSource, "IdNhanVien", true, DataSourceUpdateMode.Never));
        }
        void LoadNhanVienIntoComboBox()
        {
            List<NhanVien> listNhanVien = NhanVienDAO.Instance.GetListNhanVien(); // danh sách nhân viên

            cbNhanVien.DataSource = listNhanVien;
            cbNhanVien.DisplayMember = "HoTen";
            cbNhanVien.ValueMember = "Id";
        }

        void LoadAcount()
        {
            acountlist.DataSource = AccountDAO.Instance.GetListAcount();
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listfood = FoodDAO.Instance.SearchFoodByName(name);
            return listfood;
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkToDate.Value.AddMonths(1).AddDays(-1);
            //int currentYear = DateTime.Now.Year;
            //dtpkFromDate.Value = new DateTime(currentYear, 1, 1);     // 01/01/năm hiện tại
            //dtpkToDate.Value = new DateTime(currentYear, 12, 31);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDateHD(checkIn, checkOut);

        }
        void LoadListBillByDateHD(DateTime checkIn, DateTime checkOut)
        {

            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDateHD(checkIn, checkOut);

        }
        void AddNhanVienBinding()
        {
            txbId.DataBindings.Clear();
            txbHoTen.DataBindings.Clear();
            dtpNgaySinh.DataBindings.Clear();
            cbGioiTinh.DataBindings.Clear();
            txbSoDienThoai.DataBindings.Clear();
            txbDiaChi.DataBindings.Clear();
            cbRole.DataBindings.Clear();

            txbId.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbHoTen.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "HoTen", true, DataSourceUpdateMode.Never));
            dtpNgaySinh.DataBindings.Add(new Binding("Value", dtgvNV.DataSource, "NgaySinh", true, DataSourceUpdateMode.Never));
            cbGioiTinh.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never));
            txbSoDienThoai.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "SoDienThoai", true, DataSourceUpdateMode.Never));
            txbDiaChi.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
            cbRole.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "Role", true, DataSourceUpdateMode.Never));
        }
        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Clear();
            txbFoodName.DataBindings.Clear();
            nmFoodPrice.DataBindings.Clear();
            nmSoLuongTon.DataBindings.Clear();
            txbImagePath.DataBindings.Clear();
            cbFoodCategory.DataBindings.Clear();

            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
            nmSoLuongTon.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "SoLuongTon", true, DataSourceUpdateMode.Never));
            txbImagePath.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ImagePath", true, DataSourceUpdateMode.Never));

            // Bind combobox danh mục (chỉ lấy ID, còn hiển thị tên thì load riêng vào combobox)
            cbFoodCategory.DataBindings.Add(new Binding("SelectedValue", dtgvFood.DataSource, "IdCategory", true, DataSourceUpdateMode.Never));
            if (dtgvFood.Rows.Count > 0)
            {
                string path = dtgvFood.Rows[0].Cells["ImagePath"].Value?.ToString();
                LoadImage(path);
            }
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
            //cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }
        private void LoadTableStatus()
        {
            //cbTableStatus.Items.Clear();
            //cbTableStatus.Items.Add("Trống ");
            //cbTableStatus.Items.Add("Có người");
        }
        private void LoadGioiTinh()
        {
            cbGioiTinh.Items.Clear();
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
        }
        private void LoadChucVu()
        {
            cbRole.Items.Clear();
            cbRole.Items.Add("Thu ngân");
            cbRole.Items.Add("Phục vụ");
            cbRole.Items.Add("Trông xe");
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";  // Hiển thị tên danh mục
            cb.ValueMember = "ID";      // Giá trị là idCategory
        }
        void LoadListbill()
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListbill();
        }
        void LoadListFood()
        {
            foodlist.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadListTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void AddAccount(string userName, string displayName, int type, int idNhanVien)
        {
            if (AccountDAO.Instance.IsAccountExist(userName))
            {
                MessageBox.Show("Tên tài khoản đã tồn tại, vui lòng chọn tên khác!");
                return;
            }

            if (AccountDAO.Instance.InsertAccount(userName, displayName, type, idNhanVien))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
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
        void LoadImage(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {
                    picFood.Image = Image.FromFile(path);
                    picFood.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    picFood.Image = null; // hoặc gán ảnh mặc định
                }
            }
            catch
            {
                picFood.Image = null;
            }
        }

        #endregion
        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            TinhTongDoanhThu();
            //LoadListbill();
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
            string name = txbFoodName.Text;
            string priceText = nmFoodPrice.Text;



            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;

            int soLuongTon = (int)nmSoLuongTon.Value;
            string imagePath = txbImagePath.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên món không được để trống!");
                return;
            }

            if (string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("Giá món không được để trống!");
                return;
            }

            if (!float.TryParse(priceText, out float price) || price < 0)
            {
                MessageBox.Show("Giá món phải là số hợp lệ và không âm!");
                return;
            }

            // 🔹 Kiểm tra trùng tên khi thêm
            if (FoodDAO.Instance.IsFoodNameExists(name))
            {
                MessageBox.Show("Tên món đã tồn tại, vui lòng nhập tên khác!");
                return;
            }

            int idCategory = (cbFoodCategory.SelectedItem as Category).ID;


            if (FoodDAO.Instance.InsertFood(name, categoryID, price, soLuongTon, imagePath))
            {
                MessageBox.Show("Thêm món ăn thành công!");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món ăn!");
            }

        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int soLuongTon = (int)nmSoLuongTon.Value;
            string imagePath = txbImagePath.Text;

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price, soLuongTon, imagePath))
            {
                MessageBox.Show("Cập nhật món ăn thành công!");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật món ăn!");
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
            dtgvFood.DataSource = FoodDAO.Instance.SearchFoodByName(txbSreachFoodName.Text);
        }
        #endregion

        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dtgvFood.Rows.Count)
                return;

            DataGridViewRow row = dtgvFood.Rows[e.RowIndex];

            // Kiểm tra cột ImagePath có tồn tại không
            if (!dtgvFood.Columns.Contains("ImagePath"))
                return;

            object cellValue = row.Cells["ImagePath"].Value;
            if (cellValue == null || cellValue == DBNull.Value)
            {
                picFood.Image = null;
                txbImagePath.Text = "";
                return;
            }

            string imagePath = cellValue.ToString().Trim();
            if (string.IsNullOrEmpty(imagePath))
            {
                picFood.Image = null;
                txbImagePath.Text = "";
                return;
            }

            string imagesDir = Path.Combine(Application.StartupPath, "Images");
            string fullPath = Path.Combine(imagesDir, imagePath);

            try
            {
                // Delay nhỏ để đảm bảo DataGridView đã cập nhật CurrentRow
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (File.Exists(fullPath))
                    {
                        if (picFood.Image != null)
                        {
                            picFood.Image.Dispose();
                            picFood.Image = null;
                        }

                        picFood.Image = Image.FromFile(fullPath);
                        picFood.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        picFood.Image = null;
                    }

                    txbImagePath.Text = imagePath;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                picFood.Image = null;
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
            string name = txbNameCategory.Text.Trim();

            // 1. Kiểm tra trống
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên danh mục không được để trống!", "Thông báo");
                return;
            }

            // 2. Kiểm tra trùng
            List<Category> list = CategoryDAO.Instance.GetListCategory(); // Lấy tất cả danh mục
            bool isDuplicate = list.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                MessageBox.Show("Tên danh mục đã tồn tại, vui lòng nhập tên khác!", "Thông báo");
                return;
            }

            // 3. Thêm
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
            string name = txbNameCategory.Text.Trim();

            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên danh mục không được để trống!");
                return;
            }

            // 2. Lấy danh sách danh mục và kiểm tra trùng
            var existing = CategoryDAO.Instance.GetListCategory(); // trả về List<Category>
            bool isDuplicate = existing.Any(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && c.ID != id);

            if (isDuplicate)
            {
                MessageBox.Show("Tên danh mục đã tồn tại!");
                return;
            }

            // 3. Cập nhật
            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadListCategory();
                updateCategory?.Invoke(this, new EventArgs());
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
            string name = txbTableName.Text.Trim();

            // 1. Kiểm tra để trống
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên bàn không được để trống!", "Thông báo");
                return;
            }

            // 2. Kiểm tra trùng tên
            List<Table> tables = TableDAO.Instance.LoadTableList();
            bool isDuplicate = tables.Any(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
            {
                MessageBox.Show("Tên bàn đã tồn tại. Vui lòng nhập tên khác!", "Thông báo");
                return;
            }

            // 3. Thêm bàn
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

            if (string.IsNullOrEmpty(txbTableId.Text))
            {
                MessageBox.Show("Vui lòng chọn bàn để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(txbTableId.Text);
            string name = txbTableName.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên bàn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên cũ
            var oldTable = TableDAO.Instance.GetTableById(id);
            if (oldTable == null)
            {
                MessageBox.Show("Không tìm thấy bàn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu đổi sang tên mới thì kiểm tra trùng
            if (!name.Equals(oldTable.Name, StringComparison.OrdinalIgnoreCase) &&
                TableDAO.Instance.IsTableNameExists(name))
            {
                MessageBox.Show("Tên bàn đã tồn tại, vui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật
            if (TableDAO.Instance.UpdateTable(id, name))
            {
                MessageBox.Show("Cập nhật bàn thành công!");
                LoadListTable();
                updateTable?.Invoke(this, EventArgs.Empty);
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
            string usename = txbUseName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;
            int idNhanVien = (int)cbNhanVien.SelectedValue; // nếu bạn có combobox chọn nhân viên
            AddAccount(usename, displayName, type, idNhanVien);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {

            string usename = txbUseName.Text;
            DeleteAccount(usename);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUseName.Text.Trim();
            string displayName = txbDisplayName.Text.Trim();
            int type = (int)numericUpDown1.Value;

            // 👉 Lấy Id nhân viên an toàn
            int idNhanVien = 0;
            if (cbNhanVien.SelectedValue != null && int.TryParse(cbNhanVien.SelectedValue.ToString(), out int temp))
                idNhanVien = temp;

            // 👉 Kiểm tra dữ liệu cơ bản trước khi cập nhật
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type, idNhanVien))
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAcount();
                LoadNhanVienIntoComboBox(); // Refresh lại combobox nếu có thay đổi
            }
            else
            {
                // 👉 Kiểm tra lỗi IdNhanVien không tồn tại (xử lý trong DAO)
                MessageBox.Show("Cập nhật thất bại! Vui lòng kiểm tra lại Id nhân viên hoặc dữ liệu nhập.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string usename = txbUseName.Text;
            resetPass(usename);
        }
        private void TinhTongDoanhThu()
        {
            // Lấy ngày bắt đầu và kết thúc (tuỳ theo form bạn)
            DateTime fromDate = dtpkFromDate.Value;
            DateTime toDate = dtpkToDate.Value;

            // Load dữ liệu hóa đơn vào datagridview
            DataTable data = BillDAO.Instance.GetListBillByDateHD(fromDate, toDate);
            dtgvBill.DataSource = data;

            // Sau khi load xong => tính tổng doanh thu
            decimal tongDoanhThu = 0m;

            // Kiểm tra cột tồn tại
            if (dtgvBill.Columns.Contains("TongTien"))
            {
                foreach (DataGridViewRow row in dtgvBill.Rows)
                {
                    if (row.IsNewRow) continue;

                    object cellValue = row.Cells["TongTien"].Value;
                    if (cellValue != null && cellValue != DBNull.Value)
                    {
                        decimal value;
                        // Loại bỏ ký tự không cần thiết rồi parse
                        string str = cellValue.ToString().Replace(".", "").Replace(",", "").Trim();
                        if (decimal.TryParse(str, out value))
                        {
                            tongDoanhThu += value;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy cột 'TongTien' trong bảng hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Hiển thị ra label
            lblTotalRevenue.Text = $" {tongDoanhThu:N0} VNĐ";
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

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string imagesDir = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(imagesDir)) Directory.CreateDirectory(imagesDir);

                string src = ofd.FileName;
                string ext = Path.GetExtension(src);
                string newName = Guid.NewGuid().ToString() + ext;  // tránh trùng tên
                string dest = Path.Combine(imagesDir, newName);

                File.Copy(src, dest, true); // copy file

                // Lưu tên file vào textbox để insert DB
                txbImagePath.Text = newName;

                // Hiển thị ảnh vừa chọn lên PictureBox
                if (picFood.Image != null) picFood.Image.Dispose();
                picFood.Image = Image.FromFile(dest);
                picFood.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void txbImagePath_TextChanged(object sender, EventArgs e)
        {
            LoadImage(txbImagePath.Text);
        }

        private void dtgvFood_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string hoTen = txbHoTen.Text;
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cbGioiTinh.Text;
            string sdt = txbSoDienThoai.Text;
            string diaChi = txbDiaChi.Text;
            string role = cbRole.Text;

            if (NhanVienDAO.Instance.InsertNhanVien(hoTen, ngaySinh, gioiTinh, sdt, diaChi, role))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbId.Text);
            string hoTen = txbHoTen.Text;
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cbGioiTinh.Text;
            string sdt = txbSoDienThoai.Text;
            string diaChi = txbDiaChi.Text;
            string role = cbRole.Text;

            if (NhanVienDAO.Instance.UpdateNhanVien(id, hoTen, ngaySinh, gioiTinh, sdt, diaChi, role))
            {
                MessageBox.Show("Cập nhật nhân viên thành công!");
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Cập nhật nhân viên thất bại!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txbId.Text);

                if (NhanVienDAO.Instance.DeleteNhanVien(id))
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadNhanVien(); // reload lại danh sách nhân viên
                }
                else
                {
                    MessageBox.Show("Xóa nhân thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void dgvBillHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewBillHD_Click(object sender, EventArgs e)
        {

        }

        private void btnCloseBill_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {

        }

        private void dgvBillInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cellValue = dtgvBill.Rows[e.RowIndex].Cells["ID"].Value;

                // Kiểm tra null hoặc DBNull trước khi chuyển đổi
                if (cellValue != null && cellValue != DBNull.Value && int.TryParse(cellValue.ToString(), out int idBill))
                {
                    // Load chi tiết hóa đơn
                    dtgvBillDetail.DataSource = BillInfoDAO.Instance.GetBillInfoByBillId(idBill);
                }
                else
                {
                    // Tránh lỗi khi ô trống hoặc dữ liệu sai
                    dtgvBillDetail.DataSource = null;
                    MessageBox.Show("Dữ liệu hóa đơn không hợp lệ hoặc đang trống.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //GetBillInfoByBillId
        private void button7_Click(object sender, EventArgs e)
        {
            if (dtgvBill.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dtgvBill.SelectedRows[0];
            int idBill = Convert.ToInt32(row.Cells["ID"].Value);
            int status = Convert.ToInt32(row.Cells["TrangThai"].Value);

            // ✅ Chỉ cho phép xóa hóa đơn đã thanh toán
            if (status == 0)
            {
                MessageBox.Show("Không thể xóa hóa đơn chưa thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa hóa đơn ID = {idBill} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                if (BillDAO.Instance.DeleteBill(idBill))
                {
                    MessageBox.Show("Đã xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListBillByDateHD(dtpkFromDate.Value, dtpkToDate.Value);
                }
                else
                {
                    MessageBox.Show("Xóa hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dtgvFood_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgvFood.CurrentRow == null) return;

            DataGridViewRow row = dtgvFood.CurrentRow;

            // Kiểm tra cột "ImagePath" có tồn tại không
            if (!dtgvFood.Columns.Contains("ImagePath")) return;

            // Lấy giá trị cột
            object cellValue = row.Cells["ImagePath"].Value;
            if (cellValue == null || cellValue == DBNull.Value)
            {
                picFood.Image = null;
                txbImagePath.Text = "";
                return;
            }

            string imagePath = cellValue.ToString().Trim();
            if (string.IsNullOrEmpty(imagePath))
            {
                picFood.Image = null;
                txbImagePath.Text = "";
                return;
            }

            string imagesDir = Path.Combine(Application.StartupPath, "Images");
            string fullPath = Path.Combine(imagesDir, imagePath);

            try
            {
                if (File.Exists(fullPath))
                {
                    if (picFood.Image != null)
                    {
                        picFood.Image.Dispose();
                        picFood.Image = null;
                    }

                    picFood.Image = Image.FromFile(fullPath);
                    picFood.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    picFood.Image = null;
                }

                txbImagePath.Text = imagePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                picFood.Image = null;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void cbMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtgvSizeMonAndtgvSizeMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }
    }
}