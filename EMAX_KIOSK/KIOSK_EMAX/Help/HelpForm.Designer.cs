namespace KIOSK_EMAX
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Select = new DevExpress.XtraEditors.SimpleButton();
            this.label_Name = new DevExpress.XtraEditors.LabelControl();
            this.gc_Help = new DevExpress.XtraGrid.GridControl();
            this.gv_Help = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Help)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Help)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Close);
            this.panelControl1.Controls.Add(this.btn_Select);
            this.panelControl1.Controls.Add(this.label_Name);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(900, 70);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Appearance.Font = new System.Drawing.Font("돋움", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Close.Appearance.Options.UseFont = true;
            this.btn_Close.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Close.ImageOptions.SvgImage")));
            this.btn_Close.Location = new System.Drawing.Point(763, 7);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(130, 50);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Select.Appearance.Font = new System.Drawing.Font("돋움", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Select.Appearance.Options.UseFont = true;
            this.btn_Select.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Select.ImageOptions.SvgImage")));
            this.btn_Select.Location = new System.Drawing.Point(623, 7);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(130, 50);
            this.btn_Select.TabIndex = 2;
            this.btn_Select.Text = "선택";
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // label_Name
            // 
            this.label_Name.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Name.Appearance.Options.UseFont = true;
            this.label_Name.Location = new System.Drawing.Point(19, 17);
            this.label_Name.Margin = new System.Windows.Forms.Padding(5);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(144, 37);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "작업자 조회";
            // 
            // gc_Help
            // 
            this.gc_Help.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Help.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gc_Help.Location = new System.Drawing.Point(0, 70);
            this.gc_Help.MainView = this.gv_Help;
            this.gc_Help.Margin = new System.Windows.Forms.Padding(5);
            this.gc_Help.Name = "gc_Help";
            this.gc_Help.Size = new System.Drawing.Size(900, 600);
            this.gc_Help.TabIndex = 3;
            this.gc_Help.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Help});
            // 
            // gv_Help
            // 
            this.gv_Help.Appearance.HeaderPanel.Font = new System.Drawing.Font("돋움", 20F, System.Drawing.FontStyle.Bold);
            this.gv_Help.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_Help.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Help.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Help.Appearance.Row.Font = new System.Drawing.Font("돋움", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv_Help.Appearance.Row.Options.UseFont = true;
            this.gv_Help.DetailHeight = 625;
            this.gv_Help.GridControl = this.gc_Help;
            this.gv_Help.Name = "gv_Help";
            this.gv_Help.OptionsCustomization.AllowSort = false;
            this.gv_Help.OptionsFind.AlwaysVisible = true;
            this.gv_Help.OptionsView.ShowGroupPanel = false;
            this.gv_Help.RowHeight = 60;
            this.gv_Help.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gv_Help_RowCellClick);
            this.gv_Help.DoubleClick += new System.EventHandler(this.gv_Help_DoubleClick);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 670);
            this.Controls.Add(this.gc_Help);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Help)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Help)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl label_Name;
        private DevExpress.XtraEditors.SimpleButton btn_Select;
        private DevExpress.XtraGrid.GridControl gc_Help;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Help;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
    }
}