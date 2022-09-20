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
    public partial class rptProcessQcBad : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptProcessQcBad()
        {
            InitializeComponent();
        }

        private void rptProcessQcBad_Load(object sender, EventArgs e)
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
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Item_Code", "품목코드", "125", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Item_Name", "품목명", "120", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "SSize", "규격", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Process_Name", "공정", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Qty", "총생산량", "125", true, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "TBad_Qty", "총불량수량", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "TBad_Per", "불량률", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Fail_Name", "불량사유", "100", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Bad_Qty", "불량수량", "100", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Bad_Per", "불량점유율", "100", false, false, true);

            DbHelp.GridColumn_NumSet(gv_MatQcM, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatQcM, "TBad_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatQcM, "Bad_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatQcM, "TBad_Per", 1);
            DbHelp.GridColumn_NumSet(gv_MatQcM, "Bad_Per", 1);

            DbHelp.No_ReadOnly(gv_MatQcM);

            gc_MatQcM.MouseWheelChk = false;
            gc_MatQcM.PopMenuChk = false;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_MatQcM, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_rptProcessQcBad");
            sp.AddParam("Kind", "S");
            sp.AddParam("From", dt_From.Text == "" ? "" : dt_From.DateTime.ToString("yyyyMM"));
            sp.AddParam("To", dt_To.Text == "" ? DateTime.MaxValue.ToString("yyyyMM") : dt_To.DateTime.ToString("yyyyMM"));
            //sp.AddParam("Start", dt_Start.Text == "" ? "" : dt_Start.DateTime.ToString("yyyyMMdd"));
            //sp.AddParam("End", dt_End.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_End.DateTime.ToString("yyyyMMdd"));

            ret = DbHelp.Proc_Search(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }

            gc_MatQcM.DataSource = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
            gc_MatQcM.RefreshDataSource();
            gv_MatQcM.BestFitColumns();

            DbHelp.Footer_Set(gv_MatQcM, ForMat.SetDecimal(this.Name, "Qty1").NullString(), new string[] { "Qty", "TBad_Qty", "Bad_Qty"});
        }

        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }
        #endregion

        private void gv_MatInM_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle < 0)
            //    return;

            //if (e.Column.FieldName == "Qty" || e.Column.FieldName == "Amt" || e.Column.FieldName == "Vat_Amt" || e.Column.FieldName == "Tot_Amt")
            //{
            //    if (gv_MatQcM.GetRowCellValue(e.RowHandle, "In_Part").ToString() == "자재입고")
            //        e.Appearance.ForeColor = Color.Blue;
            //    else
            //        e.Appearance.ForeColor = Color.Red;
            //}
        }

        private void gv_MatQcM_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string sFieldName = e.Column.FieldName;
            if (sFieldName == "Item_Code" || sFieldName == "Item_Name" || sFieldName == "SSize" || sFieldName == "Process_Name")
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}

