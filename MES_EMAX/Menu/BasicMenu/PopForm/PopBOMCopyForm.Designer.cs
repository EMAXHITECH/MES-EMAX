namespace MES
{
    partial class PopBOMCopyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopBOMCopyForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txt_ItemName = new DevExpress.XtraEditors.TextEdit();
            this.txt_ItemCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Save = new MES.SimpleButtonEx();
            this.btn_Close = new MES.SimpleButtonEx();
            this.Grid_Items = new MES.GridControlEx();
            this.View_Items = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Items)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Items)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txt_ItemName);
            this.panelControl1.Controls.Add(this.txt_ItemCode);
            this.panelControl1.Controls.Add(this.labelControl19);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(950, 51);
            this.panelControl1.TabIndex = 0;
            // 
            // txt_ItemName
            // 
            this.txt_ItemName.Enabled = false;
            this.txt_ItemName.Location = new System.Drawing.Point(287, 13);
            this.txt_ItemName.Name = "txt_ItemName";
            this.txt_ItemName.Properties.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.txt_ItemName.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11F);
            this.txt_ItemName.Properties.Appearance.Options.UseBackColor = true;
            this.txt_ItemName.Properties.Appearance.Options.UseFont = true;
            this.txt_ItemName.Size = new System.Drawing.Size(298, 24);
            this.txt_ItemName.TabIndex = 52;
            // 
            // txt_ItemCode
            // 
            this.txt_ItemCode.Enabled = false;
            this.txt_ItemCode.Location = new System.Drawing.Point(83, 13);
            this.txt_ItemCode.Name = "txt_ItemCode";
            this.txt_ItemCode.Properties.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.txt_ItemCode.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 11F);
            this.txt_ItemCode.Properties.Appearance.Options.UseBackColor = true;
            this.txt_ItemCode.Properties.Appearance.Options.UseFont = true;
            this.txt_ItemCode.Size = new System.Drawing.Size(198, 24);
            this.txt_ItemCode.TabIndex = 51;
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
            this.labelControl19.Text = "원본 품목";
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
            // Grid_Items
            // 
            this.Grid_Items.AddRowYN = false;
            this.Grid_Items.CellFocus = true;
            this.Grid_Items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Items.EnterYN = true;
            this.Grid_Items.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.Grid_Items.ExpansionCHK = false;
            this.Grid_Items.Head_DoubleChk = true;
            this.Grid_Items.Location = new System.Drawing.Point(0, 51);
            this.Grid_Items.MainView = this.View_Items;
            this.Grid_Items.MouseWheelChk = true;
            this.Grid_Items.MultiSelectChk = true;
            this.Grid_Items.Name = "Grid_Items";
            this.Grid_Items.PopMenuChk = true;
            this.Grid_Items.Size = new System.Drawing.Size(950, 539);
            this.Grid_Items.TabIndex = 1;
            this.Grid_Items.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View_Items});
            // 
            // View_Items
            // 
            this.View_Items.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.View_Items.Appearance.FocusedRow.Options.UseBackColor = true;
            this.View_Items.GridControl = this.Grid_Items;
            this.View_Items.Name = "View_Items";
            this.View_Items.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.View_Items.OptionsSelection.MultiSelect = true;
            this.View_Items.OptionsView.ShowGroupPanel = false;
            // 
            // PopBOMCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 620);
            this.Controls.Add(this.Grid_Items);
            this.Controls.Add(this.panelControl1);
            this.Name = "PopBOMCopyForm";
            this.Text = "BOM 복사";
            this.Load += new System.EventHandler(this.PopBOMCopyForm_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.Grid_Items, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Items)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Items)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private SimpleButtonEx btn_Save;
        private SimpleButtonEx btn_Close;
        private GridControlEx Grid_Items;
        private DevExpress.XtraGrid.Views.Grid.GridView View_Items;
        private DevExpress.XtraEditors.TextEdit txt_ItemName;
        private DevExpress.XtraEditors.TextEdit txt_ItemCode;
        private DevExpress.XtraEditors.LabelControl labelControl19;
    }
}