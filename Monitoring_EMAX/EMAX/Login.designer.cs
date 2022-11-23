namespace EMAX_Monitoring
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label_Version = new DevExpress.XtraEditors.LabelControl();
            this.btn_Config = new DevExpress.XtraEditors.SimpleButton();
            this.txtPw = new DevExpress.XtraEditors.TextEdit();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.chkID = new DevExpress.XtraEditors.CheckEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Login = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.label_Version);
            this.panelControl1.Controls.Add(this.btn_Config);
            this.panelControl1.Controls.Add(this.txtPw);
            this.panelControl1.Controls.Add(this.txtId);
            this.panelControl1.Controls.Add(this.chkID);
            this.panelControl1.Controls.Add(this.pictureBox1);
            this.panelControl1.Controls.Add(this.btn_Cancel);
            this.panelControl1.Controls.Add(this.btn_Login);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(410, 210);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            this.panelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SplashScreen1_MouseDown);
            this.panelControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SplashScreen1_MouseMove);
            // 
            // label_Version
            // 
            this.label_Version.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label_Version.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Version.Appearance.Options.UseBackColor = true;
            this.label_Version.Appearance.Options.UseFont = true;
            this.label_Version.Appearance.Options.UseTextOptions = true;
            this.label_Version.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label_Version.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label_Version.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.label_Version.AppearanceDisabled.Options.UseBackColor = true;
            this.label_Version.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.label_Version.AppearanceHovered.Options.UseBackColor = true;
            this.label_Version.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.label_Version.AppearancePressed.Options.UseBackColor = true;
            this.label_Version.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label_Version.Location = new System.Drawing.Point(259, 168);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(139, 28);
            this.label_Version.TabIndex = 38;
            this.label_Version.Text = "Version : 1.000\r\nServerVersion : 1.000";
            // 
            // btn_Config
            // 
            this.btn_Config.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Config.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F);
            this.btn_Config.Appearance.Options.UseBackColor = true;
            this.btn_Config.Appearance.Options.UseBorderColor = true;
            this.btn_Config.Appearance.Options.UseFont = true;
            this.btn_Config.Appearance.Options.UseForeColor = true;
            this.btn_Config.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Config.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_Config.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Config.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Config.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Config.AppearancePressed.Options.UseBackColor = true;
            this.btn_Config.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Config.Location = new System.Drawing.Point(358, 101);
            this.btn_Config.LookAndFeel.SkinName = "Office 2016 Black";
            this.btn_Config.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(40, 61);
            this.btn_Config.TabIndex = 37;
            this.btn_Config.Text = "환경\r\n설정";
            this.btn_Config.Click += new System.EventHandler(this.btn_Config_Click);
            // 
            // txtPw
            // 
            this.txtPw.Location = new System.Drawing.Point(139, 137);
            this.txtPw.Margin = new System.Windows.Forms.Padding(2);
            this.txtPw.Name = "txtPw";
            this.txtPw.Properties.UseSystemPasswordChar = true;
            this.txtPw.Size = new System.Drawing.Size(130, 22);
            this.txtPw.TabIndex = 1;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            // 
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtId.Location = new System.Drawing.Point(139, 104);
            this.txtId.Margin = new System.Windows.Forms.Padding(2);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(130, 22);
            this.txtId.TabIndex = 0;
            // 
            // chkID
            // 
            this.chkID.EditValue = true;
            this.chkID.Location = new System.Drawing.Point(139, 168);
            this.chkID.Name = "chkID";
            this.chkID.Properties.Caption = "아이디 저장";
            this.chkID.Size = new System.Drawing.Size(114, 20);
            this.chkID.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::EMAX_Monitoring.Properties.Resources.Login;
            this.pictureBox1.Location = new System.Drawing.Point(11, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(387, 72);
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SplashScreen1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SplashScreen1_MouseMove);
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
            this.btn_Cancel.Location = new System.Drawing.Point(275, 134);
            this.btn_Cancel.LookAndFeel.SkinName = "Office 2016 Black";
            this.btn_Cancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(77, 28);
            this.btn_Cancel.TabIndex = 29;
            this.btn_Cancel.Text = "취소";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Login.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F);
            this.btn_Login.Appearance.Options.UseBackColor = true;
            this.btn_Login.Appearance.Options.UseBorderColor = true;
            this.btn_Login.Appearance.Options.UseFont = true;
            this.btn_Login.Appearance.Options.UseForeColor = true;
            this.btn_Login.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Login.AppearanceDisabled.Options.UseBackColor = true;
            this.btn_Login.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Login.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Login.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(102)))), ((int)(((byte)(156)))));
            this.btn_Login.AppearancePressed.Options.UseBackColor = true;
            this.btn_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Login.Location = new System.Drawing.Point(275, 101);
            this.btn_Login.LookAndFeel.SkinName = "Office 2016 Black";
            this.btn_Login.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(77, 28);
            this.btn_Login.TabIndex = 28;
            this.btn_Login.Text = "로그인";
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.AppearanceDisabled.Options.UseBackColor = true;
            this.labelControl2.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.AppearanceHovered.Options.UseBackColor = true;
            this.labelControl2.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.AppearancePressed.Options.UseBackColor = true;
            this.labelControl2.Location = new System.Drawing.Point(35, 139);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 19);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "PASSWORD";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.AppearanceDisabled.Options.UseBackColor = true;
            this.labelControl1.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.AppearanceHovered.Options.UseBackColor = true;
            this.labelControl1.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.AppearancePressed.Options.UseBackColor = true;
            this.labelControl1.Location = new System.Drawing.Point(105, 106);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(18, 19);
            this.labelControl1.TabIndex = 34;
            this.labelControl1.Text = "ID";
            // 
            // Login
            // 
            this.ActiveGlowColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(410, 210);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("Login.IconOptions.Image")));
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Login_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SplashScreen1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SplashScreen1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Login;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.CheckEdit chkID;
        private DevExpress.XtraEditors.TextEdit txtPw;
        private DevExpress.XtraEditors.TextEdit txtId;
        private DevExpress.XtraEditors.SimpleButton btn_Config;
        private DevExpress.XtraEditors.LabelControl label_Version;
    }
}
