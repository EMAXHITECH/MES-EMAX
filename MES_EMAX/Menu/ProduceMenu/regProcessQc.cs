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
using DevExpress.XtraEditors.Repository;

namespace MES
{
    public partial class regProcessQc : BaseReg
    {
        private ReturnStruct ret = new ReturnStruct();
        private DataTable dt_Bad = null;
        private DataTable dt_Value = null;

        private int iSave_Row = 0;
        private int iSelect_Row = 0;

        public regProcessQc()
        {
            InitializeComponent();
        }

        private void regProcessQc_Load(object sender, EventArgs e)
        {
            Grid_Set();

            LookUp_Set();
        }

        private void Grid_Set()
        {
            gc_Qc.AddRowYN = false;
            gc_Qc.MouseWheelChk = false;
            gc_Qc.PopMenuChk = false;

            DbHelp.GridSet(gc_Qc, gv_Qc, "Check", "체크", "100", false, false, false);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Result_Date", "실적일자", "100", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Result_No", "실적번호", "110", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Reulst_Sort", "Sort", "100", false, false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Company_Name", "사업장", "100", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Custom_Name", "작업처", "100", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Whouse_Name", "입고창고", "100", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Item_Code", "품목코드", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Item_Name", "품목명", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "SSize", "규격", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Process_Name", "공정", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Result_Qty", "생산수량", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Result", "검사구분", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Check", "검사", "80", false, true, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Good_Qty", "양품", "80", false, true, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Bad_Qty", "불량", "80", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Con_Qty", "특채", "80", false, true, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Sample_Qty", "시료수", "100", false, true, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Result_Name", "결과", "100", false, true, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Result_Code", "결과코드", "100", false, false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Qc_Date", "검사일자", "110", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "User_Name", "검사자", "110", false, false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Qc_No", "검사번호", "110", false, false, true, true);

            DbHelp.GridColumn_CheckBox(gv_Qc, "Check");

            DbHelp.GridColumn_Help(gv_Qc, "Result_Name", "Y");
            RepositoryItemButtonEdit btn_Qc = (RepositoryItemButtonEdit)gv_Qc.Columns["Result_Name"].ColumnEdit;
            btn_Qc.Buttons[0].Click += new EventHandler(Qc_Click);
            gv_Qc.Columns["Result_Name"].ColumnEdit = btn_Qc;

            DbHelp.GridColumn_CheckBox(gv_Qc, "QC_Check");

            DbHelp.GridColumn_NumSet(gv_Qc, "Result_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Qc, "Good_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Qc, "Bad_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Qc, "Con_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Qc, "Sample_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            gv_Qc.OptionsView.ShowAutoFilterRow = false;

            //불량
            gc_Bad.AddRowYN = true;
            gc_Bad.MouseWheelChk = true;
            gc_Bad.PopMenuChk = true;

            DbHelp.GridSet(gc_Bad, gv_Bad, "Result_No", "", "110", false, false, false);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Result_Sort", "", "110", false, false, false);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Qc_No", "", "110", false, false, false);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Qc_Sort", "", "100", false, false, false);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Fail_CodeNM", "수입검사 불량사유", "150", false, true, true, true);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Fail_Code", "불량코드", "110", false, false, false);
            DbHelp.GridSet(gc_Bad, gv_Bad, "Bad_Qty", "불량수량", "100", false, true, true, true);
            DbHelp.GridSet(gc_Bad, gv_Bad, "QC_GradeNM", "등급", "100", false, true, true, true);
            DbHelp.GridSet(gc_Bad, gv_Bad, "QC_Grade", "등급코드", "100", false, false, false);
            DbHelp.GridSet(gc_Bad, gv_Bad, "S_Bigo", "비고", "150", false, true, true, true);

