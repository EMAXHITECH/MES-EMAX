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
using DevExpress.XtraGrid.Views.Grid;

namespace MES
{
    public partial class regSearch : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regSearch()
        {
            InitializeComponent();
        }

        private void regSearch_Load(object sender, EventArgs e)
        {
            Grid_Set();

            btn_Select_Click(null, null);
        }

        private void Grid_Set()
        {
            // WorkM 그리드
            DbHelp.GridSet(gc_Search_M, gv_Search_M, "Use_Chk", "선택", "80", false, true, true, true);
            DbHelp.GridSet(gc_Search_M, gv_Search_M, "Help_Code", "코드", "125", false, false, true, true);
            DbHelp.GridSet(gc_Search_M, gv_Search_M, "Help_Name", "명칭", "120", false, false, true, true);

            DbHelp.GridColumn_CheckBox(gv_Search_M, "Use_Chk");
            
            /// 
            DbHelp.GridSet(gc_Search_S, gv_Search_S, "Column_Code", "컬럼 코드", "120", true, true, true, true);
            DbHelp.GridSet(gc_Search_S, gv_Search_S, "Sort_No", "정렬순서", "125", true, true, true, true);
            DbHelp.GridSet(gc_Search_S, gv_Search_S, "Column_Name", "컬럼명", "120", false, true, true, true);
            DbHelp.GridSet(gc_Search_S, gv_Search_S, "Size", "크기", "125", true, true, true, true);
            DbHelp.GridSet(gc_Search_S, gv_Search_S, "Use_Chk", "사용유무", "125", false, true, true, true);
            DbHelp.GridSet(gc_Search_S, gv_Search_S, "Num", "자릿수", "80", true, true, true, true);

            DbHelp.GridColumn_CheckBox(gv_Search_S, "Use_Chk");

            DbHelp.GridColumn_NumSet(gv_Search_S, "Num", 0);

            gc_Search_S.DeleteRowEventHandler += Delete_Row;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regSearch");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_H", "H");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk == 0)
                {
                    gc_Search_M.DataSource = ret.ReturnDataSet.Tables[0];

                    if(gv_Search_M.RowCount > 0)
                    {
                        RowClickEventArgs arg = new RowClickEventArgs(new DevExpress.Utils.DXMouseEventArgs(MouseButtons.Left, 1, 0, 0, 0), 0);
                        gv_Search_M_RowClick(null, arg);
                    }
                }
                else
                {
                    MessageBox.Show(ret.ReturnMessage);
                    return;
                }
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void gv_Search_M_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            try
            {
                SqlParam sp = new SqlParam("sp_regSearch");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_H", "D");
                sp.AddParam("Help_Code", gv_Search_M.GetRowCellValue(e.RowHandle, "Help_Code").ToString());

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    gc_Search_S.DataSource = ret.ReturnDataSet.Tables[0];
                }
                else
                {
                    MessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Showing += Args_Showing;
            args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel };
            DialogResult result = XtraMessageBox.Show(args);

            if (result == DialogResult.Yes)
            {
                gv_Search_M.AddNewRow();
            }
            else if(result == DialogResult.No)
            {
                gv_Search_S.AddNewRow();
            }
        }

        private void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Text = "선택";
            e.Buttons[DialogResult.Yes].Text = "헤더";
            e.Buttons[DialogResult.No].Text = "디테일";
            e.Buttons[DialogResult.Cancel].Text = "취소";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                for(int i = 0; i < gv_Search_M.RowCount; i++)
                {
                    if (gv_Search_M.GetRowCellValue(i, "Use_Chk").ToString() == "Y")
                    {
                        if (gv_Search_M.GetDataRow(i).RowState == DataRowState.Added)
                        {
                            gv_Search_M.DeleteRow(i);
                            i--;
                        }
                        else
                        {
                            SqlParam sp = new SqlParam("sp_regSearch");
                            sp.AddParam("Kind", "D");
                            sp.AddParam("Search_H", "H");
                            sp.AddParam("Help_Code", gv_Search_M.GetRowCellValue(i, "Help_Code").ToString());

                            ret = DbHelp.Proc_Save(sp);

                            if(ret.ReturnChk != 0)
                            {
                                MessageBox.Show(ret.ReturnMessage);
                                return;
                            }
                        }
                    }
                }
                
                int iHRow = gv_Search_M.GetFocusedDataSourceRowIndex();

                for (int i = 0; i < gv_Search_S.RowCount; i++)
                {
                    if (gv_Search_S.IsRowSelected(i))
                    {
                        if (gv_Search_S.GetDataRow(i).RowState == DataRowState.Added)
                        {
                            gv_Search_S.DeleteRow(i);
                            i--;
                        }
                        else
                        {
                            SqlParam sp = new SqlParam("sp_regSearch");
                            sp.AddParam("Kind", "D");
                            sp.AddParam("Search_H", "D");
                            sp.AddParam("Help_Code", gv_Search_M.GetRowCellValue(iHRow, "Help_Code").ToString());
                            sp.AddParam("Column_Code", gv_Search_S.GetRowCellValue(i, "Column_Code").ToString());

                            ret = DbHelp.Proc_Save(sp);

                            if (ret.ReturnChk != 0)
                            {
                                MessageBox.Show(ret.ReturnMessage);
                                return;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
                
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                for(int i = 0; i < gv_Search_M.RowCount; i++)
                {
                    if(gv_Search_M.GetRowCellValue(i, "Use_Chk").ToString() == "Y")
                    {
                        SqlParam sp = new SqlParam("sp_regSearch");
                        sp.AddParam("Kind", "I");
                        sp.AddParam("Search_H", "H");
                        sp.AddParam("Help_Code", gv_Search_M.GetRowCellValue(i, "Help_Code").ToString());
                        sp.AddParam("Help_Name", gv_Search_M.GetRowCellValue(i, "Help_Name").ToString());

                        ret = DbHelp.Proc_Save(sp);

                        if(ret.ReturnChk != 0)
                        {
                            MessageBox.Show(ret.ReturnMessage);
                            return;
                        }
                    }
                }

                int iRow = gv_Search_M.GetFocusedDataSourceRowIndex();
                string sHelp_Code = gv_Search_M.GetRowCellValue(iRow, "Help_Code").ToString();

                for (int i = 0; i < gv_Search_S.RowCount; i++)
                {
                    if(gv_Search_S.GetDataRow(i).RowState == DataRowState.Added || gv_Search_S.GetDataRow(i).RowState == DataRowState.Modified)
                    {
                        SqlParam sp = new SqlParam("sp_regSearch");
                        sp.AddParam("Kind", "I");
                        sp.AddParam("Search_H", "D");
                        sp.AddParam("Help_Code", sHelp_Code);
                        sp.AddParam("Column_Code", gv_Search_S.GetRowCellValue(i, "Column_Code").ToString());
                        sp.AddParam("Column_Name", gv_Search_S.GetRowCellValue(i, "Column_Name").ToString());
                        sp.AddParam("Sort_No", gv_Search_S.GetRowCellValue(i, "Sort_No").ToString());
                        sp.AddParam("Size", gv_Search_S.GetRowCellValue(i, "Size").ToString());
                        sp.AddParam("Use_Chk", gv_Search_S.GetRowCellValue(i, "Use_Chk").ToString());
                        sp.AddParam("Num", gv_Search_S.GetRowCellValue(i, "Num").ToString() == "" ? null : gv_Search_S.GetRowCellValue(i, "Num").ToString());

                        ret = DbHelp.Proc_Save(sp);

                        if(ret.ReturnChk != 0)
                        {
                            MessageBox.Show(ret.ReturnMessage);
                            return;
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Delete_Row(object sender, EventArgs e)
        {
            int iHRow = gv_Search_M.GetFocusedDataSourceRowIndex();
            try
            {
                if (gv_Search_S.IsFocusedView)
                {
                    int iRow = gv_Search_S.GetFocusedDataSourceRowIndex();

                    SqlParam sp = new SqlParam("sp_regSearch");
                    sp.AddParam("Kind", "D");
                    sp.AddParam("Search_H", "D");
                    sp.AddParam("Help_Code", gv_Search_M.GetRowCellValue(iHRow, "Help_Code").ToString());
                    sp.AddParam("Column_Code", gv_Search_S.GetRowCellValue(iRow, "Column_Code").ToString());

                    ret = DbHelp.Proc_Save(sp);

                    if(ret.ReturnChk != 0)
                    {
                        MessageBox.Show(ret.ReturnMessage);
                        return;
                    }
                }
                else
                {
                    SqlParam sp = new SqlParam("sp_regSearch");
                    sp.AddParam("Kind", "D");
                    sp.AddParam("Search_H", "H");
                    sp.AddParam("Help_Code", gv_Search_M.GetRowCellValue(iHRow, "Help_Code").ToString());

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        MessageBox.Show(ret.ReturnMessage);
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            this.btn_Select_Click(null, null);
        }

        private void gv_Search_M_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            if (gv_Search_M.GetDataRow(e.FocusedRowHandle).RowState == DataRowState.Added)
            {
                gv_Search_M.Columns["Help_Code"].OptionsColumn.ReadOnly = false;
                gv_Search_M.Columns["Help_Code"].OptionsColumn.AllowEdit = true;
                gv_Search_M.Columns["Help_Name"].OptionsColumn.ReadOnly = false;
                gv_Search_M.Columns["Help_Name"].OptionsColumn.AllowEdit = true;
            }
            else
            {
                gv_Search_M.Columns["Help_Code"].OptionsColumn.ReadOnly = true;
                gv_Search_M.Columns["Help_Code"].OptionsColumn.AllowEdit = false;
                gv_Search_M.Columns["Help_Name"].OptionsColumn.ReadOnly = true;
                gv_Search_M.Columns["Help_Name"].OptionsColumn.AllowEdit = false;
            }

            gv_Search_M_RowClick(sender, new RowClickEventArgs(new DevExpress.Utils.DXMouseEventArgs(MouseButtons.Left, 1, 0, 0, 0), e.FocusedRowHandle));
        }

        #region 상속 함수

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        #endregion
    }
}
