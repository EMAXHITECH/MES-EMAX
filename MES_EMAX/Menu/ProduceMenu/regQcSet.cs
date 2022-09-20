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
    public partial class regQcSet : BaseReg
    {
        private ReturnStruct ret = new ReturnStruct();

        private DataTable dt_Part = null;

        private int iFocusRow = 0;
       
        public regQcSet()
        {
            InitializeComponent();
        }

        private void regQcSet_Load(object sender, EventArgs e)
        {
            Grid_Set();

            Search();
        }

        private void Grid_Set()
        {
            gc_Qc.AddRowYN = true;

            DbHelp.GridSet(gc_Qc, gv_Qc, "Sort_No", "순번", "110", false, false, false);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Code", "검사항목코드", "100", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Name", "검사항목", "100", false, true, true);        
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_KindNM", "검사유형", "100", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Kind", "", "100", false, false, false);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_ResultNM", "결과구분", "100", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Result", "", "80", false, false, false);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Int", "소수점", "80", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_RangeNM", "범위", "80", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Range", "", "80", false, false, false);

            DbHelp.GridColumn_Help(gv_Qc, "QC_KindNM", "Y");
            RepositoryItemButtonEdit btn_edit = (RepositoryItemButtonEdit)gv_Qc.Columns["QC_KindNM"].ColumnEdit;
            btn_edit.Buttons[0].Click += new EventHandler(btn_Click);
            gv_Qc.Columns["QC_KindNM"].ColumnEdit = btn_edit;

            DbHelp.GridColumn_Help(gv_Qc, "QC_ResultNM", "Y");
            gv_Qc.Columns["QC_ResultNM"].ColumnEdit = btn_edit;

            DbHelp.GridColumn_Help(gv_Qc, "QC_RangeNM", "Y");
            gv_Qc.Columns["QC_RangeNM"].ColumnEdit = btn_edit;


            DbHelp.GridColumn_NumSet(gv_Qc, "QC_Int", 0);

            gc_Qc.DeleteRowEventHandler += new EventHandler(Code_Delete); // 그리드 우클릭 삭제

            gv_Qc.OptionsView.ShowAutoFilterRow = false;

            //Part
            gc_QcPart.AddRowYN = true;

            DbHelp.GridSet(gc_QcPart, gv_QcPart, "QC_Code", "구분", "100", false, false, false);
            DbHelp.GridSet(gc_QcPart, gv_QcPart, "QC_PartNM", "구분", "100", false, true, true);
            DbHelp.GridSet(gc_QcPart, gv_QcPart, "QC_Part", "검사항목", "100", false, false, false);

            DbHelp.GridColumn_Help(gv_QcPart, "QC_PartNM", "Y");
            RepositoryItemButtonEdit btn_part = (RepositoryItemButtonEdit)gv_QcPart.Columns["QC_PartNM"].ColumnEdit;
            btn_part.Buttons[0].Click += new EventHandler(Part_Click);
            gv_QcPart.Columns["QC_PartNM"].ColumnEdit = btn_part;

            gc_QcPart.DeleteRowEventHandler += new EventHandler(Part_Delete); // 그리드 우클릭 삭제

            gv_QcPart.OptionsView.ShowAutoFilterRow = false;
        }

        #region 내부 함수
        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regQcSet");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Qc.DataSource = ret.ReturnDataSet.Tables[0];

                gv_Qc.BestFitColumns();

                if (gv_Qc.RowCount > 0)
                {
                    gv_Qc_FocusedRowChanged(gv_Qc, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, iFocusRow));
                    gv_Qc.FocusedRowHandle = iFocusRow;
                }

                dt_Part = null;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_Part(int iRow)
        {
            try
            {
                if (dt_Part != null && dt_Part.Select("QC_Code = '" + gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString() + "'").Length > 0)
                {
                    gc_QcPart.DataSource = dt_Part.Select("QC_Code = '" + gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString() + "'").CopyToDataTable();
                }
                else
                {

                    SqlParam sp = new SqlParam("sp_regQcSet");
                    sp.AddParam("Kind", "S");
                    sp.AddParam("Search_D", "D");
                    sp.AddParam("QC_Code", gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString());

                    ret = DbHelp.Proc_Search(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return;
                    }

                    gc_QcPart.DataSource = ret.ReturnDataSet.Tables[0];
                }

                gv_QcPart.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Code_Delete(object sender, EventArgs e)
        {
            int iRow = gv_Qc.FocusedRowHandle;

            try
            {
                SqlParam sp = new SqlParam("sp_regQcSet");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("QC_Code", gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString());

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_Qc.DeleteRow(iRow);

                gv_Qc.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Part_Delete(object sender, EventArgs e)
        {
            int iRow = gv_QcPart.FocusedRowHandle;

            try
            {
                SqlParam sp = new SqlParam("sp_regQcSet");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "D");
                sp.AddParam("QC_Code", gv_QcPart.GetRowCellValue(iRow, "QC_Code").ToString());
                sp.AddParam("QC_Part", gv_QcPart.GetRowCellValue(iRow, "QC_Part").ToString());

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_QcPart.DeleteRow(iRow);

                gv_QcPart.UpdateCurrentRow();

                if(dt_Part != null && dt_Part.Select("QC_Code = '" + gv_QcPart.GetRowCellValue(iRow, "QC_Code").ToString() + "' AND QC_Part = '" + gv_QcPart.GetRowCellValue(iRow, "QC_Part").ToString() + "'").Length > 0)
                {
                    DataRow dr_Delete = dt_Part.Select("QC_Code = '" + gv_QcPart.GetRowCellValue(iRow, "QC_Code").ToString() + "' AND QC_Part = '" + gv_QcPart.GetRowCellValue(iRow, "QC_Part").ToString() + "'")[0];
                    dt_Part.Rows.Remove(dr_Delete);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Click(object sender, EventArgs e) // 수정 필요
        {
            int iRow = gv_Qc.FocusedRowHandle;

            if (iRow < 0)
                return;

            string sColumns = gv_Qc.FocusedColumn.FieldName;

            string sGeneral = "";

            if (sColumns == "QC_KindNM")
                sGeneral = "60011";
            else if (sColumns == "QC_ResultNM")
                sGeneral = "60012";
            else
                sGeneral = "60013";


            if (string.IsNullOrWhiteSpace(gv_Qc.GetRowCellValue(iRow, sColumns).ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("General", "sp_Help_General", sGeneral, gv_Qc.GetRowCellValue(iRow, sColumns).ToString(), "N");

                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Qc.SetRowCellValue(iRow, sColumns.Substring(0, sColumns.Length - 2), Help_Form.sRtCode);
                    gv_Qc.SetRowCellValue(iRow, sColumns, Help_Form.sRtCodeNm);
                }

                gv_Qc.UpdateCurrentRow();
            }
        }

        private void Part_Click(object sender, EventArgs e) // 수정 필요
        {
            int iRow = gv_QcPart.FocusedRowHandle;

            if (iRow < 0)
                return;

            if (string.IsNullOrWhiteSpace(gv_QcPart.GetRowCellValue(iRow, "QC_PartNM").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("General", "sp_Help_General", "60010", gv_QcPart.GetRowCellValue(iRow, "QC_PartNM").ToString(), "N");

                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_QcPart.SetRowCellValue(iRow, "QC_Part", Help_Form.sRtCode);
                    gv_QcPart.SetRowCellValue(iRow, "QC_PartNM", Help_Form.sRtCodeNm);
                }

                gv_QcPart.UpdateCurrentRow();
            }
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string sQC_Code = "", sQC_Name = "", sQC_Kind = "", sQC_Result = "", sQC_Int = "", sQC_Range = "";
                string sQC_CodePart = "", sQC_Part = "";

                for(int i = 0; i < gv_Qc.RowCount; i++)
                {
                    if (!string.IsNullOrWhiteSpace(gv_Qc.GetRowCellValue(i, "QC_Code").ToString()))
                    {
                        sQC_Code += gv_Qc.GetRowCellValue(i, "QC_Code").ToString() + "_/";
                        sQC_Name += gv_Qc.GetRowCellValue(i, "QC_Name").ToString() + "_/";
                        sQC_Kind += gv_Qc.GetRowCellValue(i, "QC_Kind").ToString() + "_/";
                        sQC_Result += gv_Qc.GetRowCellValue(i, "QC_Result").ToString() + "_/";
                        sQC_Int += gv_Qc.GetRowCellValue(i, "QC_Int").ToString() + "_/";
                        sQC_Range += gv_Qc.GetRowCellValue(i, "QC_Range").ToString() + "_/";
                    }
                }

                if(dt_Part != null)
                {
                    for(int i = 0; i < dt_Part.Rows.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(dt_Part.Rows[i]["QC_Part"].ToString()))
                        {
                            sQC_CodePart += dt_Part.Rows[i]["QC_Code"].ToString() + "_/";
                            sQC_Part += dt_Part.Rows[i]["QC_Part"].ToString() + "_/";
                        }
                    }
                }

                SqlParam sp = new SqlParam("sp_regQcSet");
                sp.AddParam("Kind", "I");
                sp.AddParam("QCCode", sQC_Code);
                sp.AddParam("QCName", sQC_Name);
                sp.AddParam("QCKind", sQC_Kind);
                sp.AddParam("QCResult", sQC_Result);
                sp.AddParam("QCInt", sQC_Int);
                sp.AddParam("QCRange", sQC_Range);
                sp.AddParam("QCCodePart", sQC_CodePart);
                sp.AddParam("QCPart", sQC_Part);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                iFocusRow = gv_Qc.FocusedRowHandle;

                Search();

                btn_Save.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();
        }


        #endregion

        #region 그리드 이벤트

        private void gv_Qc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if (gv_Qc.GetDataRow(e.FocusedRowHandle).RowState != DataRowState.Added)
            {
                gv_Qc.Columns["QC_Code"].OptionsColumn.ReadOnly = true;
                gv_Qc.Columns["QC_Code"].OptionsColumn.AllowEdit = false;
            }
            else
            {
                gv_Qc.Columns["QC_Code"].OptionsColumn.ReadOnly = false;
                gv_Qc.Columns["QC_Code"].OptionsColumn.AllowEdit = true;
            }

            Search_Part(e.FocusedRowHandle);
        }

        private void gc_Qc_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if (gv_Qc.FocusedColumn.FieldName == "QC_KindNM")
                    btn_Click(null, null);
                else if (gv_Qc.FocusedColumn.FieldName == "QC_ResultNM")
                    btn_Click(null, null);
                else if (gv_Qc.FocusedColumn.FieldName == "QC_RangeNM")
                    btn_Click(null, null);
            }
        }

        private void gc_QcPart_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if(gv_QcPart.FocusedColumn.FieldName == "QC_PartNM")
                {
                    Part_Click(null, null);
                }
            }
        }

        private void gc_QcPart_NewRowAdd(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv_QcPart.SetRowCellValue(e.RowHandle, "QC_Code", gv_Qc.GetRowCellValue(gv_Qc.FocusedRowHandle, "QC_Code").ToString());
        }

        private void gv_QcPart_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv_QcPart.SetRowCellValue(e.RowHandle, "QC_Code", gv_Qc.GetRowCellValue(gv_Qc.FocusedRowHandle, "QC_Code").ToString());
        }

        private void gv_QcPart_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (dt_Part != null && dt_Part.Select("QC_Code = '" + gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Code").ToString() + "' AND QC_Part = '" + gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Part").ToString() + "'").Length > 0)
            {
                DataRow dr_Delete = dt_Part.Select("QC_Code = '" + gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Code").ToString() + "' AND QC_Part = '" + gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Part").ToString() + "'")[0];
                dt_Part.Rows.Remove(dr_Delete);
            }
        }

        private void gv_QcPart_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "QC_PartNM")
            {
                string sGeneral_Code = gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Part").ToString();
                string sGeneral_Name = "";

                if ((gc_QcPart.DataSource as DataTable).Select("QC_Part = '" + sGeneral_Code + "'").Length > 0)
                {
                    XtraMessageBox.Show("동일한 검사 구분이 등록되어 있습니다");
                    gv_QcPart.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_QcPart_CellValueChanged);
                    gv_QcPart.SetRowCellValue(e.RowHandle, "QC_Part", "");
                    gv_QcPart.SetRowCellValue(e.RowHandle, "QC_PartNM", "");
                    gv_QcPart.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_QcPart_CellValueChanged);
                    return;
                }
                gv_QcPart.UpdateCurrentRow();

                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_General", e.Value.ToString(), "60010");
                if (dr_Help == null)
                {
                    gv_QcPart.SetRowCellValue(e.RowHandle, "QC_Part", "");
                }
                else
                {
                    gv_QcPart.SetRowCellValue(e.RowHandle, "QC_Part", dr_Help["GS_Code"]);
                    gv_QcPart.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_QcPart_CellValueChanged);
                    gv_QcPart.SetRowCellValue(e.RowHandle, "QC_PartNM", dr_Help["GS_Name"]);
                    gv_QcPart.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_QcPart_CellValueChanged);
                }

                string sQC_Code = gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Code").ToString();
                string sQC_Part = gv_QcPart.GetRowCellValue(e.RowHandle, "QC_Part").ToString();

                if (dt_Part != null && dt_Part.Select("QC_Code = '" + sQC_Code + "' AND QC_Part = '" + sQC_Part + "'").Length > 0)
                {
                    DataRow dr = dt_Part.Select("QC_Code = '" + sQC_Code + "' AND QC_Part = '" + sQC_Part + "'")[0];
                    dr.BeginEdit();
                    dr[e.Column.FieldName] = e.Value.ToString();
                    dr.EndEdit();
                }
                else
                {
                    if (dt_Part == null)
                    {
                        dt_Part = (gc_QcPart.DataSource as DataTable).Copy();
                    }
                    else
                    {
                        DataRow dr_Add = dt_Part.NewRow();
                        dr_Add.BeginEdit();
                        dr_Add.ItemArray = gv_QcPart.GetDataRow(e.RowHandle).ItemArray;
                        dr_Add.EndEdit();
                        dt_Part.Rows.Add(dr_Add);
                    }
                }
            }
        }

        private void gv_Qc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "QC_KindNM")
            {
                string sGeneral_Code = gv_Qc.GetRowCellValue(e.RowHandle, "QC_Kind").ToString();
                string sGeneral_Name = "";

                if (string.IsNullOrWhiteSpace(sGeneral_Code))
                {
                    sGeneral_Name = PopHelpForm.Return_Help("sp_Help_General", e.Value.ToString(), "60011");
                    if (!string.IsNullOrWhiteSpace(sGeneral_Name))
                    {
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_Kind", e.Value.ToString());
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_KindNM", sGeneral_Name);
                    }
                }
                else
                {
                    sGeneral_Name = PopHelpForm.Return_Help("sp_Help_General", sGeneral_Code, "60011");
                    if (sGeneral_Name != e.Value.ToString())
                    {
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_Kind", "");
                    }
                }
            }
            else if (e.Column.FieldName == "QC_ResultNM")
            {
                string sGeneral_Code = gv_Qc.GetRowCellValue(e.RowHandle, "QC_Result").ToString();
                string sGeneral_Name = "";

                if (string.IsNullOrWhiteSpace(sGeneral_Code))
                {
                    sGeneral_Name = PopHelpForm.Return_Help("sp_Help_General", e.Value.ToString(), "60012");
                    if (!string.IsNullOrWhiteSpace(sGeneral_Name))
                    {
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_Result", e.Value.ToString());
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_ResultNM", sGeneral_Name);
                    }
                }
                else
                {
                    sGeneral_Name = PopHelpForm.Return_Help("sp_Help_General", sGeneral_Code, "60012");
                    if (sGeneral_Name != e.Value.ToString())
                    {
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_Result", "");
                    }
                }
            }
            else if (e.Column.FieldName == "QC_RangeNM")
            {
                string sGeneral_Code = gv_Qc.GetRowCellValue(e.RowHandle, "QC_Range").ToString();
                string sGeneral_Name = "";

                if (string.IsNullOrWhiteSpace(sGeneral_Code))
                {
                    sGeneral_Name = PopHelpForm.Return_Help("sp_Help_General", e.Value.ToString(), "60013");
                    if (!string.IsNullOrWhiteSpace(sGeneral_Name))
                    {
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_Range", e.Value.ToString());
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_RangeNM", sGeneral_Name);
                    }
                }
                else
                {
                    sGeneral_Name = PopHelpForm.Return_Help("sp_Help_General", sGeneral_Code, "60013");
                    if (sGeneral_Name != e.Value.ToString())
                    {
                        gv_Qc.SetRowCellValue(e.RowHandle, "QC_Range", "");
                    }
                }
            }

            btn_Close.sUpdate = "Y";
            btn_Insert.sUpdate = "Y";
        }

        #endregion
    }
}