            DbHelp.GridColumn_NumSet(gv_Bad, "Bad_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            DbHelp.GridColumn_Help(gv_Bad, "Fail_CodeNM", "Y");
            RepositoryItemButtonEdit btn_Bad = (RepositoryItemButtonEdit)gv_Bad.Columns["Fail_CodeNM"].ColumnEdit;
            btn_Bad.Buttons[0].Click += new EventHandler(QcBad_Click);
            gv_Bad.Columns["Fail_CodeNM"].ColumnEdit = btn_Bad;

            DbHelp.GridColumn_Help(gv_Bad, "QC_GradeNM", "Y");
            gv_Bad.Columns["QC_GradeNM"].ColumnEdit = btn_Bad;

            gv_Bad.OptionsView.ShowAutoFilterRow = false;

            gc_Bad.DeleteRowEventHandler += new EventHandler(Bad_Delete);

            //검사 항목
            gc_Value.AddRowYN = false;
            gc_Value.MouseWheelChk = false;
            gc_Value.PopMenuChk = false;

            DbHelp.GridSet(gc_Value, gv_Value, "Result_No", "", "110", false, false, false);
            DbHelp.GridSet(gc_Value, gv_Value, "Result_Sort", "", "110", false, false, false);
            DbHelp.GridSet(gc_Value, gv_Value, "Qc_No", "", "110", false, false, false);
            DbHelp.GridSet(gc_Value, gv_Value, "QC_Code", "검사항목코드", "100", false, false, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "QC_Name", "검사항목", "100", false, false, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "QC_Kind", "검사유형", "110", false, false, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "QC_Result", "결과구분", "100", false, false, false);
            DbHelp.GridSet(gc_Value, gv_Value, "Max_Value", "최댓값", "100", false, false, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "Min_Value", "최솟값", "100", false, false, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "QC_Value", "측정값", "100", false, true, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "Result_Name", "결과", "100", false, true, true, true);
            DbHelp.GridSet(gc_Value, gv_Value, "Result_Code", "결과", "100", false, false, false);
            DbHelp.GridSet(gc_Value, gv_Value, "S_Bigo", "비고", "100", false, true, true, true);

            DbHelp.GridColumn_Help(gv_Value, "Result_Name", "Y");
            RepositoryItemButtonEdit btn_Value = (RepositoryItemButtonEdit)gv_Value.Columns["Result_Name"].ColumnEdit;
            btn_Value.Buttons[0].Click += new EventHandler(QcValue_Click);
            gv_Value.Columns["Result_Name"].ColumnEdit = btn_Value;

            DbHelp.GridColumn_NumSet(gv_Value, "Max_Value", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Value, "Min_Value", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Value, "QC_Value", ForMat.SetDecimal(this.Name, "Qty1"));

            gv_Value.OptionsView.ShowAutoFilterRow = false;
        }

        #region 내부 함수
        private void LookUp_Set()
        {
            DataTable dt_LookUp = new DataTable();
            dt_LookUp.Columns.Add(new DataColumn("Code"));
            dt_LookUp.Columns.Add(new DataColumn("Value"));

            DataRow dr_LookUp = dt_LookUp.NewRow();
            dr_LookUp["Code"] = "001";
            dr_LookUp["Value"] = "검사";
            dt_LookUp.Rows.Add(dr_LookUp);

            dr_LookUp = dt_LookUp.NewRow();
            dr_LookUp["Code"] = "002";
            dr_LookUp["Value"] = "미검사";
            dt_LookUp.Rows.Add(dr_LookUp);

            dr_LookUp = dt_LookUp.NewRow();
            dr_LookUp["Code"] = "003";
            dr_LookUp["Value"] = "전체";
            dt_LookUp.Rows.Add(dr_LookUp);

            lookUp_QC.Properties.ValueMember = "Code";
            lookUp_QC.Properties.DisplayMember = "Value";
            lookUp_QC.Properties.DataSource = dt_LookUp;
            lookUp_QC.ItemIndex = 1;
        }

        private void Qc_Click(object sender, EventArgs e) // 수정 필요
        {
            int iRow = gv_Qc.FocusedRowHandle;

            if (iRow < 0)
                return;

            if (string.IsNullOrWhiteSpace(gv_Qc.GetRowCellValue(iRow, "Result_Name").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("General", "sp_Help_General", "60050", gv_Qc.GetRowCellValue(iRow, "Result_Name").ToString(), "N");

                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Qc.SetRowCellValue(iRow, "Result_Code", Help_Form.sRtCode);
                    gv_Qc.SetRowCellValue(iRow, "Result_Name", Help_Form.sRtCodeNm);
                }

                gv_Qc.UpdateCurrentRow();
            }
        }

        private void QcBad_Click(object sender, EventArgs e) // 수정 필요
        {
            int iRow = gv_Bad.FocusedRowHandle;

            if (iRow < 0)
                return;

            string sColumns = gv_Bad.FocusedColumn.FieldName;

            string sGeneral = "";

            if (sColumns == "Fail_CodeNM")
                sGeneral = "60014";
            else if (sColumns == "QC_GradeNM")
                sGeneral = "60015";


            if (string.IsNullOrWhiteSpace(gv_Bad.GetRowCellValue(iRow, sColumns.Substring(0, sColumns.Length - 2)).ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("General", "sp_Help_General", sGeneral, gv_Bad.GetRowCellValue(iRow, sColumns).ToString(), "N");

                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Bad.SetRowCellValue(iRow, sColumns.Substring(0, sColumns.Length - 2), Help_Form.sRtCode);
                    gv_Bad.SetRowCellValue(iRow, sColumns, Help_Form.sRtCodeNm);
                }

                gv_Bad.UpdateCurrentRow();
            }
        }

