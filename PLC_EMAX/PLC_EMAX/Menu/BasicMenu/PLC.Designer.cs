namespace PLC_EMAX
{
    partial class PLC
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Port = new DevExpress.XtraEditors.TextEdit();
            this.btn_Write = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Read = new DevExpress.XtraEditors.SimpleButton();
            this.txt_PLC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Addr = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_IP = new DevExpress.XtraEditors.TextEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gc_Equip = new DevExpress.XtraGrid.GridControl();
            this.gv_Equip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gc_PLC = new DevExpress.XtraGrid.GridControl();
            this.gv_PLC = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Data = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.gv_OrderRece = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Port.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PLC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Addr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Equip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Equip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_PLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_OrderRece)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.txt_Port);
            this.panelControl1.Controls.Add(this.btn_Write);
            this.panelControl1.Controls.Add(this.btn_Read);
            this.panelControl1.Controls.Add(this.txt_PLC);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txt_Addr);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txt_IP);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1047, 47);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(190, 15);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 15);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "PORT : ";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(240, 12);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Size = new System.Drawing.Size(60, 22);
            this.txt_Port.TabIndex = 11;
            // 
            // btn_Write
            // 
            this.btn_Write.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Write.Appearance.Options.UseBorderColor = true;
            this.btn_Write.Location = new System.Drawing.Point(720, 11);
            this.btn_Write.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.Size = new System.Drawing.Size(74, 23);
            this.btn_Write.TabIndex = 10;
            this.btn_Write.Text = "쓰기";
            this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
            // 
            // btn_Read
            // 
            this.btn_Read.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Read.Appearance.Options.UseBorderColor = true;
            this.btn_Read.Location = new System.Drawing.Point(640, 11);
            this.btn_Read.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(74, 23);
            this.btn_Read.TabIndex = 9;
            this.btn_Read.Text = "읽기";
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // txt_PLC
            // 
            this.txt_PLC.Location = new System.Drawing.Point(532, 12);
            this.txt_PLC.Name = "txt_PLC";
            this.txt_PLC.Size = new System.Drawing.Size(93, 22);
            this.txt_PLC.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(465, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(61, 15);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "데이터 값 : ";
            // 
            // txt_Addr
            // 
            this.txt_Addr.Location = new System.Drawing.Point(351, 12);
            this.txt_Addr.Name = "txt_Addr";
            this.txt_Addr.Size = new System.Drawing.Size(93, 22);
            this.txt_Addr.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(311, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 15);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "주소 : ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(21, 15);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "IP : ";
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(40, 12);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(144, 22);
            this.txt_IP.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 47);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gc_Equip);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gc_PLC);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1047, 522);
            this.splitContainerControl1.SplitterPosition = 474;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // gc_Equip
            // 
            this.gc_Equip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Equip.Location = new System.Drawing.Point(0, 32);
            this.gc_Equip.MainView = this.gv_Equip;
            this.gc_Equip.Name = "gc_Equip";
            this.gc_Equip.Size = new System.Drawing.Size(474, 490);
            this.gc_Equip.TabIndex = 1;
            this.gc_Equip.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Equip});
            // 
            // gv_Equip
            // 
            this.gv_Equip.GridControl = this.gc_Equip;
            this.gv_Equip.Name = "gv_Equip";
            this.gv_Equip.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
            this.gv_Equip.OptionsCustomization.AllowFilter = false;
            this.gv_Equip.OptionsCustomization.AllowSort = false;
            this.gv_Equip.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv_Equip.OptionsView.ShowGroupPanel = false;
            this.gv_Equip.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gv_Equip_FocusedRowChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btn_Search);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(474, 32);
            this.panelControl2.TabIndex = 0;
            // 
            // btn_Search
            // 
            this.btn_Search.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Search.Appearance.Options.UseBorderColor = true;
            this.btn_Search.Location = new System.Drawing.Point(388, 5);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(74, 23);
            this.btn_Search.TabIndex = 11;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 9);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(63, 15);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "설비 리스트";
            // 
            // gc_PLC
            // 
            this.gc_PLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_PLC.Location = new System.Drawing.Point(0, 32);
            this.gc_PLC.MainView = this.gv_PLC;
            this.gc_PLC.Name = "gc_PLC";
            this.gc_PLC.Size = new System.Drawing.Size(563, 490);
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
            this.panelControl3.Controls.Add(this.btn_Data);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(563, 32);
            this.panelControl3.TabIndex = 1;
            // 
            // btn_Data
            // 
            this.btn_Data.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Data.Appearance.Options.UseBorderColor = true;
            this.btn_Data.Location = new System.Drawing.Point(484, 4);
            this.btn_Data.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Data.Name = "btn_Data";
            this.btn_Data.Size = new System.Drawing.Size(74, 23);
            this.btn_Data.TabIndex = 13;
            this.btn_Data.Text = "누적데이터";
            this.btn_Data.Click += new System.EventHandler(this.btn_Data_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 15);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "PLC 데이터";
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
            // PLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 569);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "PLC";
            this.Text = "PLC 통신";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PLC_FormClosed);
            this.Load += new System.EventHandler(this.PLC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Port.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PLC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Addr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Equip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Equip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_PLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_PLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_OrderRece)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txt_PLC;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_Addr;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_IP;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.GridControl gc_Equip;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Equip;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_OrderRece;
        private DevExpress.XtraGrid.GridControl gc_PLC;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_PLC;
        private DevExpress.XtraEditors.SimpleButton btn_Read;
        private DevExpress.XtraEditors.SimpleButton btn_Write;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txt_Port;
        private DevExpress.XtraEditors.SimpleButton btn_Data;
    }
}