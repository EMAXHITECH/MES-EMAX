namespace MES
{
    partial class regDept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(regDept));
            this.GridDept = new MES.GridControlEx();
            this.ViewDept = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).BeginInit();
            this.panReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).BeginInit();
            this.panButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDept)).BeginInit();
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
            this.panReg.Size = new System.Drawing.Size(1100, 40);
            // 
            // panButton
            // 
            this.panButton.Location = new System.Drawing.Point(545, 2);
            // 
            // GridDept
            // 
            this.GridDept.AddRowYN = false;
            this.GridDept.CellFocus = true;
            this.GridDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridDept.EnterYN = true;
            this.GridDept.Execl_GB = MES.GridControlEx.Excel_GB.Update;
            this.GridDept.ExpansionCHK = false;
            this.GridDept.Location = new System.Drawing.Point(0, 0);
            this.GridDept.MainView = this.ViewDept;
            this.GridDept.MouseWheelChk = false;
            this.GridDept.MultiSelectChk = true;
            this.GridDept.Name = "GridDept";
            this.GridDept.PopMenuChk = false;
            this.GridDept.Size = new System.Drawing.Size(1100, 850);
            this.GridDept.TabIndex = 2;
            this.GridDept.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewDept});
            // 
            // ViewDept
            // 
            this.ViewDept.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.ViewDept.Appearance.FocusedRow.Options.UseBackColor = true;
            this.ViewDept.GridControl = this.GridDept;
            this.ViewDept.Name = "ViewDept";
            this.ViewDept.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.ViewDept.OptionsFind.AlwaysVisible = true;
            this.ViewDept.OptionsFind.FindNullPrompt = "입력해주세요...";
            this.ViewDept.OptionsSelection.MultiSelect = true;
            this.ViewDept.OptionsView.ShowGroupPanel = false;
            this.ViewDept.DoubleClick += new System.EventHandler(this.ViewDept_DoubleClick);
            // 
            // regDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridDept);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "regDept";
            this.Size = new System.Drawing.Size(1100, 850);
            this.Load += new System.EventHandler(this.regDept_Load);
            this.Controls.SetChildIndex(this.GridDept, 0);
            this.Controls.SetChildIndex(this.panReg, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panReg)).EndInit();
            this.panReg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panButton)).EndInit();
            this.panButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDept)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private GridControlEx GridDept;
        private DevExpress.XtraGrid.Views.Grid.GridView ViewDept;
    }
}