        private void QcValue_Click(object sender, EventArgs e) // 수정 필요
        {
            int iRow = gv_Value.FocusedRowHandle;

            if (iRow < 0)
                return;

            if (string.IsNullOrWhiteSpace(gv_Value.GetRowCellValue(iRow, "Result_Name").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("General", "sp_Help_General", "60050", gv_Value.GetRowCellValue(iRow, "Result_Name").ToString(), "N");

                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Value.SetRowCellValue(iRow, "Result_Code", Help_Form.sRtCode);
                    gv_Value.SetRowCellValue(iRow, "Result_Name", Help_Form.sRtCodeNm);
                }

                gv_Value.UpdateCurrentRow();
            }
        }

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regProcessQc");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");
                sp.AddParam("F_Result", dt_FResult.Text == "" ? "" : dt_FResult.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("T_Result", dt_TResult.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_TResult.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("F_Qc", dt_FQc.Text == "" ? "" : dt_FQc.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("T_Qc", dt_TQc.Text == "" ? DateTime.MaxValue.ToString("yyyyMMdd") : dt_TQc.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("QC_Result", lookUp_QC.EditValue.ToString());

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                btn_Insert.sUpdate = "N";

                gv_Qc.FocusedRowChanged -= gv_Qc_FocusedRowChanged;
                gc_Qc.DataSource = ret.ReturnDataSet.Tables[0];
                gv_Qc.FocusedRowChanged += gv_Qc_FocusedRowChanged;

                gv_Qc.BestFitColumns();

                iSave_Row = 0;

                if (gv_Qc.RowCount > 0)
                {
                    gv_Qc_FocusedRowChanged(gv_Qc, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, iSelect_Row));
                    gv_Qc.FocusedRowHandle = iSelect_Row;
                }
                else
                {
                    gc_Bad.DataSource = null;
                    gc_Value.DataSource = null;
                }

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_D(string sQc_No, string sResult_No, string sResult_Sort, string sPart)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regProcessQc");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "D");
                sp.AddParam("Qc_No", sQc_No);
                sp.AddParam("Result_No", sResult_No);
                sp.AddParam("Result_Sort", sResult_Sort);
                sp.AddParam("Part", sPart);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                if (sPart == "Bad")
                    gc_Bad.DataSource = ret.ReturnDataSet.Tables[0];
                else
                    gc_Value.DataSource = ret.ReturnDataSet.Tables[0];

                btn_Insert.sUpdate = "N";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Bad_Delete(object sender, EventArgs e)
        {
            try
            {
                int iRow = gv_Bad.FocusedRowHandle;

                string sQc_No = gv_Bad.GetRowCellValue(iRow, "Qc_No").ToString();
                string sQc_Sort = gv_Bad.GetRowCellValue(iRow, "Qc_Sort").ToString();

                SqlParam sp = new SqlParam("sp_regProcessQc");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "S");
                sp.AddParam("Qc_No", sQc_No);
                sp.AddParam("Qc_Sort", sQc_Sort);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_Bad.DeleteRow(iRow);

                decimal dBad_Qty = 0;

                for (int i = 0; i < gv_Bad.RowCount; i++)
                {
                    dBad_Qty += decimal.Parse(gv_Bad.GetRowCellValue(i, "Bad_Qty").NumString());
                }

                gv_Qc.SetRowCellValue(gv_Qc.FocusedRowHandle, "Bad_Qty", dBad_Qty);

                btn_Insert.sUpdate = "N";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();

            btn_Select.sCHK = "Y";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult_No = "", sResult_Sort = "", sGoodQty = "", sConQty = "", sSampleQty = "", sResult_Code = "", sQc_No = "";
                string sResult_NoBad = "", sResult_SortBad = "", sQc_NoBad = "", sFail_Code = "", sBad_Qty = "", sQG_Crade = "", sBigo_Bad = "", sQc_Sort = "";
                string sResult_NoValue = "", sResult_SortValue = "", sQc_NoValue = "",  sQC_Value = "", sResult_CodeValue = "", sBigo_Value = "", sQc_Code = "";

                //for(int i = 0; i < gv_Qc.RowCount; i++)
                //{
                //    if (gv_Qc.GetRowCellValue(i, "Check").ToString() == "Y")
                //    {
                sResult_No += gv_Qc.GetRowCellValue(iSave_Row, "Result_No").ToString() + "_/";
                sResult_Sort += gv_Qc.GetRowCellValue(iSave_Row, "Result_Sort").ToString() + "_/";
                sGoodQty += gv_Qc.GetRowCellValue(iSave_Row, "Good_Qty").NumString() + "_/";
                sConQty += gv_Qc.GetRowCellValue(iSave_Row, "Con_Qty").NumString() + "_/";
                sSampleQty += gv_Qc.GetRowCellValue(iSave_Row, "Sample_Qty").NumString() + "_/";
                sResult_Code += gv_Qc.GetRowCellValue(iSave_Row, "Result_Code").ToString() + "_/";
                sQc_No += gv_Qc.GetRowCellValue(iSave_Row, "Qc_No").ToString() + "_/";
                //    }
                //}

                //if (dt_Bad != null)
                //{
                //    for (int i = 0; i < dt_Bad.Rows.Count; i++)
                //    {
                //        sIn_NoBad += dt_Bad.Rows[i]["In_No"].ToString() + "_/";
                //        sIn_SortBad += dt_Bad.Rows[i]["In_Sort"].ToString() + "_/";
                //        sQc_NoBad += dt_Bad.Rows[i]["Qc_No"].ToString() + "_/";
                //        sFail_Code += dt_Bad.Rows[i]["Fail_Code"].ToString() + "_/";
                //        sBad_Qty += dt_Bad.Rows[i]["Bad_Qty"].NumString() + "_/";
                //        sQG_Crace += dt_Bad.Rows[i]["QC_Grace"].ToString() + "_/";
                //        sBigo_Bad += dt_Bad.Rows[i]["S_Bigo"].ToString() + "_/";
                //        sQc_Sort += dt_Bad.Rows[i]["Qc_Sort"].ToString() + "_/";
                //    }
                //}

                //if (dt_Value != null)
                //{
                //    for (int i = 0; i < dt_Value.Rows.Count; i++)
                //    {
                //        sIn_NoValue += dt_Value.Rows[i]["In_No"].ToString() + "_/";
                //        sIn_SortValue += dt_Value.Rows[i]["In_Sort"].ToString() + "_/";
                //        sQc_NoValue += dt_Value.Rows[i]["Qc_No"].ToString() + "_/";
                //        sQC_Value += dt_Value.Rows[i]["QC_Value"].NumString() + "_/";
                //        sResult_CodeValue += dt_Value.Rows[i]["Result_Code"].ToString() + "_/";
                //        sBigo_Value += dt_Value.Rows[i]["S_Bigo"].ToString() + "_/";
                //        sQc_Code += dt_Value.Rows[i]["QC_Code"].ToString() + "_/";
                //    }
                //}

                for(int i = 0; i < gv_Bad.RowCount; i++)
                {
                    if (!string.IsNullOrWhiteSpace(gv_Bad.GetRowCellValue(i, "Fail_Code").ToString()))
                    {
                        sResult_NoBad += gv_Bad.GetRowCellValue(i, "Result_No").ToString() + "_/";
                        sResult_SortBad += gv_Bad.GetRowCellValue(i, "Result_Sort").ToString() + "_/";
                        sQc_NoBad += gv_Bad.GetRowCellValue(i, "Qc_No").ToString() + "_/";
                        sFail_Code += gv_Bad.GetRowCellValue(i, "Fail_Code").ToString() + "_/";
                        sBad_Qty += gv_Bad.GetRowCellValue(i, "Bad_Qty").NumString() + "_/";
                        sQG_Crade += gv_Bad.GetRowCellValue(i, "QC_Grade").ToString() + "_/";
                        sBigo_Bad += gv_Bad.GetRowCellValue(i, "S_Bigo").ToString() + "_/";
                        sQc_Sort += gv_Bad.GetRowCellValue(i, "Qc_Sort").ToString() + "_/";
                    }
                }

                for(int i = 0; i < gv_Value.RowCount; i++)
                {
                    sResult_NoValue += gv_Value.GetRowCellValue(i, "Result_No").ToString() + "_/";
                    sResult_SortValue += gv_Value.GetRowCellValue(i, "Result_Sort").ToString() + "_/";
                    sQc_NoValue += gv_Value.GetRowCellValue(i, "Qc_No").ToString() + "_/";
                    sQC_Value += gv_Value.GetRowCellValue(i, "QC_Value").NumString() + "_/";
                    sResult_CodeValue += gv_Value.GetRowCellValue(i, "Result_Code").ToString() + "_/";
                    sBigo_Value += gv_Value.GetRowCellValue(i, "S_Bigo").ToString() + "_/";
                    sQc_Code += gv_Value.GetRowCellValue(i, "QC_Code").ToString() + "_/";
                }

                SqlParam sp = new SqlParam("sp_regProcessQc");
                sp.AddParam("Kind", "I");
                sp.AddParam("User_Code", GlobalValue.sUserID);
                sp.AddParam("ResultNo", sResult_No);
                sp.AddParam("ResultSort", sResult_Sort);
                sp.AddParam("GoodQty", sGoodQty);
                sp.AddParam("ConQty", sConQty);
                sp.AddParam("SampleQty", sSampleQty);
                sp.AddParam("ResultCode", sResult_Code);
                sp.AddParam("QcNo", sQc_No);

                sp.AddParam("ResultNoBad", sResult_NoBad);
                sp.AddParam("ResultSortBad", sResult_SortBad);
                sp.AddParam("QcNoBad", sQc_NoBad);
                sp.AddParam("QcSort", sQc_Sort);
                sp.AddParam("FailCode", sFail_Code);
                sp.AddParam("BadQty", sBad_Qty);
                sp.AddParam("QCGrade", sQG_Crade);
                sp.AddParam("BigoBad", sBigo_Bad);

                sp.AddParam("ResultNoValue", sResult_NoValue);
                sp.AddParam("ResultSortValue", sResult_SortValue);
                sp.AddParam("QcNoValue", sQc_NoValue);
                sp.AddParam("QCValue", sQC_Value);
                sp.AddParam("ResultCodeValue", sResult_CodeValue);
                sp.AddParam("BigoValue", sBigo_Value);
                sp.AddParam("QCCode", sQc_Code);

                sp.AddParam("Form_Name", this.Name);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                if (gv_Qc.RowCount - 1 == iSelect_Row && lookUp_QC.EditValue.ToString() != "003")
                    iSelect_Row -= 1;

                Search();

                btn_Save.sCHK = "Y";

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int iRow = gv_Qc.FocusedRowHandle;

            string sQC_No = gv_Qc.GetRowCellValue(iRow, "Qc_No").ToString();

            if (string.IsNullOrWhiteSpace(sQC_No))
                return;

            try
            {
                SqlParam sp = new SqlParam("sp_regProcessQc");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("Qc_No", sQC_No);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                if (lookUp_QC.EditValue.ToString() != "003")
                    iSelect_Row = iRow - 1;

                Search();

                btn_Delete.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        #endregion

        #region 그리드 이벤트
        private void gc_Qc_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_Qc.FocusedColumn.FieldName == "Result_Name")
                    Qc_Click(null, null);
            }
        }
        private void gc_Bad_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_Bad.FocusedColumn.FieldName == "Fail_CodeNM")
                    QcBad_Click(null, null);
                else if (gv_Bad.FocusedColumn.FieldName == "QC_GradeNM")
                    QcBad_Click(null, null);
            }
        }

        private void gc_Value_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_Value.FocusedColumn.FieldName == "Result_Name")
                    QcValue_Click(null, null);
            }
        }

        private void gv_Qc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Result_Name")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_General", e.Value.ToString(), "60050");
                if (dr_Help == null)
                {
                    gv_Qc.SetRowCellValue(e.RowHandle, "Result_Code", "");
                }
                else
                {
                    gv_Qc.SetRowCellValue(e.RowHandle, "Result_Code", dr_Help["GS_Code"]);
                    gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                    gv_Qc.SetRowCellValue(e.RowHandle, "Result_Name", dr_Help["GS_Name"]);
                    gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                }
            }
            else if (e.Column.FieldName == "Good_Qty")
            {
                decimal dGood_Qty = decimal.Parse(e.Value.NumString());
                decimal dResult_Qty = decimal.Parse(gv_Qc.GetRowCellValue(e.RowHandle, "Result_Qty").NumString());
                decimal dBad_Qty = decimal.Parse(gv_Qc.GetRowCellValue(e.RowHandle, "Bad_Qty").NumString());

                if (dResult_Qty < (dGood_Qty + dBad_Qty))
                {
                    XtraMessageBox.Show("입고수량보다 많을 수 없습니다");

                    gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                    gv_Qc.SetRowCellValue(e.RowHandle, "Good_Qty", dResult_Qty - dBad_Qty);
                    gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                    return;
                }
            }
            else if (e.Column.FieldName == "Bad_Qty")
            {
                decimal dBad_Qty = decimal.Parse(e.Value.NumString());
                decimal dResult_Qty = decimal.Parse(gv_Qc.GetRowCellValue(e.RowHandle, "Result_Qty").NumString());
                decimal dGood_Qty = decimal.Parse(gv_Qc.GetRowCellValue(e.RowHandle, "Good_Qty").NumString());

                //if (dIn_Qty < (dGood_Qty + dBad_Qty))
                //{
                gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                gv_Qc.SetRowCellValue(e.RowHandle, "Good_Qty", dResult_Qty - dBad_Qty);
                gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                //}
            }
            else if (e.Column.FieldName == "Con_Qty")
            {
                decimal dBad_Qty = decimal.Parse(gv_Qc.GetRowCellValue(e.RowHandle, "Bad_Qty").NumString());
                decimal dCon_Qty = decimal.Parse(e.Value.NumString());

                if (dBad_Qty < dCon_Qty)
                {
                    gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                    gv_Qc.SetRowCellValue(e.RowHandle, "Con_Qty", dBad_Qty);
                    gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                }
            }
            
            btn_Insert.sUpdate = "Y";
        }

        private void gv_Qc_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "QC_Check")
            {
                if(e.Value.ToString() == "Y")
                {
                    gv_Qc.SetRowCellValue(e.RowHandle, "Good_Qty", gv_Qc.GetRowCellValue(e.RowHandle, "Result_Qty"));
                    gv_Qc.SetRowCellValue(e.RowHandle, "Qc_Date", DateTime.Today.ToString("yyyy-MM-dd"));
                    gv_Qc.SetRowCellValue(e.RowHandle, "User_Name", GlobalValue.sUserNm);
                    gv_Qc.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanging);
                    gv_Qc.SetRowCellValue(e.RowHandle, "QC_Check", "Y");
                    gv_Qc.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanging);

                    gv_Qc.UpdateCurrentRow();
                    //gv_Qc.Columns["Qc_Date"].Width = 110;
                    gv_Qc.BestFitColumns();
                }
                else
                {
                    gv_Qc.SetRowCellValue(e.RowHandle, "Good_Qty", 0);
                    gv_Qc.SetRowCellValue(e.RowHandle, "Qc_Date", "");
                    gv_Qc.SetRowCellValue(e.RowHandle, "User_Name", "");
                    gv_Qc.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanging);
                    gv_Qc.SetRowCellValue(e.RowHandle, "QC_Check", "N");
                    gv_Qc.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanging);
                    gv_Qc.UpdateCurrentRow();
                }
                btn_Insert.sUpdate = "Y";
            }
        }

        private void gv_Value_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Result_Name")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_General", e.Value.ToString(), "60050");
                if (dr_Help == null)
                {
                    gv_Value.SetRowCellValue(e.RowHandle, "Result_Code", "");
                }
                else
                {
                    gv_Value.SetRowCellValue(e.RowHandle, "Result_Code", dr_Help["GS_Code"]);
                    gv_Value.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Value_CellValueChanged);
                    gv_Value.SetRowCellValue(e.RowHandle, "Result_Name", dr_Help["GS_Name"]);
                    gv_Value.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Value_CellValueChanged);
                }
            }

            btn_Insert.sUpdate = "Y";

            //string sIn_No = gv_Value.GetRowCellValue(e.RowHandle, "In_No").ToString();
            //string sIn_Sort = gv_Value.GetRowCellValue(e.RowHandle, "In_Sort").ToString();
            //string sQc_Code = gv_Value.GetRowCellValue(e.RowHandle, "QC_Code").ToString();

            //if (dt_Value == null)
            //{
            //    dt_Value = gc_Value.DataSource as DataTable;
            //}
            //else
            //{
            //    if (dt_Value.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "' AND QC_Code = '" + sQc_Code + "'").Length > 0)
            //    {
            //        DataRow dr_Update = dt_Value.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "' AND QC_Code = '" + sQc_Code + "'")[0];
            //        dr_Update.BeginEdit();
            //        dr_Update[e.Column.FieldName] = e.Value.ToString();
            //        dr_Update.EndEdit();
            //    }
            //    else
            //    {
            //        DataRow dr_Add = dt_Value.NewRow();
            //        dr_Add.BeginEdit();
            //        dr_Add.ItemArray = gv_Value.GetDataRow(e.RowHandle).ItemArray;
            //        dr_Add.EndEdit();
            //        dt_Value.Rows.Add(dr_Add);
            //    }
            //}
        }

        private void gv_Bad_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Fail_CodeNM")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_General", e.Value.ToString(), "60014");
                if (dr_Help == null)
                {
                    gv_Bad.SetRowCellValue(e.RowHandle, "Fail_Code", "");
                }
                else
                {
                    gv_Bad.SetRowCellValue(e.RowHandle, "Fail_Code", dr_Help["GS_Code"]);
                    gv_Bad.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);
                    gv_Bad.SetRowCellValue(e.RowHandle, "Fail_CodeNM", dr_Help["GS_Name"]);
                    gv_Bad.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);
                }
            }
            else if (e.Column.FieldName == "QC_GradeNM")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_General", e.Value.ToString(), "60015");
                if (dr_Help == null)
                {
                    gv_Bad.SetRowCellValue(e.RowHandle, "QC_Grade", "");
                }
                else
                {
                    gv_Bad.SetRowCellValue(e.RowHandle, "QC_Grade", dr_Help["GS_Code"]);
                    gv_Bad.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);
                    gv_Bad.SetRowCellValue(e.RowHandle, "QC_GradeNM", dr_Help["GS_Name"]);
                    gv_Bad.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);
                }
            }
            else if (e.Column.FieldName == "Bad_Qty")
            {
                decimal dBad_Qty = 0;

                for (int i = 0; i < gv_Bad.RowCount; i++)
                {
                    //엑셀 붙여넣기시 처리되도록 수정
                    if (e.RowHandle > 0)
                        dBad_Qty += decimal.Parse(gv_Bad.GetRowCellValue(i, "Bad_Qty").NumString());
                    else
                        dBad_Qty += decimal.Parse(gv_Bad.GetRowCellValue(e.RowHandle, "Bad_Qty").NumString());
                }

                gv_Qc.SetRowCellValue(gv_Qc.FocusedRowHandle, "Bad_Qty", dBad_Qty);
            }

            btn_Insert.sUpdate = "Y";

            //string sIn_No = gv_Bad.GetRowCellValue(e.RowHandle, "In_No").ToString();
            //string sIn_Sort = gv_Bad.GetRowCellValue(e.RowHandle, "In_Sort").ToString();
            //string sQC_Sort = gv_Bad.GetRowCellValue(e.RowHandle, "Qc_Sort").ToString();

            //if (dt_Bad == null)
            //{
            //    dt_Bad = gc_Bad.DataSource as DataTable;
            //}
            //else
            //{
            //    if (dt_Bad.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "' AND Qc_Sort = '" + sQC_Sort + "'").Length > 0)
            //    {
            //        DataRow dr_Update = dt_Bad.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "' AND Qc_Sort = '" + sQC_Sort + "'")[0];
            //        dr_Update.BeginEdit();
            //        dr_Update[e.Column.FieldName] = e.Value.ToString();
            //        dr_Update.EndEdit();
            //    }
            //    else
            //    {
            //        DataRow dr_Add = dt_Bad.NewRow();
            //        dr_Add.BeginEdit();
            //        dr_Add.ItemArray = gv_Bad.GetDataRow(e.RowHandle).ItemArray;
            //        dr_Add.EndEdit();
            //        dt_Bad.Rows.Add(dr_Add);
            //    }
            //}
        }

        private void gv_Qc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if(btn_Insert.sUpdate == "Y")
            {
                iSave_Row = e.PrevFocusedRowHandle;
                iSelect_Row = e.FocusedRowHandle;
                if (XtraMessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    if (string.IsNullOrWhiteSpace(gv_Qc.GetRowCellValue(iSave_Row, "Qc_No").ToString()))
                    {
                        gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                        gv_Qc.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanging);
                        gv_Qc.SetRowCellValue(iSave_Row, "QC_Check", "N");
                        gv_Qc.SetRowCellValue(iSave_Row, "Good_Qty", null);
                        gv_Qc.SetRowCellValue(iSave_Row, "Bad_Qty", null);
                        gv_Qc.SetRowCellValue(iSave_Row, "Con_Qty", null);
                        gv_Qc.SetRowCellValue(iSave_Row, "Sample_Qty", null);
                        gv_Qc.SetRowCellValue(iSave_Row, "Result_Name", "");
                        gv_Qc.SetRowCellValue(iSave_Row, "Result_Code", "");
                        gv_Qc.SetRowCellValue(iSave_Row, "Qc_Date", "");
                        gv_Qc.SetRowCellValue(iSave_Row, "User_Name", "");
                        gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                        gv_Qc.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanging);
                    }
                    else
                    {
                        Search();
                        return;
                    }

                    iSave_Row = e.FocusedRowHandle;
                    iSelect_Row = e.FocusedRowHandle;
                }
                else
                {
                    btn_Save_Click(null, null);
                    return;
                }
            }
            else
            {
                iSave_Row = e.FocusedRowHandle;
                iSelect_Row = e.FocusedRowHandle;
            }

            string sResult_No = gv_Qc.GetRowCellValue(e.FocusedRowHandle, "Result_No").ToString();
            string sResult_Sort = gv_Qc.GetRowCellValue(e.FocusedRowHandle, "Result_Sort").ToString();
            string sQc_No = gv_Qc.GetRowCellValue(e.FocusedRowHandle, "Qc_No").ToString();

            txt_QcNo.Text = sQc_No;

            //if (dt_Bad != null && dt_Bad.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "'").Length > 0)
            //{
            //    gc_Bad.DataSource = dt_Bad.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "'").CopyToDataTable();
            //}
            //else
            //{
            Search_D(sQc_No, sResult_No, sResult_Sort, "Bad");
            //}

            //if(dt_Value != null && dt_Value.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "'").Length > 0)
            //{
            //    gc_Value.DataSource = dt_Value.Select("In_No = '" + sIn_No + "' AND In_Sort = '" + sIn_Sort + "'").CopyToDataTable();
            //}
            //else
            //{
            Search_D(sQc_No, sResult_No, sResult_Sort, "Value");
            //}

            if (!string.IsNullOrWhiteSpace(sQc_No))
            {
                gv_Qc.Columns["QC_Check"].OptionsColumn.ReadOnly = true;
                gv_Qc.Columns["QC_Check"].OptionsColumn.AllowEdit = false;
            }
            else
            {
                gv_Qc.Columns["QC_Check"].OptionsColumn.ReadOnly = false;
                gv_Qc.Columns["QC_Check"].OptionsColumn.AllowEdit = true;
            }
        }
        private void gv_Bad_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (gv_Qc.RowCount < 1)
                return;

            int iHRow = gv_Qc.FocusedRowHandle;

            gv_Bad.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);
            gv_Bad.SetRowCellValue(e.RowHandle, "Result_No", gv_Qc.GetRowCellValue(iHRow, "Result_No").ToString());
            gv_Bad.SetRowCellValue(e.RowHandle, "Result_Sort", gv_Qc.GetRowCellValue(iHRow, "Result_Sort").ToString());
            gv_Bad.SetRowCellValue(e.RowHandle, "Qc_No", gv_Qc.GetRowCellValue(iHRow, "Qc_No").ToString());
            gv_Bad.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);

            int iQc_Sort = 0;

            if (gv_Bad.RowCount == 1)
                iQc_Sort += 1;
            else
            {
                for (int i = 0; i < gv_Bad.RowCount; i++)
                {
                    if (i != e.RowHandle && iQc_Sort <= int.Parse(gv_Bad.GetRowCellValue(i, "Qc_Sort").NumString()))
                        iQc_Sort = int.Parse(gv_Bad.GetRowCellValue(i, "Qc_Sort").NumString()) + 1;
                }
            }

            gv_Bad.SetRowCellValue(e.RowHandle, "Qc_Sort", iQc_Sort);
        }

        private void gc_Bad_NewRowAdd(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (gv_Qc.RowCount < 1)
                return;

            int iHRow = gv_Qc.FocusedRowHandle;

            gv_Bad.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);
            gv_Bad.SetRowCellValue(e.RowHandle, "Result_No", gv_Qc.GetRowCellValue(iHRow, "Result_No").ToString());
            gv_Bad.SetRowCellValue(e.RowHandle, "Result_Sort", gv_Qc.GetRowCellValue(iHRow, "Result_Sort").ToString());
            gv_Bad.SetRowCellValue(e.RowHandle, "Qc_No", gv_Qc.GetRowCellValue(iHRow, "Qc_No").ToString());
            gv_Bad.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Bad_CellValueChanged);

            int iQc_Sort = 0;

            if (gv_Bad.RowCount == 1)
                iQc_Sort += 1;
            else
            {
                for (int i = 0; i < gv_Bad.RowCount; i++)
                {
                    if (i != e.RowHandle && iQc_Sort <= int.Parse(gv_Bad.GetRowCellValue(i, "Qc_Sort").NumString()))
                        iQc_Sort = int.Parse(gv_Bad.GetRowCellValue(i, "Qc_Sort").NumString()) + 1;
                }
            }


            gv_Bad.SetRowCellValue(e.RowHandle, "Qc_Sort", iQc_Sort);
        }


        #endregion

        private void lookUp_QC_EditValueChanged(object sender, EventArgs e)
        {
            iSelect_Row = 0;
            iSave_Row = 0;
        }
    }
}
