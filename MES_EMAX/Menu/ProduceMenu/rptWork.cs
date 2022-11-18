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
    public partial class rptWork : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();

        public rptWork()
        {
            InitializeComponent();
        }

        private void rptWork_Load(object sender, EventArgs e)
        {
            Grid_Set();
            dt_From.Focus();
        }

        private void Grid_Set()
        {
            // WorkM 그리드
            DbHelp.GridSet(Grid_Work, View_Work, "Work_Date", "지시일자", "110", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "WorkSheet_No", "작지번호", "100", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "Company_Name", "사업장", "100", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "Item_Code", "품목코드", "120", false, false, true); 
            DbHelp.GridSet(Grid_Work, View_Work, "Item_Name", "품목명", "120", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "SSize", "규격", "150", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "Q_Unit", "단위", "80", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "Qty", "지시수량", "80", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "Process_Name", "공정명", "100", false, false, true);
            DbHelp.GridSet(Grid_Work, View_Work, "Custom_Name", "작업팀", "100", false, false, false);

            DbHelp.GridColumn_NumSet(View_Work, "Qty", ForMat.SetDecimal("regWork", "Qty1"));
            
            DbHelp.Footer_Set(View_Work, ForMat.SetDecimal(this.Name, "Qty1").NumString(), new string[] { "Qty" });

            DbHelp.No_ReadOnly(View_Work);
            Grid_Work.PopMenuChk = false;
            Grid_Work.MouseWheelChk = false;
        }   

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void Search_Data()
        {
            string From = (string.IsNullOrWhiteSpace(dt_From.Text)) ? DateTime.MinValue.ToString("yyyyMMdd") : dt_From.DateTime.ToString("yyyyMMdd");
            string To = (string.IsNullOrWhiteSpace(dt_To.Text)) ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_To.DateTime.ToString("yyyyMMdd");

            SqlParam sp = new SqlParam("sp_rptWork");
            sp.AddParam("Kind", "S");
            sp.AddParam("From", From);
            sp.AddParam("To", To);

            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            Grid_Work.DataSource = ret.ReturnDataSet.Tables[0];
            Grid_Work.RefreshDataSource();
            View_Work.BestFitColumns();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(Grid_Work, "작업지시현황");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
    }
}
