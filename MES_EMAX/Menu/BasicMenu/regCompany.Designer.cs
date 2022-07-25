namespace MES
{
    partial class regCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(regCompany));
            this.Grid_Company = new MES.GridControlEx();
            this.View_Company = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).BeginInit();
            this.panReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Company)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseBorderColor = true;
            this.btn_Save.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Save.ImageOptions.SvgImage")));
            // 
            // btn_Close
            // 
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseBorderColor = true;
            this.btn_Close.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.ImageOptions.Image")));
            this.btn_Close.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Close.ImageOptions.SvgImage")));
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Excel
            // 
            this.btn_Excel.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Excel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Excel.Appearance.Options.UseBackColor = true;
            this.btn_Excel.Appearance.Options.UseBorderColor = true;
            this.btn_Excel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Excel.ImageOptions.Image")));
            this.btn_Excel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Excel.ImageOptions.SvgImage")));
            this.btn_Excel.Click += new System.EventHandler(this.btn_Excel_Click);
            // 
            // btn_Insert
            // 
            this.btn_Insert.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Insert.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Insert.Appearance.Options.UseBackColor = true;
            this.btn_Insert.Appearance.Options.UseBorderColor = true;
            this.btn_Insert.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Insert.ImageOptions.SvgImage")));
            this.btn_Insert.Click += new System.EventHandler(this.btn_Insert_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Delete.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Delete.Appearance.Options.UseBackColor = true;
            this.btn_Delete.Appearance.Options.UseBorderColor = true;
            this.btn_Delete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Delete.ImageOptions.SvgImage")));
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Select.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Select.Appearance.Options.UseBackColor = true;
            this.btn_Select.Appearance.Options.UseBorderColor = true;
            this.btn_Select.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Select.ImageOptions.SvgImage")));
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Print.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Print.Appearance.Options.UseBackColor = true;
            this.btn_Print.Appearance.Options.UseBorderColor = true;
            this.btn_Print.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Print.ImageOptions.SvgImage")));
            // 
            // panReg
            // 
            this.panReg.Size = new System.Drawing.Size(1611, 40);
            // 
            // panButton
            // 
            this.panButton.Location = new System.Drawing.Point(1056, 2);
            // 
            // Grid_Company
            // 
            this.Grid_Company.AddRowYN = false;
            this.Grid_Company.CellFocus = true;
            this.Grid_Company.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Company.EnterYN = true;
            this.Grid_Company.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.Grid_Company.ExpansionCHK = false;
            this.Grid_Company.Location = new System.Drawing.Point(0, 40);
            this.Grid_Company.MainView = this.View_Company;
            this.Grid_Company.MouseWheelChk = true;
            this.Grid_Company.MultiSelectChk = true;
            this.Grid_Company.Name = "Grid_Company";
            this.Grid_Company.PopMenuChk = true;
            this.Grid_Company.Size = new System.Drawing.Size(1611, 1015);
            this.Grid_Company.TabIndex = 1;
            this.Grid_Company.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View_Company});
            // 
            // View_Company
            // 
            this.View_Company.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.View_Company.Appearance.FocusedRow.Options.UseBackColor = true;
            this.View_Company.GridControl = this.Grid_Company;
            this.View_Company.Name = "View_Company";
            this.View_Company.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.View_Company.OptionsSelection.MultiSelect = true;
            this.View_Company.OptionsView.ShowAutoFilterRow = true;
            this.View_Company.OptionsView.ShowGroupPanel = false;
            this.View_Company.DoubleClick += new System.EventHandler(this.View_Company_DoubleClick);
            // 
            // regCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grid_Company);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "regCompany";
            this.Size = new System.Drawing.Size(1611, 1055);
            this.Load += new System.EventHandler(this.regCompany_Load);
            this.Controls.SetChildIndex(this.panReg, 0);
            this.Controls.SetChildIndex(this.Grid_Company, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).EndInit();
            this.panReg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Company)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GridControlEx Grid_Company;
        private DevExpress.XtraGrid.Views.Grid.GridView View_Company;
    }
}
