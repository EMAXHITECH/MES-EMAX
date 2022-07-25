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
    public partial class regRouting : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        string sMutiCHK = "Y";

        public regRouting()
        {
            InitializeComponent();
        }

        private void regRouting_Load(object sender, EventArgs e)
        {
            Grid_Set();

            Search();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Code", "품목코드", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_Name", "품목명", "150", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "SSize", "규격", "120", false, false, true, false);
            DbHelp.GridSet(gc_Item, gv_Item, "Item_BPart", "품목구분", "120", false, false, true, false);

            gc_Item.AddRowYN = false;
            gc_Item.PopMenuChk = false;
            gc_Item.MouseWheelChk = false;

            gc_Rout.AddRowYN = true;
            DbHelp.GridSet(gc_Rout, gv_Rout, "Sort_No", "No", "", false, false, false);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Process_Code", "공정코드", "80", false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Process_Name", "공정명", "100", false, false, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Qc_Ck", "공정검사", "100", false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "End_Ck", "완료", "100", false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "MatOut_Ck", "자재출고", "100", false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Custom_Name", "작업처", "80", false, true, true);
            DbHelp.GridSet(gc_Rout, gv_Rout, "Custom_Code", "작업코드", "100", false, false, false);

            DbHelp.GridColumn_CheckBox(gv_Rout, "Qc_Ck");
            DbHelp.GridColumn_CheckBox(gv_Rout, "End_Ck");
            DbHelp.GridColumn_CheckBox(gv_Rout, "MatOut_Ck");

            DbHelp.GridColumn_Help(gv_Rout, "Process_Code", "Y");
            RepositoryItemButtonEdit button_Help_M1 = (RepositoryItemButtonEdit)gv_Rout.Columns["Process_Code"].ColumnEdit;
            button_Help_M1.Buttons[0].Click += new EventHandler(grid_Help);
            gv_Rout.Columns["Process_Code"].ColumnEdit = button_Help_M1;

            DbHelp.GridColumn_Help(gv_Rout, "Custom_Name", "Y");
            RepositoryItemButtonEdit button_Help_Custom = (RepositoryItemButtonEdit)gv_Rout.Columns["Custom_Name"].ColumnEdit;
            button_Help_Custom.Buttons[0].Click += new EventHandler(grid_Help_Custom);
            gv_Rout.Columns["Custom_Name"].ColumnEdit = button_Help_Custom;

            gv_Rout.OptionsView.ShowAutoFilterRow = false;

            gc_Rout.DeleteRowEventHandler += new EventHandler(Delete_D);
        }

        #region 헬프창
        private void grid_Help(object sender, EventArgs e)
        {
            int iRow = gv_Rout.GetFocusedDataSourceRowIndex();

            if (string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Process_Name").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("General", "sp_Help_General", "30030" ,gv_Rout.GetRowCellValue(iRow, "Process_Code").ToString(), sMutiCHK);
                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    if (sMutiCHK == "Y")
                    {
                        foreach (DataRow row in Help_Form.drReturn)
                        {
                            gv_Rout.SetRowCellValue(iRow, "Process_Code", row["GS_Code"].ToString());
                            if (!string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Process_Code").ToString()))
                            {
                                gv_Rout.SetRowCellValue(iRow, "Process_Name", row["GS_Name"].ToString());
                                gv_Rout.SetRowCellValue(iRow, "Sort_No", (iRow + 1).ToString());

                                iRow++;
                                if (iRow == gv_Rout.RowCount)
                                    gv_Rout.AddNewRow();

                                gv_Rout.UpdateCurrentRow();
                            }
                        }

                        if(iRow == gv_Rout.RowCount)
                            gv_Rout.DeleteRow(iRow);
                    }
                    else
                    {
                        gv_Rout.SetRowCellValue(iRow, "Process_Code", Help_Form.sRtCode);
                        if (!string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Process_Code").ToString()))
                        {
                            gv_Rout.SetRowCellValue(iRow, "Process_Name", Help_Form.sRtCodeNm);
                        }
                    }
                }
            }
        }

        private void grid_Help_Custom(object sender, EventArgs e)
        {
            int iRow = gv_Rout.GetFocusedDataSourceRowIndex();

            if (string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Custom_Code").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("Custom", "sp_Help_Custom_Param", gv_Rout.GetRowCellValue(iRow, "Custom_Name").ToString());
                Help_Form.Set_Value("생산,외주", "", "", "", "");
                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Rout.SetRowCellValue(iRow, "Custom_Code", Help_Form.sRtCode);
                    if (!string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(iRow, "Custom_Code").ToString()))
                    {
                        gv_Rout.SetRowCellValue(iRow, "Custom_Name", Help_Form.sRtCodeNm);
                    }
                }
            }
        }

        #endregion

        #region 함수       

        private void Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regRouting");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SH");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Item.DataSource = ret.ReturnDataSet.Tables[0];
                gv_Item.BestFitColumns();

                Item_Search_D(0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        //품목 정보 조회
        private void Item_Search_D(int iRow)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regRouting");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SD");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(iRow, "Item_Code").ToString());

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk == 0)
                {
                    if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                        sMutiCHK = "N";
                    else
                        sMutiCHK = "Y";

                    gc_Rout.DataSource = ret.ReturnDataSet.Tables[0];
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        //에러 체크
        private bool Check_Err()
        {
            if((gc_Rout.DataSource as DataTable).Select("End_Ck = 'Y'").Length < 1)
            {
                XtraMessageBox.Show("완료는 필수 입력 값입니다");
                return false;
            }

            return true;
        }

        //삭제 D
        private void Delete_D(object sender, EventArgs e)
        {
            int iRow = gv_Rout.GetFocusedDataSourceRowIndex();

            try
            {
                SqlParam sp = new SqlParam("sp_regRouting");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "D");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());
                sp.AddParam("Process_Code", gv_Rout.GetRowCellValue(iRow, "Process_Code").ToString());

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Item_Search_D(gv_Item.FocusedRowHandle);

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 상속 함수

        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnCopy()
        {
            btn_Copy.PerformClick();
        }
        #endregion

        #region 그리드 이벤트
        private void gv_Rout_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if(e.Column.FieldName == "Process_Code")
            {
                if (!string.IsNullOrWhiteSpace(e.Value.ToString()) && (gc_Rout.DataSource as DataTable).Select("Process_Code = '" + e.Value.ToString() + "'").Length > 0)
                {
                    XtraMessageBox.Show("동일한 공정이 이미 등록이 되어 있습니다");
                    gv_Rout.SetRowCellValue(e.RowHandle, "Process_Code", "");
                    return;
                }

                string sProcess_Name = PopHelpForm.Return_Help("sp_Help_General", e.Value.ToString(), "30030", "");

                if (!string.IsNullOrWhiteSpace(sProcess_Name))
                    gv_Rout.SetRowCellValue(e.RowHandle, "Process_Name", sProcess_Name);
            }
            else if(e.Column.FieldName == "Custom_Name")
            {
                string sCustom_Code = gv_Rout.GetRowCellValue(e.RowHandle, "Custom_Code").ToString();
                string sCustom_Name = "";

                if (string.IsNullOrWhiteSpace(sCustom_Code))
                {
                    sCustom_Name = PopHelpForm.Return_Help("sp_Help_Custom_Param", e.Value.ToString(), "", "생산,외주");
                    if (!string.IsNullOrWhiteSpace(sCustom_Name))
                    {
                        gv_Rout.SetRowCellValue(e.RowHandle, "Custom_Code", e.Value.ToString());
                        gv_Rout.SetRowCellValue(e.RowHandle, "Custom_Name", sCustom_Name);
                    }
                }
                else
                {
                    sCustom_Name = PopHelpForm.Return_Help("sp_Help_Custom_Param", sCustom_Code, "", "생산,외주");
                    if (sCustom_Name != e.Value.ToString())
                    {
                        gv_Rout.SetRowCellValue(e.RowHandle, "Custom_Code", "");
                    }
                }
            }

            btn_Copy.sUpdate = "Y";
        }

        private void gc_Rout_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_Rout.FocusedColumn == gv_Rout.Columns["Process_Code"])
                {
                    grid_Help(null, null);
                }
                else if(gv_Rout.FocusedColumn == gv_Rout.Columns["Custom_Name"])
                {
                    grid_Help_Custom(null, null);
                }
            }
        }

        private void gc_Rout_NewRowAdd(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            for(int i = e.RowHandle; i < gv_Rout.RowCount; i++)
            {
                gv_Rout.SetRowCellValue(i, "Sort_No", (i + 1).ToString());
            }
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            gv_Rout.AddNewRow();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (gv_Rout.RowCount < 1)
                return;

            if (!Check_Err())
                return;

            try
            {
                string sProcessCode = "", sSortNo = "", sQc_Ck = "", sEnd_Ck = "", sMatOut_Ck = "", sCustom_Code = "";
                
                for(int i = 0; i < gv_Rout.RowCount; i++)
                {
                    if (!string.IsNullOrWhiteSpace(gv_Rout.GetRowCellValue(i, "Process_Name").ToString()))
                    {
                        sProcessCode += gv_Rout.GetRowCellValue(i, "Process_Code").ToString() + "_/";
                        sSortNo += gv_Rout.GetRowCellValue(i, "Sort_No").ToString() + "_/";
                        sQc_Ck += gv_Rout.GetRowCellValue(i, "Qc_Ck").ToString() + "_/";
                        sEnd_Ck += gv_Rout.GetRowCellValue(i, "End_Ck").ToString() + "_/";
                        sMatOut_Ck += gv_Rout.GetRowCellValue(i, "MatOut_Ck").ToString() + "_/";
                        sCustom_Code += gv_Rout.GetRowCellValue(i, "Custom_Code").ToString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regRouting");
                sp.AddParam("Kind", "I");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());
                sp.AddParam("ProcessCode", sProcessCode);
                sp.AddParam("SortNo", sSortNo);
                sp.AddParam("Qc_Ck", sQc_Ck);
                sp.AddParam("End_Ck", sEnd_Ck);
                sp.AddParam("MatOut_Ck", sMatOut_Ck);
                sp.AddParam("Custom_Code", sCustom_Code);

                ret = DbHelp.Proc_Save(sp);
                
                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Item_Search_D(gv_Item.FocusedRowHandle);

                btn_Save.sCHK = "Y";

                btn_Copy.sUpdate = "N";
                btn_Close.sUpdate = "N";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (gv_Item.FocusedRowHandle < 0)
                return;

            try
            {
                SqlParam sp = new SqlParam("sp_regRouting");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("Item_Code", gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString());

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Item_Search_D(gv_Item.FocusedRowHandle);

                btn_Delete.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_Rout, "라우팅 등록");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;
                btn_Save_Click(null, null);
            }

            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        #endregion

        private void gv_Item_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            Item_Search_D(e.FocusedRowHandle);
        }

        private void gv_Rout_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "End_Ck")
            {
                for (int i = 0; i < gv_Rout.RowCount; i++)
                {
                    if (i != e.RowHandle)
                    {
                        gv_Rout.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanging);
                        gv_Rout.SetRowCellValue(i, "End_Ck", "N");
                        gv_Rout.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanging);
                    }
                }
            }
            else if (e.Column.FieldName == "MatOut_Ck")
            {
                for (int i = 0; i < gv_Rout.RowCount; i++)
                {
                    if (i != e.RowHandle)
                    {
                        gv_Rout.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanging);
                        gv_Rout.SetRowCellValue(i, "MatOut_Ck", "N");
                        gv_Rout.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Rout_CellValueChanging);
                    }
                }
            }

            btn_Copy.sUpdate = "Y";
        }

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            if (btn_Copy.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;
                btn_Save_Click(null, null);
            }

            if (gv_Rout.RowCount > 0)
            {
                PopRoutCopyForm Form = new PopRoutCopyForm();
                Form.sItem_Code = gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Code").ToString();
                Form.sItem_Name = gv_Item.GetRowCellValue(gv_Item.FocusedRowHandle, "Item_Name").ToString();
                Form.StartPosition = FormStartPosition.CenterScreen;
                if (Form.ShowDialog() == DialogResult.OK)
                {
                    btn_Copy.sCHK = "Y";
                }
            }
        }
    }
}
