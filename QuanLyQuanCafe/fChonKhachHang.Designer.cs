namespace QuanLyQuanCafe
{
    partial class fChonKhachHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.nmUsePoint = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtgvKhachHang = new System.Windows.Forms.DataGridView();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmUsePoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvKhachHang)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(461, 158);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 22);
            this.label10.TabIndex = 32;
            this.label10.Text = " Điểm hiện tại:";
            // 
            // nmUsePoint
            // 
            this.nmUsePoint.Location = new System.Drawing.Point(606, 158);
            this.nmUsePoint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nmUsePoint.Name = "nmUsePoint";
            this.nmUsePoint.Size = new System.Drawing.Size(241, 26);
            this.nmUsePoint.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(461, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 22);
            this.label8.TabIndex = 29;
            this.label8.Text = "Tên Khách:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(71, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 22);
            this.label7.TabIndex = 27;
            this.label7.Text = "SDT:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(606, 101);
            this.txtTenKH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.ReadOnly = true;
            this.txtTenKH.Size = new System.Drawing.Size(240, 26);
            this.txtTenKH.TabIndex = 26;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(140, 164);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.ReadOnly = true;
            this.txtSDT.Size = new System.Drawing.Size(240, 26);
            this.txtSDT.TabIndex = 25;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(516, 32);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(246, 26);
            this.txtSearch.TabIndex = 33;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(770, 32);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 29);
            this.btnSearch.TabIndex = 34;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtgvKhachHang
            // 
            this.dtgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvKhachHang.Location = new System.Drawing.Point(14, 299);
            this.dtgvKhachHang.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgvKhachHang.Name = "dtgvKhachHang";
            this.dtgvKhachHang.RowHeadersWidth = 51;
            this.dtgvKhachHang.RowTemplate.Height = 24;
            this.dtgvKhachHang.Size = new System.Drawing.Size(857, 236);
            this.dtgvKhachHang.TabIndex = 35;
            this.dtgvKhachHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvKhachHang_CellClick);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSelect.Location = new System.Drawing.Point(638, 242);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(84, 49);
            this.btnSelect.TabIndex = 36;
            this.btnSelect.Text = "Chọn";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(759, 242);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 49);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(71, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 22);
            this.label1.TabIndex = 39;
            this.label1.Text = "ID:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(140, 96);
            this.txtId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(240, 26);
            this.txtId.TabIndex = 38;
            // 
            // fChonKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dtgvKhachHang);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nmUsePoint);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.txtSDT);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "fChonKhachHang";
            this.Text = "fChonKhachHang";
            this.Load += new System.EventHandler(this.fChonKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmUsePoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvKhachHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nmUsePoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dtgvKhachHang;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtId;
    }
}