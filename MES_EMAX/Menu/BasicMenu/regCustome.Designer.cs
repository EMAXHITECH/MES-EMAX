namespace SERP
{
    partial class regCustome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(regCustome));
            this.gc_Cust = new SERP.GridControlEx();
            this.gv_Cust = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).BeginInit();
            this.panReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Cust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Cust)).BeginInit();
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
            this.btn_Close.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_Excel
            // 
            this.btn_Excel.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Excel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Excel.Appearance.Options.UseBackColor = true;
            this.btn_Excel.Appearance.Options.UseBorderColor = true;
            this.btn_Excel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Excel.ImageOptions.Image")));
            this.btn_Excel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Excel.ImageOptions.SvgImage")));
            this.btn_Excel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btn_Insert
            // 
            this.btn_Insert.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Insert.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Insert.Appearance.Options.UseBackColor = true;
            this.btn_Insert.Appearance.Options.UseBorderColor = true;
            this.btn_Insert.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Insert.ImageOptions.SvgImage")));
            this.btn_Insert.Click += new System.EventHandler(this.btnInsert_Click);
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
            this.btn_Select.Click += new System.EventHandler(this.btnSelect_Click);
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
            this.panReg.Size = new System.Drawing.Size(1100, 40);
            // 
            // panButton
            // 
            this.panButton.Location = new System.Drawing.Point(545, 2);
            // 
            // gc_Cust
            // 
            this.gc_Cust.AddRowYN = false;
            this.gc_Cust.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_Cust.CellFocus = true;
            this.gc_Cust.EnterYN = true;
            this.gc_Cust.Execl_GB = SERP.GridControlEx.Excel_GB.Update;
            this.gc_Cust.ExpansionCHK = false;
            this.gc_Cust.Location = new System.Drawing.Point(0, 42);
            this.gc_Cust.MainView = this.gv_Cust;
            this.gc_Cust.MouseWheelChk = false;
            this.gc_Cust.MultiSelectChk = true;
            this.gc_Cust.Name = "gc_Cust";
            this.gc_Cust.PopMenuChk = false;
            this.gc_Cust.Size = new System.Drawing.Size(1100, 808);
            this.gc_Cust.TabIndex = 2;
            this.gc_Cust.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Cust});
            this.gc_Cust.DoubleClick += new System.EventHandler(this.gc_Cust_DoubleClick);
            // 
            // gv_Cust
            // 
            this.gv_Cust.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gv_Cust.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_Cust.GridControl = this.gc_Cust;
            this.gv_Cust.Name = "gv_Cust";
            this.gv_Cust.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_Cust.OptionsFind.AlwaysVisible = true;
            this.gv_Cust.OptionsFind.FindNullPrompt = "입력해주세요...";
            this.gv_Cust.OptionsSelection.MultiSelect = true;
            this.gv_Cust.OptionsView.ShowAutoFilterRow = true;
            this.gv_Cust.OptionsView.ShowGroupPanel = false;
            // 
            // regCustome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_Cust);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "regCustome";
            this.Size = new System.Drawing.Size(1100, 850);
            this.Load += new System.EventHandler(this.regCustome_Load);
            this.Controls.SetChildIndex(this.gc_Cust, 0);
            this.Controls.SetChildIndex(this.panReg, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).EndInit();
            this.panReg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Cust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Cust)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private SERP.GridControlEx gc_Cust;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Cust;
    }
}
