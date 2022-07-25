using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MES
{
    public partial class rptWorkResult : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();

        public rptWorkResult()
        {
            InitializeComponent();
        }

        private void rptWorkResult_Load(object sender, EventArgs e)
        {
            Grid_Set();
            dt_From.Focus();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_Result, View_Result, "Result_Date", "실적일자", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Result_No", "실적번호", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "WorkSheet_No", "작지번호", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Company_Name", "사업장", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Item_Code", "품목코드", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Item_Name", "품목명", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "SSize", "규격", "150", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Q_Unit", "단위", "80", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Process_Name", "공정", "120", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Qty", "지시수량", "80", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Work_Qty", "총작업수량", "80", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Good_Qty", "양품수량", "80", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Bad_Qty", "불량수량", "80", false, false, true);
            DbHelp.GridSet(Grid_Result, View_Result, "Custom_Name", "작업팀", "100", false, false, true);

            DbHelp.GridColumn_NumSet(View_Result, "Qty", ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridColumn_NumSet(View_Result, "Work_Qty", ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridColumn_NumSet(View_Result, "Good_Qty", ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.GridColumn_NumSet(View_Result, "Bad_Qty", ForMat.SetDecimal("regWork", "Qty1"));
            DbHelp.Footer_Set(View_Result, ForMat.SetDecimal("regWork", "Qty1").NumString(), new string[] { "Qty", "Work_Qty", "Good_Qty", "Bad_Qty" });

            DbHelp.No_ReadOnly(View_Result);
            Grid_Result.PopMenuChk = false;
            Grid_Result.MouseWheelChk = false;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void Search_Data()
        {
            string From = (string.IsNullOrWhiteSpace(dt_From.Text)) ? DateTime.MinValue.ToString("yyyyMMdd") : dt_From.DateTime.ToString("yyyyMMdd");
            string To = (string.IsNullOrWhiteSpace(dt_To.Text)) ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_To.DateTime.ToString("yyyyMMdd");

            SqlParam sp = new SqlParam("sp_rptWorkResult");
            sp.AddParam("Kind", "S");
            sp.AddParam("From", From);
            sp.AddParam("To", To);

            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            Grid_Result.DataSource = ret.ReturnDataSet.Tables[0];
            Grid_Result.RefreshDataSource();
            View_Result.BestFitColumns();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(Grid_Result, "생산실적현황");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
    }
}
