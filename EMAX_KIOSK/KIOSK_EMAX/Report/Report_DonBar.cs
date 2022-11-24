using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace KIOSK_EMAX
{
    public partial class Report_DonBar : DevExpress.XtraReports.UI.XtraReport
    {
        public Report_DonBar()
        {
            InitializeComponent();
        }

        public Report_DonBar(DataRow Row) : this()
        {
            SetData(Row);
        }

        private void SetData(DataRow Row)
        {
            BarCode.Text = Row["BarCode"].ToString();
            Lbl_Lot.Text = Row["BarCode"].ToString();
            Lbl_Date.Text = Row["Result_Date"].ToString();
            Lbl_CustName.Text = Row["cst_nm"].ToString();
            Lbl_Thic.Text = string.Format("{0:0}", Row["thic"].ToString());
            //Lbl_Pouk.Text = string.Format("{0:0}", Row["pouk"].ToString()); 
            Lbl_Length.Text = string.Format("{0:0}", Row["length"].ToString());
            Lbl_Wht.Text = string.Format("{0:0.#}", Row["wht"].ToString());
            Lbl_Cnt.Text = string.Format("{0:0}", Row["cnt"].ToString());

            ReportPrintTool print = new ReportPrintTool(this);
            this.ShowPrintMarginsWarning = false;

#if DEBUG
            
#else
            print.Print();
#endif
        }
    }
}
