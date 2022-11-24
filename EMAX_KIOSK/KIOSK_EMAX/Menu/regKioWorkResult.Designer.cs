namespace KIOSK_EMAX
{
    partial class regKioWorkResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(regKioWorkResult));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gc_Sheet = new KIOSK_EMAX.GridControlEx();
            this.gv_Sheet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_User = new KIOSK_EMAX.GridControlEx();
            this.gv_User = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Add = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Del = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUp_Custom = new DevExpress.XtraEditors.LookUpEdit();
            this.btn_Start = new DevExpress.XtraEditors.SimpleButton();
            this.btn_End = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Sheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Sheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUp_Custom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.splitContainerControl1);
            this.groupControl2.Controls.Add(this.panelControl2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(1280, 850);
            this.groupControl2.TabIndex = 8;
            this.groupControl2.Text = "groupControl2";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 62);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gc_Sheet);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gc_User);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1276, 786);
            this.splitContainerControl1.SplitterPosition = 420;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // gc_Sheet
            // 
            this.gc_Sheet.AddRowYN = false;
            this.gc_Sheet.CellFocus = true;
            this.gc_Sheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Sheet.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.gc_Sheet.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            this.gc_Sheet.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gc_Sheet.EnterYN = true;
            this.gc_Sheet.Execl_GB = KIOSK_EMAX.GridControlEx.Excel_GB.Update;
            this.gc_Sheet.ExpansionCHK = false;
            this.gc_Sheet.Head_DoubleChk = true;
            this.gc_Sheet.Hide_Point = false;
            this.gc_Sheet.Location = new System.Drawing.Point(0, 0);
            this.gc_Sheet.MainView = this.gv_Sheet;
            this.gc_Sheet.Margin = new System.Windows.Forms.Padding(5);
            this.gc_Sheet.MouseWheelChk = true;
            this.gc_Sheet.MultiSelectChk = true;
            this.gc_Sheet.Name = "gc_Sheet";
            this.gc_Sheet.PopMenuChk = true;
            this.gc_Sheet.Size = new System.Drawing.Size(1276, 420);
            this.gc_Sheet.TabIndex = 2;
            this.gc_Sheet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Sheet});
            this.gc_Sheet.Load += new System.EventHandler(this.gc_Sheet_Load);
            // 
            // gv_Sheet
            // 
            this.gv_Sheet.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.gv_Sheet.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_Sheet.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.gv_Sheet.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_Sheet.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Sheet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Sheet.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.gv_Sheet.Appearance.Row.Options.UseFont = true;
            this.gv_Sheet.DetailHeight = 625;
            this.gv_Sheet.GridControl = this.gc_Sheet;
            this.gv_Sheet.Name = "gv_Sheet";
            this.gv_Sheet.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gv_Sheet.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_Sheet.OptionsCustomization.AllowSort = false;
            this.gv_Sheet.OptionsSelection.MultiSelect = true;
            this.gv_Sheet.OptionsView.ShowGroupPanel = false;
            this.gv_Sheet.RowHeight = 60;
            this.gv_Sheet.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gv_Sheet_RowStyle);
            this.gv_Sheet.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gv_Sheet_FocusedRowChanged);
            // 
            // gc_User
            // 
            this.gc_User.AddRowYN = false;
            this.gc_User.CellFocus = true;
            this.gc_User.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_User.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.gc_User.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            this.gc_User.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gc_User.EnterYN = true;
            this.gc_User.Execl_GB = KIOSK_EMAX.GridControlEx.Excel_GB.Update;
            this.gc_User.ExpansionCHK = false;
            this.gc_User.Head_DoubleChk = true;
            this.gc_User.Hide_Point = false;
            this.gc_User.Location = new System.Drawing.Point(0, 60);
            this.gc_User.MainView = this.gv_User;
            this.gc_User.Margin = new System.Windows.Forms.Padding(5);
            this.gc_User.MouseWheelChk = true;
            this.gc_User.MultiSelectChk = true;
            this.gc_User.Name = "gc_User";
            this.gc_User.PopMenuChk = true;
            this.gc_User.Size = new System.Drawing.Size(1276, 296);
            this.gc_User.TabIndex = 3;
            this.gc_User.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_User});
            // 
            // gv_User
            // 
            this.gv_User.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.gv_User.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_User.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.gv_User.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_User.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_User.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_User.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.gv_User.Appearance.Row.Options.UseFont = true;
            this.gv_User.DetailHeight = 625;
            this.gv_User.GridControl = this.gc_User;
            this.gv_User.Name = "gv_User";
            this.gv_User.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_User.OptionsCustomization.AllowSort = false;
            this.gv_User.OptionsSelection.MultiSelect = true;
            this.gv_User.OptionsView.ShowGroupPanel = false;
            this.gv_User.RowHeight = 60;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Orange;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.btn_Add);
            this.panelControl1.Controls.Add(this.btn_Del);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1276, 60);
            this.panelControl1.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(14, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 32);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "작업자";
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Add.Appearance.Options.UseFont = true;
            this.btn_Add.ImageOptions.Image = global::KIOSK_EMAX.Properties.Resources.add_32x32;
            this.btn_Add.Location = new System.Drawing.Point(1072, 5);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(95, 50);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "추가";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Del.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Del.Appearance.Options.UseFont = true;
            this.btn_Del.ImageOptions.Image = global::KIOSK_EMAX.Properties.Resources.remove_32x32;
            this.btn_Del.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Del.Location = new System.Drawing.Point(1174, 5);
            this.btn_Del.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(95, 50);
            this.btn_Del.TabIndex = 1;
            this.btn_Del.Text = "삭제";
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Orange;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btn_Search);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.lookUp_Custom);
            this.panelControl2.Controls.Add(this.btn_Start);
            this.panelControl2.Controls.Add(this.btn_End);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1276, 60);
            this.panelControl2.TabIndex = 1;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Search.Appearance.Options.UseFont = true;
            this.btn_Search.ImageOptions.SvgImage = global::KIOSK_EMAX.Properties.Resources.images;
            this.btn_Search.Location = new System.Drawing.Point(760, 5);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(150, 50);
            this.btn_Search.TabIndex = 11;
            this.btn_Search.Text = "도면보기";
            this.btn_Search.Visible = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(27, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 32);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "작업처";
            // 
            // lookUp_Custom
            // 
            this.lookUp_Custom.Location = new System.Drawing.Point(118, 7);
            this.lookUp_Custom.Name = "lookUp_Custom";
            this.lookUp_Custom.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.lookUp_Custom.Properties.Appearance.Options.UseFont = true;
            this.lookUp_Custom.Properties.AutoHeight = false;
            this.lookUp_Custom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUp_Custom.Properties.NullText = "";
            this.lookUp_Custom.Size = new System.Drawing.Size(162, 50);
            this.lookUp_Custom.TabIndex = 9;
            this.lookUp_Custom.EditValueChanged += new System.EventHandler(this.lookUp_Custom_EditValueChanged);
            // 
            // btn_Start
            // 
            this.btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Start.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Start.Appearance.Options.UseFont = true;
            this.btn_Start.ImageOptions.SvgImage = global::KIOSK_EMAX.Properties.Resources.gettingstarted;
            this.btn_Start.Location = new System.Drawing.Point(920, 5);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(150, 50);
            this.btn_Start.TabIndex = 8;
            this.btn_Start.Text = "작업시작";
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_End
            // 
            this.btn_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_End.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_End.Appearance.Options.UseFont = true;
            this.btn_End.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_End.ImageOptions.SvgImage = global::KIOSK_EMAX.Properties.Resources.actions_removecircled;
            this.btn_End.Location = new System.Drawing.Point(1080, 5);
            this.btn_End.Margin = new System.Windows.Forms.Padding(5);
            this.btn_End.Name = "btn_End";
            this.btn_End.Size = new System.Drawing.Size(150, 50);
            this.btn_End.TabIndex = 1;
            this.btn_End.Text = "작업종료";
            this.btn_End.Click += new System.EventHandler(this.btn_End_Click);
            // 
            // regKioWorkResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 850);
            this.Controls.Add(this.groupControl2);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("regKioWorkResult.IconOptions.Image")));
            this.Name = "regKioWorkResult";
            this.Text = "regKioWorkResult";
            this.Load += new System.EventHandler(this.regKioWorkResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Sheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Sheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUp_Custom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private KIOSK_EMAX.GridControlEx gc_Sheet;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Sheet;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Start;
        private DevExpress.XtraEditors.SimpleButton btn_End;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUp_Custom;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private GridControlEx gc_User;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_User;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Add;
        private DevExpress.XtraEditors.SimpleButton btn_Del;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
    }
}