﻿namespace SERP
{
    partial class Change_Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Change_Config));
            this.btn_Select = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Port = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_PW = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_DB = new DevExpress.XtraEditors.TextEdit();
            this.txt_IP = new DevExpress.XtraEditors.TextEdit();
            this.xtraTabControl_Config = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Port.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PW.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_DB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl_Config)).BeginInit();
            this.xtraTabControl_Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Select
            // 
            this.btn_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Select.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Select.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.btn_Select.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Select.Appearance.Options.UseBackColor = true;
            this.btn_Select.Appearance.Options.UseBorderColor = true;
            this.btn_Select.Appearance.Options.UseForeColor = true;
            this.btn_Select.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Select.ImageOptions.SvgImage")));
            this.btn_Select.Location = new System.Drawing.Point(113, 219);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(74, 34);
            this.btn_Select.TabIndex = 42;
            this.btn_Select.Text = "적용";
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.btn_Close.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseBorderColor = true;
            this.btn_Close.Appearance.Options.UseForeColor = true;
            this.btn_Close.ImageOptions.SvgImage = global::SERP.Properties.Resources.actions_deletecircled;
            this.btn_Close.Location = new System.Drawing.Point(209, 219);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(74, 34);
            this.btn_Close.TabIndex = 41;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panelControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(402, 183);
            this.xtraTabPage1.Text = "데이터베이스 주소";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.txt_IP);
            this.panelControl1.Controls.Add(this.txt_DB);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.txt_PW);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txt_ID);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txt_Port);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(402, 183);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.AppearanceDisabled.Options.UseBackColor = true;
            this.labelControl1.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.AppearanceHovered.Options.UseBackColor = true;
            this.labelControl1.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.AppearancePressed.Options.UseBackColor = true;
            this.labelControl1.Location = new System.Drawing.Point(59, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(112, 19);
            this.labelControl1.TabIndex = 34;
            this.labelControl1.Text = "접속 주소(IP) : ";
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
            this.labelControl2.Location = new System.Drawing.Point(75, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(96, 19);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "포트(Port) : ";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(182, 43);
            this.txt_Port.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txt_Port.Size = new System.Drawing.Size(203, 22);
            this.txt_Port.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.AppearanceDisabled.Options.UseBackColor = true;
            this.labelControl3.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.AppearanceHovered.Options.UseBackColor = true;
            this.labelControl3.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.AppearancePressed.Options.UseBackColor = true;
            this.labelControl3.Location = new System.Drawing.Point(76, 80);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(95, 19);
            this.labelControl3.TabIndex = 38;
            this.labelControl3.Text = "아이디(ID) : ";
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(182, 77);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(203, 22);
            this.txt_ID.TabIndex = 37;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.AppearanceDisabled.Options.UseBackColor = true;
            this.labelControl4.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.AppearanceHovered.Options.UseBackColor = true;
            this.labelControl4.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.AppearancePressed.Options.UseBackColor = true;
            this.labelControl4.Location = new System.Drawing.Point(53, 112);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(118, 19);
            this.labelControl4.TabIndex = 40;
            this.labelControl4.Text = "패스워드(PW) : ";
            // 
            // txt_PW
            // 
            this.txt_PW.Location = new System.Drawing.Point(182, 112);
            this.txt_PW.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PW.Name = "txt_PW";
            this.txt_PW.Properties.UseSystemPasswordChar = true;
            this.txt_PW.Size = new System.Drawing.Size(203, 22);
            this.txt_PW.TabIndex = 39;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseBackColor = true;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl5.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.labelControl5.AppearanceDisabled.Options.UseBackColor = true;
            this.labelControl5.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.labelControl5.AppearanceHovered.Options.UseBackColor = true;
            this.labelControl5.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.labelControl5.AppearancePressed.Options.UseBackColor = true;
            this.labelControl5.Location = new System.Drawing.Point(5, 145);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(166, 19);
            this.labelControl5.TabIndex = 44;
            this.labelControl5.Text = "데이터베이스(DB) 명 : ";
            // 
            // txt_DB
            // 
            this.txt_DB.Location = new System.Drawing.Point(182, 145);
            this.txt_DB.Margin = new System.Windows.Forms.Padding(2);
            this.txt_DB.Name = "txt_DB";
            this.txt_DB.Size = new System.Drawing.Size(203, 22);
            this.txt_DB.TabIndex = 43;
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(182, 12);
            this.txt_IP.Margin = new System.Windows.Forms.Padding(2);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(203, 22);
            this.txt_IP.TabIndex = 45;
            // 
            // xtraTabControl_Config
            // 
            this.xtraTabControl_Config.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraTabControl_Config.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl_Config.Name = "xtraTabControl_Config";
            this.xtraTabControl_Config.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl_Config.Size = new System.Drawing.Size(404, 214);
            this.xtraTabControl_Config.TabIndex = 1;
            this.xtraTabControl_Config.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // Change_Config
            // 
            this.ActiveGlowColor = System.Drawing.Color.Black;
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(404, 260);
            this.ControlBox = false;
            this.Controls.Add(this.xtraTabControl_Config);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Select);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.ShowIcon = false;
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Change_Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "접속 주소 변경";
            this.Load += new System.EventHandler(this.Change_Config_Load);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Port.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PW.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_DB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl_Config)).EndInit();
            this.xtraTabControl_Config.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_Select;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txt_IP;
        private DevExpress.XtraEditors.TextEdit txt_DB;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txt_PW;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_ID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_Port;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl_Config;
    }
}
