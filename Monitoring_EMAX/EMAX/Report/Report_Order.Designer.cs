namespace EMAX_Monitoring
{
    partial class Report_Order
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.Label_Title = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.Label_Order_No = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Label_Order_No,
            this.Label_Title});
            this.TopMargin.HeightF = 107.2916F;
            this.TopMargin.Name = "TopMargin";
            // 
            // Label_Title
            // 
            this.Label_Title.Font = new System.Drawing.Font("맑은 고딕", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Title.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.Label_Title.Multiline = true;
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Label_Title.SizeF = new System.Drawing.SizeF(1169F, 72.29164F);
            this.Label_Title.StylePriority.UseFont = false;
            this.Label_Title.StylePriority.UseTextAlignment = false;
            this.Label_Title.Text = "Title";
            this.Label_Title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 3.125F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 1171.875F;
            this.Detail.Name = "Detail";
            // 
            // Label_Order_No
            // 
            this.Label_Order_No.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Order_No.LocationFloat = new DevExpress.Utils.PointFloat(34.5F, 72.29F);
            this.Label_Order_No.Multiline = true;
            this.Label_Order_No.Name = "Label_Order_No";
            this.Label_Order_No.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Label_Order_No.SizeF = new System.Drawing.SizeF(392.9584F, 35.00001F);
            this.Label_Order_No.StylePriority.UseFont = false;
            this.Label_Order_No.StylePriority.UseTextAlignment = false;
            this.Label_Order_No.Text = "Title";
            this.Label_Order_No.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // Report_Order
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 107, 3);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4Rotated;
            this.Version = "20.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel Label_Title;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel Label_Order_No;
    }
}
