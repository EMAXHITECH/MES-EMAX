namespace MES
{
    partial class PopAuthoriyCopyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopAuthoriyCopyForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txt_UserName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Save = new MES.SimpleButtonEx();
            this.btn_Close = new MES.SimpleButtonEx();
            this.gc_User = new MES.GridControlEx();
            this.gv_User = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_User)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txt_UserName);
            this.panelControl1.Controls.Add(this.labelControl19);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(950, 51);
            this.panelControl1.TabIndex = 0;
            // 
            // txt_UserName
            // 
            this.txt_UserName.Enabled = false;
            this.txt_UserName.Location = new System.Drawing.Point(83, 13);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Properties.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.txt_UserName.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11F);
            this.txt_UserName.Properties.Appearance.Options.UseBackColor = true;
            this.txt_UserName.Properties.Appearance.Options.UseFont = true;
            this.txt_UserName.Size = new System.Drawing.Size(141, 24);
            this.txt_UserName.TabIndex = 51;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl19.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Appearance.Options.UseForeColor = true;
            this.labelControl19.Location = new System.Drawing.Point(12, 16);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(55, 17);
            this.labelControl19.TabIndex = 50;
            this.labelControl19.Text = "원본 사원";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.btn_Save);
            this.panelControl2.Controls.Add(this.btn_Close);
            this.panelControl2.Location = new System.Drawing.Point(781, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(165, 43);
            this.panelControl2.TabIndex = 10;
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseBorderColor = true;
            this.btn_Save.button_GB = MES.SimpleButtonEx.Button_GB.Save;
            this.btn_Save.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Save.ImageOptions.SvgImage")));
            this.btn_Save.Location = new System.Drawing.Point(6, 4);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Save.sCHK = "N";
            this.btn_Save.Size = new System.Drawing.Size(74, 34);
            this.btn_Save.sSearch = "Y";
            this.btn_Save.sUpdate = "N";
            this.btn_Save.TabIndex = 23;
            this.btn_Save.Text = "저장";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseBorderColor = true;
            this.btn_Close.button_GB = MES.SimpleButtonEx.Button_GB.Exit;
            this.btn_Close.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.ImageOptions.Image")));
            this.btn_Close.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Close.ImageOptions.SvgImage")));
            this.btn_Close.Location = new System.Drawing.Point(85, 4);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Close.sCHK = "N";
            this.btn_Close.Size = new System.Drawing.Size(74, 34);
            this.btn_Close.sSearch = "Y";
            this.btn_Close.sUpdate = "N";
            this.btn_Close.TabIndex = 24;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // gc_User
            // 
            this.gc_User.AddRowYN = false;
            this.gc_User.CellFocus = true;
            this.gc_User.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_User.EnterYN = true;
            this.gc_User.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.gc_User.ExpansionCHK = false;
            this.gc_User.Location = new System.Drawing.Point(0, 51);
            this.gc_User.MainView = this.gv_User;
            this.gc_User.MouseWheelChk = true;
            this.gc_User.MultiSelectChk = true;
            this.gc_User.Name = "gc_User";
            this.gc_User.PopMenuChk = true;
            this.gc_User.Size = new System.Drawing.Size(950, 569);
            this.gc_User.TabIndex = 1;
            this.gc_User.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_User});
            // 
            // gv_User
            // 
            this.gv_User.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gv_User.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_User.GridControl = this.gc_User;
            this.gv_User.Name = "gv_User";
            this.gv_User.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_User.OptionsSelection.MultiSelect = true;
            this.gv_User.OptionsView.ShowGroupPanel = false;
            // 
            // PopAuthoriyCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 620);
            this.Controls.Add(this.gc_User);
            this.Controls.Add(this.panelControl1);
            this.Name = "PopAuthoriyCopyForm";
            this.Text = "권한 복사";
            this.Load += new System.EventHandler(this.PopAuthoriyCopyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_User)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private SimpleButtonEx btn_Save;
        private SimpleButtonEx btn_Close;
        private GridControlEx gc_User;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_User;
        private DevExpress.XtraEditors.TextEdit txt_UserName;
        private DevExpress.XtraEditors.LabelControl labelControl19;
    }
}