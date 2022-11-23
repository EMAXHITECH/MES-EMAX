using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using DevExpress.XtraPrinting.Drawing;

namespace EMAX_Monitoring
{
    public partial class Report_Quot1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report_Quot1()
        {
            InitializeComponent();
        }

        public Report_Quot1(DataSet ds, string preview = "N") : this()
        {
            SetInit(ds, preview);
        }

        private void SetInit(DataSet ds, string preview)
        {
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            DataTable dt2 = ds.Tables[2];

            label_QuotNo.Text = dt.Rows[0]["Quot_No"].ToString();
            label_Custom_Name.Text = dt.Rows[0]["Custom_Name"].ToString();
            label_CustomUser.Text = dt.Rows[0]["Custom_User"].ToString();
            label_CustomNumber.Text = dt.Rows[0]["Custom_Number"].ToString();
            label_CustomEmail.Text = dt.Rows[0]["Custom_Email"].ToString();
            label_QuotDate.Text = dt.Rows[0]["Quot_Date"].ToString();
            label_Addr1.Text = dt.Rows[0]["Addr1"].ToString();
            label_Addr2.Text = dt.Rows[0]["Addr2"].ToString();
            label_Owenr.Text = dt.Rows[0]["Owner"].ToString();
            label_ProjectTitle.Text = dt.Rows[0]["Project_Title"].ToString();
            label_UserName.Text = dt.Rows[0]["User_Name"].ToString();
            label_MoblieNo.Text = dt.Rows[0]["Mobile_No"].ToString();
            label_EMail.Text = dt.Rows[0]["E_Mail"].ToString();
            label_SerialNo.Text = dt.Rows[0]["SerialNo"].ToString();

            MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Logo_Img"]);
            Image logo = Image.FromStream(ms);
            Picture_Logo.ImageSource = new ImageSource(logo);


            label_Bigo.Text = dt.Rows[0]["Quot_Bigo"].ToString();

            
            Amt.Text = dt.Rows[0]["Amt1"].ToString();
            Vat_Amt.Text = dt.Rows[0]["Vat_Amt"].ToString();
            Tot_Amt.Text = dt.Rows[0]["Tot_Amt"].ToString();

            //***출고정보를 가지고 바인딩 해줘야함***
            S_Item_Part.Text = dt1.Rows[0]["S_Item_Part"].ToString();
            S_Item_Name.Text = dt1.Rows[0]["S_Item_Name"].ToString();
            S_Spec.Text = dt1.Rows[0]["S_Spec"].ToString();
            S_Qty.Text = dt1.Rows[0]["S_Qty"].ToString();
            S_Month.Text = dt1.Rows[0]["S_Month"].ToString();
            S_SPrice.Text = dt1.Rows[0]["S_SPrice"].ToString();
            S_Amt.Text = dt1.Rows[0]["S_Amt"].ToString();

            for (int i = 0; i < Table_S1.Cells.Count; i++)
            {
                if (dt2.Columns[Table_S1.Cells[i].Name].DataType == typeof(decimal))
                {
                    Table_S1.Cells[i].DataBindings.Add("Text", null, dt2.Columns[Table_S1.Cells[i].Name].ColumnName, "{0:#,#}");
                }
                else
                {
                    Table_S1.Cells[i].DataBindings.Add("Text", null, dt2.Columns[Table_S1.Cells[i].Name].ColumnName);
                }
            }
            this.DataSource = dt2;

            ReportPrintTool print = new ReportPrintTool(this);
            this.ShowPrintMarginsWarning = false;

            if (preview == "Y")
                print.ShowPreview();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (PrintingSystem.PageCount == 0)
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
