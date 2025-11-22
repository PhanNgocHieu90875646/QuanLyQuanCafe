using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fChonKhachHang : Form
    {
        public KhachHang SelectedKhach { get; private set; }

        public fChonKhachHang()
        {
            InitializeComponent();
            dtgvKhachHang.CellClick += dtgvKhachHang_CellClick;
        }

        private void fChonKhachHang_Load(object sender, EventArgs e)
        {
            dtgvKhachHang.DataSource = KhachHangDAO.Instance.GetAllKhachHang();
        }
        private void LoadDanhSachKhach()
        {
            dtgvKhachHang.DataSource = KhachHangDAO.Instance.GetAllKhachHang();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sdt = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!");
                return;
            }

            // Tìm khách theo SĐT
            KhachHang kh = KhachHangDAO.Instance.GetKHByPhone(sdt);

            // ============================== KHÔNG TÌM THẤY ==============================
            if (kh == null)
            {
                DialogResult ask = MessageBox.Show(
                    "Không tìm thấy khách hàng!\nBạn có muốn tạo khách mới không?",
                    "Khách hàng mới",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (ask == DialogResult.No)
                    return;

                // ======= Nhập tên khách nếu chưa có ========
                string ten = txtTenKH.Text.Trim();

                if (string.IsNullOrEmpty(ten))
                {
                    // mở form nhập tên nếu bạn muốn
                    FormNhapTen f = new FormNhapTen();
                    if (f.ShowDialog() != DialogResult.OK)
                        return;

                    ten = f.TenKhach;
                }

                // ======= Tạo khách mới ========
                bool ok = KhachHangDAO.Instance.Inserrt(ten, sdt);

                if (!ok)
                {
                    MessageBox.Show("Lỗi thêm khách mới!");
                    return;
                }

                // Tải lại khách sau khi tạo
                kh = KhachHangDAO.Instance.GetKHByPhone(sdt);

                MessageBox.Show("Đã tạo khách hàng mới!");

                // Load lại danh sách khách trong DataGridView
                LoadDanhSachKhach();

                // Đồng thời fill vào textbox
                txtTenKH.Text = kh.TenKH;
                txtSDT.Text = kh.SoDienThoai;
            }

            // ============================== LOAD VÀO FORM ==============================
            txtTenKH.Text = kh.TenKH;
            nmUsePoint.Value = kh.DiemTichLuy;

      
        }

        // CLICK VÀO DÒNG → ĐỔ LÊN TEXTBOX
        private void dtgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dtgvKhachHang.Rows[e.RowIndex];

            txtId.Text = row.Cells["Id"].Value.ToString();
            txtTenKH.Text = row.Cells["TenKH"].Value.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
            nmUsePoint.Text = row.Cells["DiemTichLuy"].Value.ToString();
        }

        // NÚT CHỌN → TRẢ VỀ FORM CHÍNH
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng", "Thông báo");
                return;
            }

            SelectedKhach = new KhachHang()
            {
                Id = int.Parse(txtId.Text),
                TenKH = txtTenKH.Text,
                SoDienThoai = txtSDT.Text,
                DiemTichLuy = int.Parse(nmUsePoint.Text)
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
