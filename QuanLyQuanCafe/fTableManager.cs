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
using System.IO;
using System.Drawing.Printing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.VisualBasic;



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
        private KhachHang currentKhachHang = null;


        public fTableManager(Account acc, NhanVien staff)
        {
            InitializeComponent();
            this.currentAccount = acc;
            this.currentStaff = staff;
            this.LoginAccount = acc;
           
            LoadTable();
            LoadCategory();
     
            LoadComboboxTable(cbSwitchTable);
            LoadKhuyenMai();
        }

        #region Method
        public void ExportBillToPDF(int idBill)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PDF (*.pdf)|*.pdf";
            saveFile.FileName = $"HoaDon_{idBill}.pdf";

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            // Lấy thông tin hóa đơn
            Bill bill = BillDAO.Instance.GetBillInfo(idBill);
            List<BillInfo> listBillInfo = BillInfoDAO.Instance.GetBillInfoByBillId(idBill);
            double tongTien = BillDAO.Instance.GetTotalPrice(idBill);

            Document doc = new Document(PageSize.A4);
            PdfWriter.GetInstance(doc, new FileStream(saveFile.FileName, FileMode.Create));

            doc.Open();

            string ARIAL_FONT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");

            // Tạo BaseFont (bf) và Font (font, fontBold) hỗ trợ Unicode
            BaseFont bf = BaseFont.CreateFont(ARIAL_FONT, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            // Định nghĩa các Font để sử dụng trong tài liệu
            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD); // Dùng cho Tiêu đề
            iTextSharp.text.Font fontInfo = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL); // Dùng cho thông tin hóa đơn
                                                                                                               
            // Sử dụng fontTitle đã được tạo bằng bf (BaseFont hỗ trợ Unicode)
            Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", fontTitle);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            doc.Add(new Paragraph("\n"));

            // Thông tin hóa đơn
            // Sử dụng fontInfo (thay thế biến font cũ)
            doc.Add(new Paragraph($"Mã hóa đơn: {bill.ID}", fontInfo));
            doc.Add(new Paragraph($"Ngày vào: {bill.DateCheckIn}", fontInfo));
            doc.Add(new Paragraph($"Ngày thanh toán: {bill.DateCheckOnt}", fontInfo));
            doc.Add(new Paragraph($"Nhân viên: {bill.TenNhanVien}", fontInfo));
            doc.Add(new Paragraph($"Khuyến mãi: {bill.TenKhuyenMai}", fontInfo));
            //doc.Add(new Paragraph($"Giảm giá điểm: {bill.GiamGiaDiem}", fontInfo));
            doc.Add(new Paragraph("\n"));

            // Bảng món ăn
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 4f, 2f, 2f, 2f, 2f });

            table.AddCell("Tên món");
            table.AddCell("Size");
            table.AddCell("SL");
            table.AddCell("Đơn giá");
            table.AddCell("Thành tiền");

            foreach (var item in listBillInfo)
            {
                table.AddCell(item.TenMon);
                table.AddCell(item.SizeMon);
                table.AddCell(item.SoLuong.ToString());
                table.AddCell(item.DonGia.ToString());
                table.AddCell(item.ThanhTien.ToString());
            }

            doc.Add(table);

                        doc.Add(new Paragraph(
                $"\nTổng thanh toán: {tongTien} VNĐ",
                new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD)
            ));

            doc.Close();

            MessageBox.Show("Xuất hóa đơn thành công!", "Thông báo");
        }

        private bool isLoadingPromotion = false;
        private double GetTongTienHienTai()
        {
            double tong = 0;
            foreach (ListViewItem item in lsvBill.Items)
            {
                tong += Convert.ToDouble(item.SubItems[3].Text); // cột Thành tiền
            }
            return tong;
        }

        void LoadKhuyenMai()
        {
            isLoadingPromotion = true; // ✅ Bắt đầu gán dữ liệu

            cbKhuyenMai.DataSource = PromotionDAO.Instance.GetActivePromotions();
            cbKhuyenMai.DisplayMember = "TenKM";
            cbKhuyenMai.ValueMember = "Id";
            cbKhuyenMai.SelectedIndex = -1; // Không chọn mặc định

            isLoadingPromotion = false; // ✅ Cho phép event chạy lại
        }
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
        void LoadSzListBySzyID(int id)
        {

            List<SizeMonAn> listSize = SizeMonAnDAO.Instance.GetListSizeByFoodIdd(id);

            cbSize.DataSource = listSize;
            cbSize.DisplayMember = "Size";   // hoặc "TenSize" nếu bạn đặt tên vậy trong DTO
            cbSize.ValueMember = "Id";
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
                ListViewItem lsvitem= new ListViewItem(item.TenMon.ToString());            
                lsvitem.SubItems.Add(item.SoLuong.ToString());
                lsvitem.SubItems.Add(item.DonGia.ToString());
                lsvitem.SubItems.Add(item.ThanhTien.ToString());
                lsvitem.SubItems.Add(item.SizeMon);
                totalPrice += item.ThanhTien;
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

            f.InsertSz += f_InsertSz;
            f.UpdateSz += f_UpdateSz;
            f.DeleteSz += f_DeleteSz;
            f.ShowDialog();
        }

        private void f_DeleteSz(object sender, EventArgs e)
        {
            if (cbSize.SelectedItem == null)
                return;

            SizeMonAn selectedSize = cbSize.SelectedItem as SizeMonAn;
            if (selectedSize == null)
                return;

            LoadSzListBySzyID(selectedSize.Id);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_UpdateSz(object sender, EventArgs e)
        {
            if (cbSize.SelectedItem == null)
                return;

            SizeMonAn selectedSize = cbSize.SelectedItem as SizeMonAn;
            if (selectedSize == null)
                return;

            LoadSzListBySzyID(selectedSize.Id);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_InsertSz(object sender, EventArgs e)
        {
            if (cbSize.SelectedItem == null)
                return;

            SizeMonAn selectedSize = cbSize.SelectedItem as SizeMonAn;
            if (selectedSize == null)
                return;

            LoadSzListBySzyID(selectedSize.Id);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_DeleteTable(object sender, EventArgs e)
        {
            LoadTableListByCategoryID((cbCategory.SelectedItem as Category).ID);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_UpdateTable(object sender, EventArgs e)
        {
            LoadTableListByCategoryID((cbCategory.SelectedItem as Category).ID);

            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }

            LoadTable();
        }

        private void f_InsertTable(object sender, EventArgs e)
        {
            LoadTableListByCategoryID((cbCategory.SelectedItem as Table).ID);

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

            if (cbSize.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn size món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)mnFoodCount.Value;

            // Lấy size được chọn
            var selectedSize = cbSize.SelectedItem as SizeMonAn;
            if (selectedSize == null)
            {
                MessageBox.Show("Không tìm thấy size món trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tính đơn giá (giá món + giá size)aaa
            float giaMon = (cbFood.SelectedItem as Food).Price;
            float giaSize = (float)selectedSize.Gia;
            float donGia = (float)giaMon + giaSize;

            int idSizeMonAn = selectedSize.Id;

            int currentStock = FoodDAO.Instance.GetFoodStock(foodID);
            if (count > currentStock)
            {
                MessageBox.Show("Số lượng bạn chọn vượt quá số lượng tồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                idBill = BillDAO.Instance.GetMaxIDBill();

                int idNV = LoginAccount.IdNhanVien;  // ← bạn đang có biến nhân viên đang đăng nhập
                BillDAO.Instance.SetStaffForBill(idBill, idNV);
            }

            // Thêm món vào bill với đúng size + giá size
            BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count, idSizeMonAn, donGia);

            FoodDAO.Instance.UpdateFoodStock(foodID, currentStock - count);

            ShowBill(table.ID);
            LoadTable();

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            // ================== 0. Lấy bàn ==================
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

            // =====================================================================================
            // 1. KHÁCH HÀNG – KHÔNG BẮT BUỘC
            // =====================================================================================

            string sdt = txtSDT.Text.Trim();
            KhachHang kh = null;
            int? idKhachHang = null;

            if (!string.IsNullOrEmpty(sdt))
            {
                kh = KhachHangDAO.Instance.GetKHByPhone(sdt);

                if (kh == null)
                {
                    DialogResult rs = MessageBox.Show(
                        "Số điện thoại chưa có trong hệ thống.\n" +
                        "Bạn có muốn tạo khách mới để tích điểm không?",
                        "Khách mới",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (rs == DialogResult.Yes)
                    {
                        if (string.IsNullOrEmpty(txtTenKH.Text))
                        {
                            MessageBox.Show("Vui lòng nhập tên khách hàng!");
                            return;
                        }

                        KhachHangDAO.Instance.Insert(txtTenKH.Text, sdt);
                        kh = KhachHangDAO.Instance.GetKHByPhone(sdt);
                    }
                }

                if (kh != null)
                    idKhachHang = kh.Id;
            }

            int diemHienCo = kh?.DiemTichLuy ?? 0;

            // =====================================================================================
            // 2. TÍNH TIỀN
            // =====================================================================================

            double totalPrice = GetTongTienHienTai();
            if (totalPrice <= 0)
            {
                MessageBox.Show("Tổng tiền phải lớn hơn 0!");
                return;
            }

            double finalTotalPrice = totalPrice;
            double giamGia = 0;
            int? idKhuyenMai = null;

            // =============== 2.1 Khuyến mãi ===============
            if (cbKhuyenMai.SelectedItem != null)
            {
                Promotion km = cbKhuyenMai.SelectedItem as Promotion;

                if (km != null)
                {
                    if (totalPrice >= km.DieuKienToiThieu)
                    {
                        idKhuyenMai = km.Id;

                        if (km.LoaiKM == "Phần trăm")
                            giamGia = totalPrice * km.GiaTri / 100;
                        else
                            giamGia = km.GiaTri;

                        finalTotalPrice -= giamGia;
                    }
                    else
                    {
                        MessageBox.Show("Chưa đủ điều kiện khuyến mãi!");
                        return;
                    }
                }
            }

            // =============== 2.2 Giảm giá thủ công ===============
            int giamThuCong = (int)nmDisCount.Value;

            if (giamThuCong > 0)
            {
                double giam = totalPrice * giamThuCong / 100.0;
                giamGia += giam;
                finalTotalPrice -= giam;
            }

            // =============== 2.3 Dùng điểm (NHẬP THỦ CÔNG) ===============

            int diemDung = (int)nmUsePoint.Value;  // lấy giá trị user nhập

            if (kh != null)
            {
                // KHÔNG ĐƯỢC DÙNG QUÁ SỐ ĐIỂM HIỆN CÓ
                if (diemDung > diemHienCo)
                {
                    MessageBox.Show($"Khách chỉ có {diemHienCo} điểm!");
                    return;
                }

                // ĐIỂM GIỚI HẠN TỐI ĐA 50%
                if (diemDung > 50)
                {
                    MessageBox.Show("Chỉ được dùng tối đa 50 điểm (tương đương 50%)!");
                    return;
                }

                if (diemDung > 0)
                {
                    double giam = totalPrice * diemDung / 100.0;
                    giamGia += giam;
                    finalTotalPrice -= giam;
                }
            }

            if (finalTotalPrice < 0)
                finalTotalPrice = 0;

            // =====================================================================================
            // 3. XÁC NHẬN
            // =====================================================================================

            string tenKM = cbKhuyenMai.SelectedItem != null ?
                (cbKhuyenMai.SelectedItem as Promotion).TenKM :
                "Không có";

            if (MessageBox.Show(
                $"Bàn: {table.Name}\n" +
                $"Tổng: {totalPrice:N0} đ\n" +
                $"Khuyến mãi: {tenKM}\n" +
                $"Giảm thủ công: {giamThuCong}%\n" +
                $"Dùng điểm: {diemDung}%\n" +
                $"==> Phải trả: {finalTotalPrice:N0} đ\n\n" +
                $"Xác nhận thanh toán?",
                "Thanh toán",
                MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;

            // =====================================================================================
            // 4. CẬP NHẬT ĐIỂM
            // =====================================================================================

            if (kh != null)
            {
                int diemCong = (int)(finalTotalPrice / 10000);  // 10k = 1 điểm
                int diemMoi = kh.DiemTichLuy - diemDung + diemCong;

                if (diemMoi < 0) diemMoi = 0;

                KhachHangDAO.Instance.UpdateDiem(kh.Id, diemMoi);
            }

            // =====================================================================================
            // 5. CHECKOUT
            // =====================================================================================

            BillDAO.Instance.CheckOut(
                idBill,
                giamThuCong,
                finalTotalPrice,
                currentStaff.Id,
                idKhuyenMai,
                idKhachHang,
                diemDung
            );

            MessageBox.Show("Thanh toán thành công!");

            ShowBill(table.ID);
            LoadTable();
        }
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

        private void cbFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;

            // Lấy món ăn được chọn
            Food selectedFood = cb.SelectedItem as Food;
            if (selectedFood == null) return;

            int idFood = selectedFood.ID;

            try
            {
                // Giả sử trong DB cột Image chỉ lưu tên file, ví dụ: "trasua.png"
                string imagePath = Application.StartupPath + @"\Images\" + selectedFood.ImagePath;

                if (File.Exists(imagePath))
                {
                    picFoodImage.Image = System.Drawing.Image.FromFile(imagePath);
                }
                else
                {
                    picFoodImage.Image = null;
                }
            }
            catch
            {
                picFoodImage.Image = null;
            }

            // Load size theo món ăn
            cbSize.DataSource = SizeMonAnDAO.Instance.GetListSizeByFoodId(idFood);
            cbSize.DisplayMember = "Size";
        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingPromotion) return; // ✅ Không làm gì khi đang load
            if (cbKhuyenMai.SelectedItem == null) return;

            Promotion km = cbKhuyenMai.SelectedItem as Promotion;
            double tongTien = GetTongTienHienTai();

            if (tongTien < km.DieuKienToiThieu)
            {
                MessageBox.Show("Chưa đủ điều kiện áp dụng khuyến mãi này!");
                cbKhuyenMai.SelectedIndex = -1; // Bỏ chọn để tránh lặp
                return;
            }

            double giam = 0;
            if (km.LoaiKM == "Phần trăm")
                giam = tongTien * km.GiaTri / 100;
            else if (km.LoaiKM == "Số tiền")
                giam = km.GiaTri;

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Table table = lsvBill.Tag as Table;
            //if (table == null)
            //{
            //    MessageBox.Show("Không có bàn nào được chọn!");
            //    return;
            //}

            //int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);
            //if (idBill == -1)
            //{
            //    MessageBox.Show("Bàn này chưa có hóa đơn!");
            //    return;
            //}

            //SaveFileDialog save = new SaveFileDialog();
            //save.Filter = "PDF file (*.pdf)|*.pdf";
            //save.FileName = $"HoaDon_Ban{table.Name}_{DateTime.Now:ddMMyyyyHHmm}.pdf";

            //if (save.ShowDialog() == DialogResult.OK)
            //{
            //    PrintDocument doc = new PrintDocument();
            //    doc.PrintPage += (s, ev) =>
            //    {
            //        float y = 20;
            //        Font font = new Font("Arial", 12);

            //        ev.Graphics.DrawString("HÓA ĐƠN THANH TOÁN",
            //            new Font("Arial", 16, FontStyle.Bold),
            //            Brushes.Black, 200, y);

            //        y += 50;

            //        List<BillInfo> list = BillInfoDAO.Instance.GetBillInfoByBillId(idBill);

            //        foreach (var item in list)
            //        {
            //            string line = $"{item.TenMon} ({item.SizeMon}) x{item.SoLuong}  =  {item.ThanhTien:N0}₫";
            //            ev.Graphics.DrawString(line, font, Brushes.Black, 50, y);
            //            y += 25;
            //        }

            //        y += 20;
            //        var total = BillDAO.Instance.GetTotalPrice(idBill);
            //        ev.Graphics.DrawString($"TỔNG TIỀN: {total:N0}₫",
            //            new Font("Arial", 14, FontStyle.Bold),
            //            Brushes.Black, 50, y);
            //    };

            //    // Xuất PDF không cần thư viện ngoài
            //    doc.PrinterSettings.PrintToFile = true;
            //    doc.PrinterSettings.PrintFileName = save.FileName;

            //    doc.Print();

            //    MessageBox.Show("Xuất hóa đơn thành công!");
            //}
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo");
                return;
            }

            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);

            if (idBill == -1)
            {
                MessageBox.Show("Bàn chưa có hóa đơn!", "Thông báo");
                return;
            }

            ExportBillToPDF(idBill);
        }
        //private void ChangeItemEdit(bool mode, bool isInCart = false)
        //{
        //    itemRemove.Enabled = mode && isInCart;
        //    if (!mode)
        //    {
        //        itemName.Text = "";
        //        itemPrice.Text = "";
        //        itemId.Text = "";
        //        itemPicture.Image = null;
        //    }

        //    itemQuantity.Enabled = mode;
        //    itemSave.Enabled = mode;
        //    itemCancel.Enabled = mode;
        //}
      

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private int? currentCustomerId = null;
        private void btnTimKhachHang_Click(object sender, EventArgs e)
        {
            fChonKhachHang f = new fChonKhachHang();
            if (f.ShowDialog() == DialogResult.OK)
            {
                currentKhachHang = f.SelectedKhach;

                // Gán ra giao diện
                txtTenKH.Text = currentKhachHang.TenKH;
                txtSDT.Text = currentKhachHang.SoDienThoai;
                nmUsePoint.Text = currentKhachHang.DiemTichLuy.ToString();
            }
        }

        private void btnCancelFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo");
                return;
            }

            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.ID);

            if (idBill == -1)
            {
                MessageBox.Show("Bàn này chưa gọi món nào!", "Thông báo");
                return;
            }

            // Hiển thị xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy toàn bộ món của bàn \"" + table.Name + "\" không?",
                "Xác nhận hủy món",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
                return;

            // Xóa toàn bộ món trong hóa đơn
            BillInfoDAO.Instance.DeleteAllFoodByBillID(idBill);

            // Cập nhật giao diện
            ShowBill(table.ID);
            LoadTable();

            MessageBox.Show("Đã hủy toàn bộ món của bàn " + table.Name, "Thông báo");
        }
    }
}
