namespace KIOSK_EMAX
{
    partial class Report_DonBar
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.Lbl_Length = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbl_Thic = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbl_Cnt = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbl_Wht = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbl_CustName = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbl_Date = new DevExpress.XtraReports.UI.XRLabel();
            this.Lbl_Lot = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.BarCode = new DevExpress.XtraReports.UI.XRBarCode();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Visible = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0.6249745F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Visible = false;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Lbl_Length,
            this.Lbl_Thic,
            this.Lbl_Cnt,
            this.Lbl_Wht,
            this.Lbl_CustName,
            this.Lbl_Date,
            this.Lbl_Lot,
            this.xrPictureBox1,
            this.BarCode});
            this.Detail.HeightF = 466F;
            this.Detail.Name = "Detail";
            // 
            // Lbl_Length
            // 
            this.Lbl_Length.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Length.LocationFloat = new DevExpress.Utils.PointFloat(212.3749F, 210.0417F);
            this.Lbl_Length.Multiline = true;
            this.Lbl_Length.Name = "Lbl_Length";
            this.Lbl_Length.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_Length.SizeF = new System.Drawing.SizeF(70F, 40F);
            this.Lbl_Length.StylePriority.UseFont = false;
            this.Lbl_Length.StylePriority.UseTextAlignment = false;
            this.Lbl_Length.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Lbl_Thic
            // 
            this.Lbl_Thic.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Thic.LocationFloat = new DevExpress.Utils.PointFloat(134.7916F, 211.0417F);
            this.Lbl_Thic.Multiline = true;
            this.Lbl_Thic.Name = "Lbl_Thic";
            this.Lbl_Thic.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_Thic.SizeF = new System.Drawing.SizeF(60F, 40F);
            this.Lbl_Thic.StylePriority.UseFont = false;
            this.Lbl_Thic.StylePriority.UseTextAlignment = false;
            this.Lbl_Thic.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Lbl_Cnt
            // 
            this.Lbl_Cnt.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Cnt.LocationFloat = new DevExpress.Utils.PointFloat(232.3748F, 255.9167F);
            this.Lbl_Cnt.Multiline = true;
            this.Lbl_Cnt.Name = "Lbl_Cnt";
            this.Lbl_Cnt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_Cnt.SizeF = new System.Drawing.SizeF(61.46F, 45F);
            this.Lbl_Cnt.StylePriority.UseFont = false;
            this.Lbl_Cnt.StylePriority.UseTextAlignment = false;
            this.Lbl_Cnt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Lbl_Wht
            // 
            this.Lbl_Wht.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Wht.LocationFloat = new DevExpress.Utils.PointFloat(110.3744F, 255.9167F);
            this.Lbl_Wht.Multiline = true;
            this.Lbl_Wht.Name = "Lbl_Wht";
            this.Lbl_Wht.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_Wht.SizeF = new System.Drawing.SizeF(98.54F, 45F);
            this.Lbl_Wht.StylePriority.UseFont = false;
            this.Lbl_Wht.StylePriority.UseTextAlignment = false;
            this.Lbl_Wht.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Lbl_CustName
            // 
            this.Lbl_CustName.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_CustName.LocationFloat = new DevExpress.Utils.PointFloat(110.375F, 125.4167F);
            this.Lbl_CustName.Multiline = true;
            this.Lbl_CustName.Name = "Lbl_CustName";
            this.Lbl_CustName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_CustName.SizeF = new System.Drawing.SizeF(222F, 40F);
            this.Lbl_CustName.StylePriority.UseFont = false;
            this.Lbl_CustName.StylePriority.UseTextAlignment = false;
            this.Lbl_CustName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Lbl_Date
            // 
            this.Lbl_Date.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Lbl_Date.LocationFloat = new DevExpress.Utils.PointFloat(226.3749F, 77.41673F);
            this.Lbl_Date.Name = "Lbl_Date";
            this.Lbl_Date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_Date.SizeF = new System.Drawing.SizeF(106F, 45F);
            this.Lbl_Date.StylePriority.UseFont = false;
            this.Lbl_Date.StylePriority.UseTextAlignment = false;
            this.Lbl_Date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Lbl_Lot
            // 
            this.Lbl_Lot.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Lot.LocationFloat = new DevExpress.Utils.PointFloat(61.79156F, 77.41673F);
            this.Lbl_Lot.Name = "Lbl_Lot";
            this.Lbl_Lot.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Lbl_Lot.SizeF = new System.Drawing.SizeF(106F, 45F);
            this.Lbl_Lot.StylePriority.UseFont = false;
            this.Lbl_Lot.StylePriority.UseTextAlignment = false;
            this.Lbl_Lot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.TopLeft;
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(global::KIOSK_EMAX.Properties.Resources.busDonBar, true);
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(340F, 425.21F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // BarCode
            // 
            this.BarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.BarCode.LocationFloat = new DevExpress.Utils.PointFloat(4.004883F, 425.21F);
            this.BarCode.Name = "BarCode";
            this.BarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.BarCode.SizeF = new System.Drawing.SizeF(328.37F, 40.54F);
            this.BarCode.StylePriority.UseTextAlignment = false;
            code128Generator1.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetAuto;
            this.BarCode.Symbology = code128Generator1;
            this.BarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Report_DonBar
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 1);
            this.PageHeight = 475;
            this.PageWidth = 346;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "20.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRBarCode BarCode;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel Lbl_Cnt;
        private DevExpress.XtraReports.UI.XRLabel Lbl_Wht;
        private DevExpress.XtraReports.UI.XRLabel Lbl_CustName;
        private DevExpress.XtraReports.UI.XRLabel Lbl_Date;
        private DevExpress.XtraReports.UI.XRLabel Lbl_Lot;
        private DevExpress.XtraReports.UI.XRLabel Lbl_Length;
        private DevExpress.XtraReports.UI.XRLabel Lbl_Thic;
    }
}
