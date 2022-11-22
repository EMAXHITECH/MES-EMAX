namespace PLC_EMAX
{
    partial class PLC_Data
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
            this.gc_PLC = new DevExpress.XtraGrid.GridControl();
            this.gv_PLC = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Time = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Top = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dt_T = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dt_F = new DevExpress.XtraEditors.DateEdit();
            this.label_Name = new DevExpress.XtraEditors.LabelControl();
            this.gv_OrderRece = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gc_PLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Top.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_T.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_T.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_F.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_F.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_OrderRece)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_PLC
            // 
            this.gc_PLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_PLC.Location = new System.Drawing.Point(0, 43);
            this.gc_PLC.MainView = this.gv_PLC;
            this.gc_PLC.Name = "gc_PLC";
            this.gc_PLC.Size = new System.Drawing.Size(1047, 526);
            this.gc_PLC.TabIndex = 2;
            this.gc_PLC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_PLC});
            // 
            // gv_PLC
            // 
            this.gv_PLC.GridControl = this.gc_PLC;
            this.gv_PLC.Name = "gv_PLC";
            this.gv_PLC.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
            this.gv_PLC.OptionsCustomization.AllowFilter = false;
            this.gv_PLC.OptionsCustomization.AllowSort = false;
            this.gv_PLC.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv_PLC.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.txt_Time);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.txt_Top);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.dt_T);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.dt_F);
            this.panelControl3.Controls.Add(this.label_Name);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1047, 43);
            this.panelControl3.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(820, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(17, 22);
            this.labelControl4.TabIndex = 21;
            this.labelControl4.Text = "초";
            // 
            // txt_Time
            // 
            this.txt_Time.Location = new System.Drawing.Point(721, 7);
            this.txt_Time.Name = "txt_Time";
            this.txt_Time.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.txt_Time.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F);
            this.txt_Time.Properties.Appearance.Options.UseFont = true;
            this.txt_Time.Properties.BeepOnError = false;
            this.txt_Time.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txt_Time.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txt_Time.Properties.MaskSettings.Set("mask", "N2");
            this.txt_Time.Properties.UseMaskAsDisplayFormat = true;
            this.txt_Time.Size = new System.Drawing.Size(93, 28);
            this.txt_Time.TabIndex = 20;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(643, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 22);
            this.labelControl3.TabIndex = 19;
            this.labelControl3.Text = "조회 시간";
            // 
            // txt_Top
            // 
            this.txt_Top.Location = new System.Drawing.Point(526, 9);
            this.txt_Top.Name = "txt_Top";
            this.txt_Top.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.txt_Top.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F);
            this.txt_Top.Properties.Appearance.Options.UseFont = true;
            this.txt_Top.Properties.BeepOnError = false;
            this.txt_Top.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txt_Top.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txt_Top.Properties.MaskSettings.Set("mask", "N0");
            this.txt_Top.Properties.UseMaskAsDisplayFormat = true;
            this.txt_Top.Size = new System.Drawing.Size(93, 28);
            this.txt_Top.TabIndex = 18;
            this.txt_Top.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Top_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(427, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 22);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "최대 조회 수";
            // 
            // dt_T
            // 
            this.dt_T.EditValue = null;
            this.dt_T.Location = new System.Drawing.Point(264, 10);
            this.dt_T.Name = "dt_T";
            this.dt_T.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dt_T.Properties.Appearance.Options.UseFont = true;
            this.dt_T.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_T.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_T.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.dt_T.Properties.UseMaskAsDisplayFormat = true;
            this.dt_T.Size = new System.Drawing.Size(140, 28);
            this.dt_T.TabIndex = 16;
            this.dt_T.EditValueChanged += new System.EventHandler(this.dt_T_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(245, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(13, 22);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "~";
            // 
            // dt_F
            // 
            this.dt_F.EditValue = null;
            this.dt_F.Location = new System.Drawing.Point(99, 9);
            this.dt_F.Name = "dt_F";
            this.dt_F.Properties.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dt_F.Properties.Appearance.Options.UseFont = true;
            this.dt_F.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_F.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_F.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.dt_F.Properties.UseMaskAsDisplayFormat = true;
            this.dt_F.Size = new System.Drawing.Size(140, 28);
            this.dt_F.TabIndex = 14;
            this.dt_F.EditValueChanged += new System.EventHandler(this.dt_F_EditValueChanged);
            // 
            // label_Name
            // 
            this.label_Name.Appearance.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Name.Appearance.Options.UseFont = true;
            this.label_Name.Location = new System.Drawing.Point(12, 12);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(72, 22);
            this.label_Name.TabIndex = 6;
            this.label_Name.Text = "조회 일자";
            // 
            // gv_OrderRece
            // 
            this.gv_OrderRece.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.gv_OrderRece.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gv_OrderRece.Name = "gv_OrderRece";
            this.gv_OrderRece.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
            this.gv_OrderRece.OptionsCustomization.AllowFilter = false;
            this.gv_OrderRece.OptionsCustomization.AllowSort = false;
            this.gv_OrderRece.OptionsSelection.MultiSelect = true;
            this.gv_OrderRece.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv_OrderRece.OptionsView.ShowGroupPanel = false;
            // 
            // PLC_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 569);
            this.Controls.Add(this.gc_PLC);
            this.Controls.Add(this.panelControl3);
            this.Name = "PLC_Data";
            this.Text = "PLC 누적 데이터";
            this.Load += new System.EventHandler(this.PLC_Data_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gc_PLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Top.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_T.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_T.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_F.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_F.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_OrderRece)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl label_Name;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_OrderRece;
        private DevExpress.XtraGrid.GridControl gc_PLC;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_PLC;
        private DevExpress.XtraEditors.DateEdit dt_F;
        private DevExpress.XtraEditors.DateEdit dt_T;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_Top;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_Time;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}