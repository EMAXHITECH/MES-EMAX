namespace MES
{
    partial class FileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Delete = new MES.SimpleButtonEx();
            this.btn_Upload = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Download = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Close = new MES.SimpleButtonEx();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.Grid_File = new MES.GridControlEx();
            this.View_File = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_File)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Delete);
            this.panelControl1.Controls.Add(this.btn_Upload);
            this.panelControl1.Controls.Add(this.btn_Download);
            this.panelControl1.Controls.Add(this.btn_Close);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(853, 40);
            this.panelControl1.TabIndex = 2;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Delete.Appearance.Options.UseBackColor = true;
            this.btn_Delete.button_GB = MES.SimpleButtonEx.Button_GB.Delete;
            this.btn_Delete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButtonEx1.ImageOptions.SvgImage")));
            this.btn_Delete.Location = new System.Drawing.Point(693, 3);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Delete.sCHK = "N";
            this.btn_Delete.Size = new System.Drawing.Size(74, 34);
            this.btn_Delete.sSearch = "Y";
            this.btn_Delete.sUpdate = "N";
            this.btn_Delete.TabIndex = 5;
            this.btn_Delete.Text = "삭제";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Upload.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Upload.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11F);
            this.btn_Upload.Appearance.Options.UseBackColor = true;
            this.btn_Upload.Appearance.Options.UseFont = true;
            this.btn_Upload.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Upload.ImageOptions.SvgImage")));
            this.btn_Upload.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btn_Upload.Location = new System.Drawing.Point(519, 3);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(74, 34);
            this.btn_Upload.TabIndex = 4;
            this.btn_Upload.Text = "업로드";
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Download.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Download.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11F);
            this.btn_Download.Appearance.Options.UseBackColor = true;
            this.btn_Download.Appearance.Options.UseFont = true;
            this.btn_Download.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Download.ImageOptions.SvgImage")));
            this.btn_Download.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btn_Download.Location = new System.Drawing.Point(599, 3);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(88, 34);
            this.btn_Download.TabIndex = 3;
            this.btn_Download.Text = "다운로드";
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11F);
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseFont = true;
            this.btn_Close.button_GB = MES.SimpleButtonEx.Button_GB.Exit;
            this.btn_Close.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Close.ImageOptions.SvgImage")));
            this.btn_Close.Location = new System.Drawing.Point(773, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Close.sCHK = "N";
            this.btn_Close.Size = new System.Drawing.Size(74, 34);
            this.btn_Close.sSearch = "Y";
            this.btn_Close.sUpdate = "N";
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.Grid_File);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 40);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(853, 416);
            this.panelControl2.TabIndex = 3;
            // 
            // Grid_File
            // 
            this.Grid_File.AddRowYN = false;
            this.Grid_File.CellFocus = true;
            this.Grid_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_File.EnterYN = true;
            this.Grid_File.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.Grid_File.ExpansionCHK = false;
            this.Grid_File.Location = new System.Drawing.Point(2, 2);
            this.Grid_File.MainView = this.View_File;
            this.Grid_File.MouseWheelChk = true;
            this.Grid_File.MultiSelectChk = true;
            this.Grid_File.Name = "Grid_File";
            this.Grid_File.PopMenuChk = true;
            this.Grid_File.Size = new System.Drawing.Size(849, 412);
            this.Grid_File.TabIndex = 0;
            this.Grid_File.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View_File});
            // 
            // View_File
            // 
            this.View_File.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.View_File.Appearance.FocusedRow.Options.UseBackColor = true;
            this.View_File.GridControl = this.Grid_File;
            this.View_File.Name = "View_File";
            this.View_File.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.View_File.OptionsSelection.MultiSelect = true;
            this.View_File.OptionsView.ShowGroupPanel = false;
            // 
            // FileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 456);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FileForm";
            this.Text = "FileForm";
            this.Load += new System.EventHandler(this.FileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_File)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Download;
        private SimpleButtonEx btn_Close;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private GridControlEx Grid_File;
        private DevExpress.XtraGrid.Views.Grid.GridView View_File;
        private DevExpress.XtraEditors.SimpleButton btn_Upload;
        private SimpleButtonEx btn_Delete;
    }
}