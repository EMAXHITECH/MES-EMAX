namespace EMAX_Monitoring
{
    partial class PopHelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopHelpForm));
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label_Name = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Select = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.gc_Help = new EMAX_Monitoring.GridControlEx();
            this.gv_Help = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Help)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Help)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Search.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Search.Appearance.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Search.Appearance.Options.UseBackColor = true;
            this.btn_Search.Appearance.Options.UseBorderColor = true;
            this.btn_Search.Appearance.Options.UseForeColor = true;
            this.btn_Search.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Search.ImageOptions.SvgImage")));
            this.btn_Search.Location = new System.Drawing.Point(9, 4);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(74, 34);
            this.btn_Search.TabIndex = 8;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.label_Name);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(851, 51);
            this.panelControl1.TabIndex = 0;
            // 
            // label_Name
            // 
            this.label_Name.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Name.Appearance.Options.UseFont = true;
            this.label_Name.Location = new System.Drawing.Point(23, 13);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(36, 23);
            this.label_Name.TabIndex = 10;
            this.label_Name.Text = "조회";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.btn_Select);
            this.panelControl2.Controls.Add(this.btn_Close);
            this.panelControl2.Controls.Add(this.btn_Search);
            this.panelControl2.Location = new System.Drawing.Point(590, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(254, 43);
            this.panelControl2.TabIndex = 9;
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
            this.btn_Select.Location = new System.Drawing.Point(90, 4);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(74, 34);
            this.btn_Select.TabIndex = 12;
            this.btn_Select.Text = "선택";
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
            this.btn_Close.ImageOptions.SvgImage = global::EMAX_Monitoring.Properties.Resources.actions_deletecircled;
            this.btn_Close.Location = new System.Drawing.Point(171, 4);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(74, 34);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // gc_Help
            // 
            this.gc_Help.AddRowYN = false;
            this.gc_Help.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gc_Help.CellFocus = false;
            this.gc_Help.EnterYN = false;
            this.gc_Help.Execl_GB = EMAX_Monitoring.GridControlEx.Excel_GB.Update;
            this.gc_Help.ExpansionCHK = false;
            this.gc_Help.Location = new System.Drawing.Point(3, 56);
            this.gc_Help.MainView = this.gv_Help;
            this.gc_Help.MouseWheelChk = false;
            this.gc_Help.MultiSelectChk = false;
            this.gc_Help.Name = "gc_Help";
            this.gc_Help.PopMenuChk = false;
            this.gc_Help.Size = new System.Drawing.Size(841, 562);
            this.gc_Help.TabIndex = 0;
            this.gc_Help.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Help});
            // 
            // gv_Help
            // 
            this.gv_Help.GridControl = this.gc_Help;
            this.gv_Help.Name = "gv_Help";
            this.gv_Help.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_Help.OptionsFind.AlwaysVisible = true;
            this.gv_Help.OptionsFind.FindNullPrompt = "입력해주세요...";
            this.gv_Help.OptionsView.ShowAutoFilterRow = true;
            this.gv_Help.OptionsView.ShowGroupPanel = false;
            this.gv_Help.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gv_Help_KeyPress);
            this.gv_Help.DoubleClick += new System.EventHandler(this.gv_Help_DoubleClick);
            // 
            // PopHelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 620);
            this.Controls.Add(this.gc_Help);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PopHelpForm";
            this.Load += new System.EventHandler(this.PopHelpForm_Load);
            this.Shown += new System.EventHandler(this.PopHelpForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Help)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Help)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private GridControlEx gc_Help;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Help;
        private DevExpress.XtraEditors.SimpleButton btn_Select;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
        private DevExpress.XtraEditors.LabelControl label_Name;
    }
}
