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
    public partial class rptProcessQc : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public rptProcessQc()
        {
            InitializeComponent();
        }

        private void rptProcessQc_Load(object sender, EventArgs e)
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
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Qc_Date", "검사일자", "125", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Qc_No", "검사번호", "120", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "User_Name", "검사자", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Custom_Name", "작업처", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Result_Date", "실적일자", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Item_Code", "품목코드", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Item_Name", "품목명", "80", true, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Process_Name", "공정", "125", true, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Good_Qty", "양품", "125", true, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Bad_Qty", "불량", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Con_Qty", "특채", "150", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Sample_Qty", "시료수", "100", false, false, true);
            DbHelp.GridSet(gc_MatQcM, gv_MatQcM, "Result_Name", "결과", "100", false, false, true);

            DbHelp.GridColumn_NumSet(gv_MatQcM, "Good_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatQcM, "Bad_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatQcM, "Con_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_MatQcM, "Sample_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            DbHelp.No_ReadOnly(gv_MatQcM);

            gc_MatQcM.MouseWheelChk = false;
            gc_MatQcM.PopMenuChk = false;


            //불량 유형
            DbHelp.GridSet(gc_Bad, gv_Bad, "Fail_Name", "불량사유", "125", false, false, true);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Bad_Qty", "불량수량", "120", false, false, true);

            DbHelp.GridColumn_NumSet(gv_Bad, "Bad_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            gc_Bad.MouseWheelChk = false;
            gc_Bad.PopMenuChk = false;
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
            SqlParam sp = new SqlParam("sp_rptProcessQc");
            sp.AddParam("Kind", "S");
            sp.AddParam("Search_D", "H");
            sp.AddParam("From", dt_From.Text == "" ? "" : dt_From.DateTime.ToString("yyyyMMdd"));
            sp.AddParam("To", dt_To.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_To.DateTime.ToString("yyyyMMdd"));
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

            DbHelp.Footer_Set(gv_MatQcM, ForMat.SetDecimal(this.Name, "Qty1").NullString(), new string[] { "Good_Qty", "Bad_Qty", "Con_Qty", "Sample_Qty" });
        }

        private void Search_D(string sQC_No)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_rptProcessQc");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "D");
                sp.AddParam("Qc_No", sQC_No);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Bad.DataSource = ret.ReturnDataSet.Tables[0];
                gv_Bad.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
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

        private void gv_MatQcM_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            Search_D(gv_MatQcM.GetRowCellValue(e.FocusedRowHandle, "Qc_No").ToString());
        }
    }
}

