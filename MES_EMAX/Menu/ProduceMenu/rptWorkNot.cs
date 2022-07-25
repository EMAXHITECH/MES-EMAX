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
    public partial class rptWorkNot : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptWorkNot()
        {
            InitializeComponent();
        }

        private void rptWorkNot_Load(object sender, EventArgs e)
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
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Work_Date", "작지예정일", "125", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "In_Date", "완료예정일", "110", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Plan_No", "계획번호", "120", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Company_Name", "사업장", "100", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Plan_Memo", "비고", "100", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Item_Code", "품목코드", "120", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Item_Name", "품목명", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "SSize", "규격", "150", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Process_Name", "공정", "100", false, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Qty", "지시예정수량", "80", true, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Not_Qty", "미지시수량", "80", true, false, true);
            DbHelp.GridSet(gc_MatPoM, gv_MatPoM, "Custom_Name", "작업팀", "100", false, false, true);

            DbHelp.GridColumn_NumSet(gv_MatPoM, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatPoM, "Not_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

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
                SqlParam sp = new SqlParam("sp_rptWorkNot");
                sp.AddParam("Kind", "S");
                sp.AddParam("FDate", dt_From.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("TDate", dt_To.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_To.DateTime.ToString("yyyyMMdd"));

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_MatPoM.DataSource = ret.ReturnDataSet.Tables[0];
                gv_MatPoM.BestFitColumns();
                DbHelp.Footer_Set(gv_MatPoM, ForMat.SetDecimal("regMatPo", "Qty1").NullString(), new string[] { "Qty", "Not_Qty" });
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

            string sIn_Date = gv_MatPoM.GetRowCellValue(e.RowHandle, "Work_Date").ToString();

            if (!string.IsNullOrWhiteSpace(sIn_Date))
            {
                if(DateTime.Compare(DateTime.Today, DateTime.Parse(sIn_Date)) > 0)
                {
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }
    }
}
