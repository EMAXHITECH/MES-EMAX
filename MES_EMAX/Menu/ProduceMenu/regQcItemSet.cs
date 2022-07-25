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
    public partial class regQcItemSet : BaseReg
    {

        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        DataTable dt_QC = null;

        public regQcItemSet()
        {
            InitializeComponent();
        }

        private void regQcItemSet_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();
        }

        private void Grid_Set()
        {
            gc_Qc.AddRowYN = true;

            DbHelp.GridSet(gc_Qc, gv_Qc, "Item_Code", "품목코드", "100", false, false, false);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Code", "검사항목코드", "100", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Name", "검사항목", "100", false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_KindNM", "검사유형", "100", false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_ResultNM", "결과구분", "100", false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_Int", "소수점", "80", false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "QC_RangeNM", "범위", "80", false, false, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Min_Value", "최솟값", "80", false, true, true);
            DbHelp.GridSet(gc_Qc, gv_Qc, "Max_Value", "최댓값", "80", false, true, true);

            DbHelp.GridColumn_Help(gv_Qc, "QC_Code", "Y");
            RepositoryItemButtonEdit btn_edit = (RepositoryItemButtonEdit)gv_Qc.Columns["QC_Code"].ColumnEdit;
            btn_edit.Buttons[0].Click += new EventHandler(btn_Click);
            gv_Qc.Columns["QC_Code"].ColumnEdit = btn_edit;

            DbHelp.GridColumn_NumSet(gv_Qc, "Min_Value", 3);
            DbHelp.GridColumn_NumSet(gv_Qc, "Min_Value", 3);

            gc_Qc.DeleteRowEventHandler += new EventHandler(QC_Delete); // 그리드 우클릭 삭제

            gv_Qc.OptionsView.ShowAutoFilterRow = false;

            gc_Item.AddRowYN = false;
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Code", "품목코드", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Name", "품목명", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "SSize", "규격", "100", false, false, true);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_BPart", "품목구분", "100", false, false, true);

            gc_Item.PopMenuChk = false;
            gc_Item.MouseWheelChk = false;
        }

        #region 구분 조회
        private void txt_QCPart_EditValueChanged(object sender, EventArgs e)
        {
            txt_QCPartNM.Text = PopHelpForm.Return_Help("sp_Help_General", txt_QCPart.Text, "60010");
        }

        private void txt_QCPart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txt_QCPart_Properties_ButtonClick(sender, null);
        }

        private void txt_QCPart_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("General", "sp_Help_General", "60010", txt_QCPart.Text, "N");

            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                txt_QCPart.Text = HelpForm.sRtCode;
                txt_QCPartNM.Text = HelpForm.sRtCodeNm;
            }
        }
        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_QCPartNM.Text))
                return;


            int iRow = gv_Item.FocusedRowHandle;

            string sItem_Code = gv_Item.GetRowCellValue(iRow, "Item_Code").ToString();

            try
            {
                SqlParam sp = new SqlParam("sp_regQcItemSet");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "A");
                sp.AddParam("Item_Code", sItem_Code);
                sp.AddParam("QC_Part", txt_QCPart.Text);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                //dt_QC = null;
                Search_Q(sItem_Code);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

        }

        private void QC_Delete(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_QCPartNM.Text))
                return;

            try
            {
                int iRow = gv_Qc.FocusedRowHandle;

                string sQC_Code = gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString();
                string sItem_Code = gv_Qc.GetRowCellValue(iRow, "Item_Code").ToString();

                SqlParam sp = new SqlParam("sp_regQcItemSet");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "S");
                sp.AddParam("QC_Code", sQC_Code);
                sp.AddParam("Item_Code", sItem_Code);
                sp.AddParam("QC_Part", txt_QCPart.Text);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                //if(dt_QC != null && dt_QC.Select("QC_Code = '" + sQC_Code + "' AND Item_Code = '" + sItem_Code + "'").Length > 0)
                //{
                //    DataRow dr_Delete = dt_QC.Select("QC_Code = '" + sQC_Code + "' AND Item_Code = '" + sItem_Code + "'")[0];
                //    dt_QC.Rows.Remove(dr_Delete);
                //}

                Search_Q(sItem_Code);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string sQC_Code = "", sItem_Code = "", sMinValue = "", sMaxValue = "";

            //for(int i = 0; i < dt_QC.Rows.Count; i++)
            //{
            //    if (!string.IsNullOrWhiteSpace(dt_QC.Rows[i]["QC_Code"].ToString()))
            //    {
            //        sQC_Code += dt_QC.Rows[i]["QC_Code"].ToString() + "_/";
            //        sItem_Code += dt_QC.Rows[i]["Item_Code"].ToString() + "_/";
            //        sMinValue += dt_QC.Rows[i]["Min_Value"].NumString() + "_/";
            //        sMaxValue += dt_QC.Rows[i]["Max_Value"].NumString() + "_/";
            //    }
            //}

            for(int i = 0; i < gv_Qc.RowCount; i++)
            {
                sQC_Code += gv_Qc.GetRowCellValue(i, "QC_Code").ToString() + "_/";
                sItem_Code += gv_Qc.GetRowCellValue(i, "Item_Code").ToString() + "_/";
                sMinValue += gv_Qc.GetRowCellValue(i, "Min_Value").NumString() + "_/";
                sMaxValue += gv_Qc.GetRowCellValue(i, "Max_Value").NumString() + "_/";
            }

            try
            {
                SqlParam sp = new SqlParam("sp_regQcItemSet");
                sp.AddParam("Kind", "I");
                sp.AddParam("QC_Part", txt_QCPart.Text);
                sp.AddParam("QCCode", sQC_Code);
                sp.AddParam("ItemCode", sItem_Code);
                sp.AddParam("MinValue", sMinValue);
                sp.AddParam("MaxValue", sMaxValue);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                //dt_QC = null;
                Search_Q(gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());

                btn_Save.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_Qc, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        #region 메소드 (저장, 조회)
        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regQcItemSet");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "I");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Item.DataSource = ret.ReturnDataSet.Tables[0];

                gv_Item.BestFitColumns();

                if (gv_Item.RowCount > 0)
                    gv_Item_FocusedRowChanged(gv_Item, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_Q(string sItem_Code)
        {
            try
            {
                //if (dt_QC != null && dt_QC.Select("Item_Code = '" + sItem_Code + "'").Length > 0)
                //{
                //    gc_Qc.DataSource = dt_QC.Select("Item_Code = '" + sItem_Code + "'").CopyToDataTable();
                //}
                //else
                //{

                SqlParam sp = new SqlParam("sp_regQcItemSet");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "D");
                sp.AddParam("Item_Code", sItem_Code);
                sp.AddParam("QC_Part", txt_QCPart.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Qc.DataSource = ret.ReturnDataSet.Tables[0];
                //}

                gv_Item.BestFitColumns();

                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }
        
        private void Search_QC(string sQC_Code, int iRow)
        {
            string sItem_Code = gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString();

            try
            {
                SqlParam sp = new SqlParam("sp_regQcItemSet");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "Q");
                sp.AddParam("QC_Code", sQC_Code);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);

                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ret.ReturnDataSet.Tables[0].Rows[0];

                    gv_Qc.SetRowCellValue(iRow, "Item_Code", sItem_Code);
                    gv_Qc.SetRowCellValue(iRow, "QC_Name", dr["QC_Name"].ToString());
                    gv_Qc.SetRowCellValue(iRow, "QC_KindNM", dr["QC_KindNM"].ToString());
                    gv_Qc.SetRowCellValue(iRow, "QC_ResultNM", dr["QC_ResultNM"].ToString());
                    gv_Qc.SetRowCellValue(iRow, "QC_Int", dr["QC_Int"].ToString());
                    gv_Qc.SetRowCellValue(iRow, "QC_RangeNM", dr["QC_RangeNM"].ToString());
                }
                else
                {
                    gv_Qc.SetRowCellValue(iRow, "Item_Code", "");
                    gv_Qc.SetRowCellValue(iRow, "QC_Name", "");
                    gv_Qc.SetRowCellValue(iRow, "QC_KindNM", "");
                    gv_Qc.SetRowCellValue(iRow, "QC_ResultNM", "");
                    gv_Qc.SetRowCellValue(iRow, "QC_Int", "");
                    gv_Qc.SetRowCellValue(iRow, "QC_RangeNM", "");
                }
                gv_Qc.UpdateCurrentRow();
                gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Click(object sender, EventArgs e) // 수정 필요
        {
            if (string.IsNullOrWhiteSpace(txt_QCPartNM.Text))
            {
                XtraMessageBox.Show("검사 구분을 먼저 입력하세요");
                return;
            }

            int iRow = gv_Qc.FocusedRowHandle;

            if (iRow < 0)
                return;

            if (string.IsNullOrWhiteSpace(gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("QC", "sp_Help_QC", gv_Qc.GetRowCellValue(iRow, "QC_Code").ToString(), "Y");
                Help_Form.Set_Value(txt_QCPart.Text, "", "", "", "");
                Help_Form.sNotReturn = "Y";

                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in Help_Form.drReturn)
                    {
                        gv_Qc.SetRowCellValue(iRow, "QC_Code", row["QC_Code"].ToString());

                        if (iRow + 1 == gv_Qc.RowCount)
                            gv_Qc.AddNewRow();

                        iRow++;

                        gv_Qc.UpdateCurrentRow();
                    }
                    gv_Qc.RowDeleting -= new DevExpress.Data.RowDeletingEventHandler(gv_Qc_RowDeleting);
                    gv_Qc.DeleteRow(iRow);
                    gv_Qc.RowDeleting += new DevExpress.Data.RowDeletingEventHandler(gv_Qc_RowDeleting);
                }
            }
        }


        #endregion

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

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }
        #endregion

        private void gc_Qc_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && gv_Qc.FocusedColumn == gv_Qc.Columns["QC_Code"])
            {
                btn_Click(sender, null);
            }
        }

        private void gv_Qc_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "QC_Code")
            {
                if (!string.IsNullOrWhiteSpace(e.Value.ToString()) && (gc_Qc.DataSource as DataTable).Select("QC_Code = '" + e.Value.ToString() + "'").Length > 0)
                {
                    XtraMessageBox.Show("동일한 검사정보가 이미 등록이 되어있습니다");
                    gv_Qc.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                    gv_Qc.SetRowCellValue(e.RowHandle, "QC_Code", "");
                    gv_Qc.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Qc_CellValueChanged);
                    return;
                }

                Search_QC(e.Value.ToString(), e.RowHandle);
            }

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";

            //string sItem_Code = gv_Qc.GetRowCellValue(e.RowHandle, "Item_Code").ToString();
            //string sQC_Code = gv_Qc.GetRowCellValue(e.RowHandle, "QC_Code").ToString();

            //if (dt_QC != null && dt_QC.Select("QC_Code = '" + sQC_Code + "' AND Item_Code = '" + sItem_Code + "'").Length > 0)
            //{
            //    DataRow dr = dt_QC.Select("QC_Code = '" + sQC_Code + "' AND Item_Code = '" + sItem_Code + "'")[0];
            //    dr.BeginEdit();
            //    dr[e.Column.FieldName] = e.Value.ToString();
            //    dr.EndEdit();
            //}
            //else
            //{
            //    if (dt_QC == null)
            //    {
            //        dt_QC = (gc_Qc.DataSource as DataTable).Copy();
            //    }
            //    else
            //    {
            //        DataRow dr_Add = dt_QC.NewRow();
            //        dr_Add.BeginEdit();
            //        dr_Add.ItemArray = gv_Qc.GetDataRow(e.RowHandle).ItemArray;
            //        dr_Add.EndEdit();
            //        dt_QC.Rows.Add(dr_Add);
            //    }
            //}
        }

        private void gv_Item_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if(btn_Insert.sUpdate == "Y")
            {
                btn_Save.PerformClick();
            }

            Search_Q(gv_Item.GetRowCellValue(e.FocusedRowHandle, "Item_Code").ToString());
        }

        private void txt_QCPartNM_EditValueChanged(object sender, EventArgs e)
        {
            //dt_QC = null;

            if (gv_Item.RowCount > 0)
                gv_Item_FocusedRowChanged(gv_Item, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
        }

        private void gv_Qc_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_QCPartNM.Text))
                return;

            string sQC_Code = gv_Qc.GetRowCellValue(e.RowHandle, "QC_Code").ToString();
            string sItem_Code = gv_Qc.GetRowCellValue(e.RowHandle, "Item_Code").ToString();

            //if (dt_QC != null && dt_QC.Select("QC_Code = '" + sQC_Code + "' AND Item_Code = '" + sItem_Code + "'").Length > 0)
            //{
            //    DataRow dr_Delete = dt_QC.Select("QC_Code = '" + sQC_Code + "' AND Item_Code = '" + sItem_Code + "'")[0];
            //    dt_QC.Rows.Remove(dr_Delete);
            //}
        }
    }
}
