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
    public partial class Report_Po : DevExpress.XtraReports.UI.XtraReport
    {
        public Report_Po()
        {
            InitializeComponent();
        }

        public Report_Po(DataSet ds, string preview = "N") : this()
        {
            SetInit(ds, preview);
        }

        private void SetInit(DataSet ds, string preview)
        {
            DataTable dt = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            label_PoBigo.Text = dt.Rows[0]["Po_Bigo"].ToString();

            label_Custom_Name.Text = dt.Rows[0]["Custom_Name"].ToString();
            label_CustomUser.Text = dt.Rows[0]["Custom_User"].ToString();
            label_CustomTel.Text = dt.Rows[0]["Custom_Tel"].ToString();
            label_CustomFax.Text = dt.Rows[0]["Custom_Fax"].ToString();

            label_User.Text = dt.Rows[0]["User_Name"].ToString();
            label_Addr.Text = dt.Rows[0]["Addr"].ToString();
            label_Tel.Text = dt.Rows[0]["Tel"].ToString();
            label_Fax.Text = dt.Rows[0]["Fax"].ToString();
            label_Company.Text = dt.Rows[0]["Company"].ToString();

            MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Logo_Img"]);
            Image logo = Image.FromStream(ms);
            Picture_Logo.ImageSource = new ImageSource(logo);

            Po_Date.Text = dt.Rows[0]["Po_Date"].ToString();

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

            int iAdd = dt2.Rows.Count % 10;

            for(int i = iAdd; i < 10; i++)
            {
                DataRow dr = dt2.NewRow();
                dr["No"] = (i + 1);
                dt2.Rows.Add(dr);
            }

            this.DataSource = dt2;

            //this.ReportPrintOptions.DetailCountOnEmptyDataSource = 10;
            //this.ReportPrintOptions.BlankDetailCount = 10;
            //this.ReportPrintOptions.DetailCount = 10;

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
