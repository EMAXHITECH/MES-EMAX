namespace KIOSK_EMAX
{
    partial class regKioProcess
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(regKioProcess));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gc_Process = new KIOSK_EMAX.GridControlEx();
            this.gv_Process = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dt_Result = new DevExpress.XtraEditors.DateEdit();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Process)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Process)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Result.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Result.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gc_Process);
            this.groupControl2.Controls.Add(this.panelControl2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(1280, 850);
            this.groupControl2.TabIndex = 8;
            this.groupControl2.Text = "groupControl2";
            // 
            // gc_Process
            // 
            this.gc_Process.AddRowYN = false;
            this.gc_Process.CellFocus = true;
            this.gc_Process.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Process.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.gc_Process.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            this.gc_Process.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gc_Process.EnterYN = true;
            this.gc_Process.Execl_GB = KIOSK_EMAX.GridControlEx.Excel_GB.Update;
            this.gc_Process.ExpansionCHK = false;
            this.gc_Process.Head_DoubleChk = true;
            this.gc_Process.Hide_Point = false;
            this.gc_Process.Location = new System.Drawing.Point(2, 62);
            this.gc_Process.MainView = this.gv_Process;
            this.gc_Process.Margin = new System.Windows.Forms.Padding(5);
            this.gc_Process.MouseWheelChk = true;
            this.gc_Process.MultiSelectChk = true;
            this.gc_Process.Name = "gc_Process";
            this.gc_Process.PopMenuChk = true;
            this.gc_Process.Size = new System.Drawing.Size(1276, 786);
            this.gc_Process.TabIndex = 2;
            this.gc_Process.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Process});
            // 
            // gv_Process
            // 
            this.gv_Process.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.gv_Process.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_Process.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.gv_Process.Appearance.HeaderPanel.Options.UseFont = true;
            this.gv_Process.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gv_Process.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gv_Process.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.gv_Process.Appearance.Row.Options.UseFont = true;
            this.gv_Process.DetailHeight = 625;
            this.gv_Process.GridControl = this.gc_Process;
            this.gv_Process.Name = "gv_Process";
            this.gv_Process.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.gv_Process.OptionsCustomization.AllowSort = false;
            this.gv_Process.OptionsSelection.MultiSelect = true;
            this.gv_Process.OptionsView.ShowGroupPanel = false;
            this.gv_Process.RowHeight = 60;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Orange;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.dt_Result);
            this.panelControl2.Controls.Add(this.btn_Search);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1276, 60);
            this.panelControl2.TabIndex = 1;
            // 
            // dt_Result
            // 
            this.dt_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_Result.EditValue = null;
            this.dt_Result.Location = new System.Drawing.Point(131, 5);
            this.dt_Result.Margin = new System.Windows.Forms.Padding(5);
            this.dt_Result.Name = "dt_Result";
            this.dt_Result.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(247)))), ((int)(((byte)(182)))));
            this.dt_Result.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.dt_Result.Properties.Appearance.Options.UseBackColor = true;
            this.dt_Result.Properties.Appearance.Options.UseFont = true;
            this.dt_Result.Properties.AutoHeight = false;
            this.dt_Result.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", 35, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.dt_Result.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dt_Result.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_Result.Size = new System.Drawing.Size(250, 50);
            this.dt_Result.TabIndex = 12;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Search.Appearance.Options.UseFont = true;
            this.btn_Search.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_Search.ImageOptions.SvgImage")));
            this.btn_Search.Location = new System.Drawing.Point(1121, 5);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(150, 50);
            this.btn_Search.TabIndex = 11;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(27, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 32);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "작업일자";
            // 
            // regKioProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 850);
            this.Controls.Add(this.groupControl2);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("regKioProcess.IconOptions.Image")));
            this.Name = "regKioProcess";
            this.Text = "regKioProcess";
            this.Load += new System.EventHandler(this.regKioProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Process)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Process)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Result.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Result.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private KIOSK_EMAX.GridControlEx gc_Process;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Process;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private DevExpress.XtraEditors.DateEdit dt_Result;
    }
}