namespace MES
{
    partial class Exit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Exit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.panel1.Controls.Add(this.btn_Exit);
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 170);
            this.panel1.TabIndex = 0;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Exit.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F);
            this.btn_Exit.Appearance.Options.UseBackColor = true;
            this.btn_Exit.Appearance.Options.UseFont = true;
            this.btn_Exit.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Exit.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_Exit.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Exit.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Exit.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Exit.AppearancePressed.Options.UseBackColor = true;
            this.btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Exit.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.btn_Exit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.btn_Exit.Location = new System.Drawing.Point(79, 127);
            this.btn_Exit.LookAndFeel.SkinName = "Office 2016 Black";
            this.btn_Exit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(77, 28);
            this.btn_Exit.TabIndex = 0;
            this.btn_Exit.Text = "종료";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F);
            this.btn_Cancel.Appearance.Options.UseBackColor = true;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Cancel.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_Cancel.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Cancel.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Cancel.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Cancel.AppearancePressed.Options.UseBackColor = true;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Cancel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.btn_Cancel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.btn_Cancel.Location = new System.Drawing.Point(193, 127);
            this.btn_Cancel.LookAndFeel.SkinName = "Office 2016 Black";
            this.btn_Cancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(77, 28);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "취소";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click_1);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 13F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(64, 89);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(224, 20);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "프로그램을 종료하시겠습니까?";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MES.Properties.Resources.Application_Exit;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(349, 72);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // Exit
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.Appearance.Options.UseImage = true;
            this.Appearance.Options.UseTextOptions = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(362, 170);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("나눔바른고딕", 12F);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Exit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public DevExpress.XtraEditors.SimpleButton btn_Exit;
        public DevExpress.XtraEditors.SimpleButton btn_Cancel;
    }
}