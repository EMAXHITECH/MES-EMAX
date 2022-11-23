namespace EMAX_Monitoring
{
    partial class BaseForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.panelControlEx1 = new EMAX_Monitoring.PanelControlEx(this.components);
            this.btn_File = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlEx1)).BeginInit();
            this.panelControlEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlEx1
            // 
            this.panelControlEx1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlEx1.Controls.Add(this.btn_File);
            this.panelControlEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlEx1.Location = new System.Drawing.Point(0, 420);
            this.panelControlEx1.Name = "panelControlEx1";
            this.panelControlEx1.Size = new System.Drawing.Size(550, 30);
            this.panelControlEx1.TabIndex = 90;
            // 
            // btn_File
            // 
            this.btn_File.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_File.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_File.ImageOptions.SvgImage")));
            this.btn_File.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btn_File.Location = new System.Drawing.Point(2, 3);
            this.btn_File.Name = "btn_File";
            this.btn_File.Size = new System.Drawing.Size(123, 24);
            this.btn_File.TabIndex = 90;
            this.btn_File.Text = "파일 Up/Down";
            this.btn_File.Click += new System.EventHandler(this.btn_File_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 450);
            this.Controls.Add(this.panelControlEx1);
            this.KeyPreview = true;
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BaseForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlEx1)).EndInit();
            this.panelControlEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton btn_File;
        public PanelControlEx panelControlEx1;
    }
}