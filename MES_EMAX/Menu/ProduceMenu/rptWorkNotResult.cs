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
    public partial class rptWorkNotResult : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptWorkNotResult()
        {
            InitializeComponent();
        }

        private void rptWorkNotResult_Load(object sender, EventArgs e)
        {
            //버튼 클릭 이벤트 선언
            this.btn_Select.Click += btn_Select_Click;
            this.btn_Excel.Click += btn_Excel_Click;
            this.btn_Close.Click += btn_Close_Click;
            Grid_Set();
        }

        private void Grid_Set()
        {
            // WorkM 그리드

            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Result_Date", "완료예정일", "125", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Work_Date", "작지일자", "125", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Work_No", "작지번호", "120", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Work_Sort", "입고번호", "120", false, false, false);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Company_Name", "사업장", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Short_Name", "작업팀", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Item_Code", "품목코드", "120", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Item_Name", "품목명", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Ssize", "규격", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Process_Name", "공정", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Qty", "지시수량", "80", true, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "NotQty", "미작업수량", "80", true, false, true);

            DbHelp.GridColumn_NumSet(gv_MatPoM, "Qty"       , ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatPoM, "NotQty"    , ForMat.SetDecimal(this.Name, "Qty1"));

            DbHelp.No_ReadOnly(gv_MatPoM);
        }

        #region 버튼 이벤트

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_MatPoM, this.Name);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();
        }

        #endregion

        #region 내부 함수

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_rptWorkNotResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("FDate_Work", dt_From.Text == "" ? "" : dt_From.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("TDate_Work", dt_To.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_To.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("FDate_Result", dt_InF.Text == "" ? "" : dt_InF.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("TDate_Result", dt_InT.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_InT.DateTime.ToString("yyyyMMdd"));

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_MatPoM.DataSource = ret.ReturnDataSet.Tables[0];
                gv_MatPoM.BestFitColumns();
                DbHelp.Footer_Set(gv_MatPoM, ForMat.SetDecimal("regMatPo", "Qty1").NullString(), new string[] { "Qty", "NotQty" });
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 상속 함수

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        #endregion

        private void gv_MatPoM_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string sIn_Date = gv_MatPoM.GetRowCellValue(e.RowHandle, "Result_Date").ToString();

            if (!string.IsNullOrWhiteSpace(sIn_Date))
            {
                if(DateTime.Compare(DateTime.Today, DateTime.Parse(sIn_Date)) > 0)
                {
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }

        private void gv_MatPoM_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }
    }
}
