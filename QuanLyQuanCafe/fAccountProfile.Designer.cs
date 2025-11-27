namespace QuanLyQuanCafe
{
    partial class fAccountProfile
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbUseName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbDisplayName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txbNewPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txbReEnterPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExti = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txbUseName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(25, 39);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 62);
            this.panel2.TabIndex = 2;
            // 
            // txbUseName
            // 
            this.txbUseName.Location = new System.Drawing.Point(183, 16);
            this.txbUseName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbUseName.Name = "txbUseName";
            this.txbUseName.ReadOnly = true;
            this.txbUseName.Size = new System.Drawing.Size(424, 26);
            this.txbUseName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên hiển thị:";
            // 
            // txbDisplayName
            // 
            this.txbDisplayName.Location = new System.Drawing.Point(183, 16);
            this.txbDisplayName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbDisplayName.Name = "txbDisplayName";
            this.txbDisplayName.Size = new System.Drawing.Size(424, 26);
            this.txbDisplayName.TabIndex = 1;
            this.txbDisplayName.TextChanged += new System.EventHandler(this.txbDisplayName_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbDisplayName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(25, 109);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 62);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mật khẩu:";
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(183, 16);
            this.txbPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(424, 26);
            this.txbPassword.TabIndex = 1;
            this.txbPassword.UseSystemPasswordChar = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txbPassword);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(25, 179);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(620, 62);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txbNewPass);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(25, 249);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(620, 62);
            this.panel4.TabIndex = 5;
            // 
            // txbNewPass
            // 
            this.txbNewPass.Location = new System.Drawing.Point(183, 16);
            this.txbNewPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbNewPass.Name = "txbNewPass";
            this.txbNewPass.Size = new System.Drawing.Size(424, 26);
            this.txbNewPass.TabIndex = 1;
            this.txbNewPass.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mật khẩu mới:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txbReEnterPass);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(25, 319);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(620, 62);
            this.panel5.TabIndex = 6;
            // 
            // txbReEnterPass
            // 
            this.txbReEnterPass.Location = new System.Drawing.Point(183, 16);
            this.txbReEnterPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbReEnterPass.Name = "txbReEnterPass";
            this.txbReEnterPass.Size = new System.Drawing.Size(424, 26);
            this.txbReEnterPass.TabIndex = 1;
            this.txbReEnterPass.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(3, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 29);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nhập lại:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Peru;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUpdate.Location = new System.Drawing.Point(389, 389);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(118, 48);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Cập nhập";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExti
            // 
            this.btnExti.BackColor = System.Drawing.Color.Peru;
            this.btnExti.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExti.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExti.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExti.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExti.Location = new System.Drawing.Point(532, 389);
            this.btnExti.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExti.Name = "btnExti";
            this.btnExti.Size = new System.Drawing.Size(112, 48);
            this.btnExti.TabIndex = 8;
            this.btnExti.Text = "Thoát";
            this.btnExti.UseVisualStyleBackColor = false;
            this.btnExti.Click += new System.EventHandler(this.btnExti_Click);
            // 
            // fAccountProfile
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.CancelButton = this.btnExti;
            this.ClientSize = new System.Drawing.Size(658, 454);
            this.Controls.Add(this.btnExti);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "fAccountProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin cá nhân";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txbUseName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbDisplayName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txbNewPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txbReEnterPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExti;
    }
}