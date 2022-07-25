namespace MES
{
    partial class regBOM
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(regBOM));
            this.gc_BOM = new MES.GridControlEx();
            this.gv_BOM = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dpiAwareImageCollection1 = new DevExpress.Utils.DPIAwareImageCollection(this.components);
            this.btn_Copy = new MES.SimpleButtonEx();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gc_Item = new MES.GridControlEx();
            this.gv_Item = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).BeginInit();
            this.panReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_BOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpiAwareImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(85)))), ((int)(((byte)(117)))));
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseBorderColor = true;
            this.btn_Save.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Save.ImageOptions.SvgImage")));
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
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
            this.panReg.Controls.Add(this.labelControl6);
            this.panReg.Size = new System.Drawing.Size(1100, 40);
            this.panReg.Controls.SetChildIndex(this.panButton, 0);
            this.panReg.Controls.SetChildIndex(this.labelControl6, 0);
            // 
            // panButton
            // 
            this.panButton.Location = new System.Drawing.Point(545, 2);
            // 
            // gc_BOM
            // 
            this.gc_BOM.AddRowYN = false;
            this.gc_BOM.CellFocus = true;
            this.gc_BOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_BOM.EnterYN = true;
            this.gc_BOM.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.gc_BOM.ExpansionCHK = false;
            this.gc_BOM.Location = new System.Drawing.Point(2, 29);
            this.gc_BOM.MainView = this.gv_BOM;
            this.gc_BOM.MouseWheelChk = true;
            this.gc_BOM.MultiSelectChk = true;
            this.gc_BOM.Name = "gc_BOM";
            this.gc_BOM.PopMenuChk = true;
            this.gc_BOM.Size = new System.Drawing.Size(159, 779);
            this.gc_BOM.TabIndex = 3;
            this.gc_BOM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_BOM});
            this.gc_BOM.NewRowAdd += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs>(this.gc_BOM_NewRowAdd);
            this.gc_BOM.EditorKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gc_BOM_EditorKeyPress);
            // 
            // gv_BOM
            // 
            this.gv_BOM.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gv_BOM.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_BOM.GridControl = this.gc_BOM;
            this.gv_BOM.Name = "gv_BOM";
            this.gv_BOM.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_BOM.OptionsCustomization.AllowFilter = false;
            this.gv_BOM.OptionsCustomization.AllowSort = false;
            this.gv_BOM.OptionsSelection.MultiSelect = true;
            this.gv_BOM.OptionsView.ShowAutoFilterRow = true;
            this.gv_BOM.OptionsView.ShowGroupPanel = false;
            this.gv_BOM.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gv_BOM_CellValueChanged);
            // 
            // dpiAwareImageCollection1
            // 
            this.dpiAwareImageCollection1.Stream = ((DevExpress.Utils.DPIAwareImageCollectionStreamer)(resources.GetObject("dpiAwareImageCollection1.Stream")));
            // 
            // btn_Copy
            // 
            this.btn_Copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Copy.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Copy.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Copy.Appearance.Options.UseBackColor = true;
            this.btn_Copy.Appearance.Options.UseBorderColor = true;
            this.btn_Copy.button_GB = MES.SimpleButtonEx.Button_GB.Copy;
            this.btn_Copy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Copy.ImageOptions.SvgImage")));
            this.btn_Copy.Location = new System.Drawing.Point(842, 103);
            this.btn_Copy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Result_Update = System.Windows.Forms.DialogResult.None;
            this.btn_Copy.sCHK = "N";
            this.btn_Copy.Size = new System.Drawing.Size(74, 34);
            this.btn_Copy.sSearch = "Y";
            this.btn_Copy.sUpdate = "N";
            this.btn_Copy.TabIndex = 52;
            this.btn_Copy.TabStop = false;
            this.btn_Copy.Text = "복사";
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(411, 11);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(128, 17);
            this.labelControl6.TabIndex = 53;
            this.labelControl6.Text = "* 복사 단축키 : F10";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 40);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gc_Item);
            this.splitContainerControl1.Panel1.Controls.Add(this.btn_Copy);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1100, 810);
            this.splitContainerControl1.SplitterPosition = 927;
            this.splitContainerControl1.TabIndex = 8;
            // 
            // gc_Item
            // 
            this.gc_Item.AddRowYN = false;
            this.gc_Item.CellFocus = true;
            this.gc_Item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Item.EnterYN = true;
            this.gc_Item.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.gc_Item.ExpansionCHK = false;
            this.gc_Item.Location = new System.Drawing.Point(0, 0);
            this.gc_Item.MainView = this.gv_Item;
            this.gc_Item.MouseWheelChk = true;
            this.gc_Item.MultiSelectChk = true;
            this.gc_Item.Name = "gc_Item";
            this.gc_Item.PopMenuChk = true;
            this.gc_Item.Size = new System.Drawing.Size(927, 810);
            this.gc_Item.TabIndex = 4;
            this.gc_Item.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Item});
            // 
            // gv_Item
            // 
            this.gv_Item.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gv_Item.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_Item.GridControl = this.gc_Item;
            this.gv_Item.Name = "gv_Item";
            this.gv_Item.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_Item.OptionsSelection.MultiSelect = true;
            this.gv_Item.OptionsView.ShowAutoFilterRow = true;
            this.gv_Item.OptionsView.ShowGroupPanel = false;
            this.gv_Item.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gv_Item_FocusedRowChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gc_BOM);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(163, 810);
            this.panelControl1.TabIndex = 8;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl19);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(159, 27);
            this.panelControl2.TabIndex = 0;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl19.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Appearance.Options.UseForeColor = true;
            this.labelControl19.Location = new System.Drawing.Point(5, 5);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(62, 17);
            this.labelControl19.TabIndex = 17;
            this.labelControl19.Text = "BOM 등록";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "product.png");
            this.imageCollection1.Images.SetKeyName(1, "semi-product.png");
            this.imageCollection1.Images.SetKeyName(2, "materials.png");
            // 
            // regBOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "regBOM";
            this.Size = new System.Drawing.Size(1100, 850);
            this.Load += new System.EventHandler(this.regBOM_Load);
            this.Controls.SetChildIndex(this.panReg, 0);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).EndInit();
            this.panReg.ResumeLayout(false);
            this.panReg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_BOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpiAwareImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private GridControlEx gc_BOM;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_BOM;
        private DevExpress.Utils.DPIAwareImageCollection dpiAwareImageCollection1;
        private SimpleButtonEx btn_Copy;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private GridControlEx gc_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Item;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
