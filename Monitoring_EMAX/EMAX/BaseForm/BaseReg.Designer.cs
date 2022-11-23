namespace EMAX_Monitoring
{
    partial class BaseReg
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseReg));
            this.panReg = new DevExpress.XtraEditors.PanelControl();
            this.panButton = new DevExpress.XtraEditors.PanelControl();
            this.btn_Print = new EMAX_Monitoring.SimpleButtonEx();
            this.btn_Save = new EMAX_Monitoring.SimpleButtonEx();
            this.btn_Close = new EMAX_Monitoring.SimpleButtonEx();
            this.btn_Excel = new EMAX_Monitoring.SimpleButtonEx();
            this.btn_Select = new EMAX_Monitoring.SimpleButtonEx();
            this.btn_Insert = new EMAX_Monitoring.SimpleButtonEx();
            this.btn_Delete = new EMAX_Monitoring.SimpleButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).BeginInit();
            this.panReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panReg
            // 
            this.panReg.Controls.Add(this.panButton);
            this.panReg.Dock = System.Windows.Forms.DockStyle.Top;
            this.panReg.Location = new System.Drawing.Point(0, 0);
            this.panReg.Name = "panReg";
            this.panReg.Size = new System.Drawing.Size(1119, 40);
            this.panReg.TabIndex = 0;
            // 
            // panButton
            // 
            this.panButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panButton.Controls.Add(this.btn_Print);
            this.panButton.Controls.Add(this.btn_Save);
            this.panButton.Controls.Add(this.btn_Close);
            this.panButton.Controls.Add(this.btn_Excel);
            this.panButton.Controls.Add(this.btn_Select);
            this.panButton.Controls.Add(this.btn_Insert);
            this.panButton.Controls.Add(this.btn_Delete);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.panButton.Location = new System.Drawing.Point(564, 2);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(553, 36);
            this.panButton.TabIndex = 21;
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Print.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Print.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Print.Appearance.Options.UseBackColor = true;
            this.btn_Print.Appearance.Options.UseBorderColor = true;
            this.btn_Print.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Print;
            this.btn_Print.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Print.ImageOptions.SvgImage")));
            this.btn_Print.Location = new System.Drawing.Point(398, 1);
            this.btn_Print.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Print.sCHK = "N";
            this.btn_Print.Size = new System.Drawing.Size(74, 34);
            this.btn_Print.sSearch = "Y";
            this.btn_Print.sUpdate = "N";
            this.btn_Print.TabIndex = 53;
            this.btn_Print.TabStop = false;
            this.btn_Print.Text = "출력";
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseBorderColor = true;
            this.btn_Save.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Save;
            this.btn_Save.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Save.ImageOptions.SvgImage")));
            this.btn_Save.Location = new System.Drawing.Point(164, 1);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Save.sCHK = "N";
            this.btn_Save.Size = new System.Drawing.Size(74, 34);
            this.btn_Save.sSearch = "Y";
            this.btn_Save.sUpdate = "N";
            this.btn_Save.TabIndex = 24;
            this.btn_Save.Text = "저장";
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseBorderColor = true;
            this.btn_Close.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Exit;
            this.btn_Close.ImageOptions.Image = global::EMAX_Monitoring.Properties.Resources.cancel_32x32;
            this.btn_Close.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Close.ImageOptions.SvgImage")));
            this.btn_Close.Location = new System.Drawing.Point(476, 1);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Close.sCHK = "N";
            this.btn_Close.Size = new System.Drawing.Size(74, 34);
            this.btn_Close.sSearch = "Y";
            this.btn_Close.sUpdate = "N";
            this.btn_Close.TabIndex = 26;
            this.btn_Close.Text = "닫기";
            // 
            // btn_Excel
            // 
            this.btn_Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Excel.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Excel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Excel.Appearance.Options.UseBackColor = true;
            this.btn_Excel.Appearance.Options.UseBorderColor = true;
            this.btn_Excel.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Excel;
            this.btn_Excel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Excel.ImageOptions.Image")));
            this.btn_Excel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Excel.ImageOptions.SvgImage")));
            this.btn_Excel.Location = new System.Drawing.Point(320, 1);
            this.btn_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Excel.sCHK = "N";
            this.btn_Excel.Size = new System.Drawing.Size(74, 34);
            this.btn_Excel.sSearch = "Y";
            this.btn_Excel.sUpdate = "N";
            this.btn_Excel.TabIndex = 25;
            this.btn_Excel.Text = "엑셀";
            // 
            // btn_Select
            // 
            this.btn_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Select.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Select.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Select.Appearance.Options.UseBackColor = true;
            this.btn_Select.Appearance.Options.UseBorderColor = true;
            this.btn_Select.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Search;
            this.btn_Select.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Select.ImageOptions.SvgImage")));
            this.btn_Select.Location = new System.Drawing.Point(5, 1);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Select.sCHK = "N";
            this.btn_Select.Size = new System.Drawing.Size(74, 34);
            this.btn_Select.sSearch = "Y";
            this.btn_Select.sUpdate = "N";
            this.btn_Select.TabIndex = 21;
            this.btn_Select.Text = "조회";
            // 
            // btn_Insert
            // 
            this.btn_Insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Insert.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Insert.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Insert.Appearance.Options.UseBackColor = true;
            this.btn_Insert.Appearance.Options.UseBorderColor = true;
            this.btn_Insert.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Add;
            this.btn_Insert.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Insert.ImageOptions.SvgImage")));
            this.btn_Insert.Location = new System.Drawing.Point(85, 1);
            this.btn_Insert.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Insert.Name = "btn_Insert";
            this.btn_Insert.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Insert.sCHK = "N";
            this.btn_Insert.Size = new System.Drawing.Size(74, 34);
            this.btn_Insert.sSearch = "Y";
            this.btn_Insert.sUpdate = "N";
            this.btn_Insert.TabIndex = 22;
            this.btn_Insert.Text = "추가";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Delete.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Delete.Appearance.Options.UseBackColor = true;
            this.btn_Delete.Appearance.Options.UseBorderColor = true;
            this.btn_Delete.button_GB = EMAX_Monitoring.SimpleButtonEx.Button_GB.Delete;
            this.btn_Delete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Delete.ImageOptions.SvgImage")));
            this.btn_Delete.Location = new System.Drawing.Point(242, 1);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Delete.sCHK = "N";
            this.btn_Delete.Size = new System.Drawing.Size(74, 34);
            this.btn_Delete.sSearch = "Y";
            this.btn_Delete.sUpdate = "N";
            this.btn_Delete.TabIndex = 23;
            this.btn_Delete.Text = "삭제";
            // 
            // BaseReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panReg);
            this.Name = "BaseReg";
            this.Size = new System.Drawing.Size(1119, 703);
            this.Load += new System.EventHandler(this.BaseUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).EndInit();
            this.panReg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected SimpleButtonEx btn_Save;
        protected SimpleButtonEx btn_Close;
        protected SimpleButtonEx btn_Excel;
        protected SimpleButtonEx btn_Insert;
        protected SimpleButtonEx btn_Delete;
        protected SimpleButtonEx btn_Select;
        protected SimpleButtonEx btn_Print;
        protected DevExpress.XtraEditors.PanelControl panReg;
        protected DevExpress.XtraEditors.PanelControl panButton;
    }
}
