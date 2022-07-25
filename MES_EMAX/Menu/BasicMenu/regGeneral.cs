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
using DevExpress.XtraEditors.Controls;

namespace MES
{
    public partial class regGeneral : BaseReg
    {
        int old_count = 0;  // 행 추가시 이전 행의 수와 비교를 위한 변수
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regGeneral()
        {
            InitializeComponent();
        }

        private void regGeneral_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Param();
            btnSelect_Click(null, null);
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_GeneralH, gv_GeneralH, "GM_Code", "분류코드", "80", false, false, true);
            DbHelp.GridSet(gc_GeneralH, gv_GeneralH, "GM_Name", "분류명", "120", false, false, true);
            DbHelp.GridSet(gc_GeneralH, gv_GeneralH, "GM_Remark", "비고", "200", false, false, true);
            DbHelp.GridSet(gc_GeneralH, gv_GeneralH, "View_Ck", "뷰체크", "80", false, false, false);

            DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "GM_Code", "분류코드", "80", false, false, false);
            DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "GS_Code", "공통코드", "80", false, true, true);
            DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "GS_Name", "공통명", "120", false, true, true);
            DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "Use_Ck", "사용여부", "80", false, true, true);
            DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "Def_Ck", "기본값", "80", false, true, true);

            gc_GeneralH.PopMenuChk = false;
            gc_GeneralH.MouseWheelChk = false;
            gc_GeneralD.DeleteRowEventHandler += btnDelete_Click;

            RepositoryItemCheckEdit Check_Edit = gc_GeneralD.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            Check_Edit.ValueChecked = "Y";
            Check_Edit.ValueGrayed = "N";
            Check_Edit.ValueUnchecked = "N";
            Check_Edit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;

            gv_GeneralH.Columns["View_Ck"].ColumnEdit = Check_Edit;
            gv_GeneralH.OptionsSelection.MultiSelect = true;
            gv_GeneralD.Columns["Use_Ck"].ColumnEdit = Check_Edit;
            gv_GeneralD.OptionsSelection.MultiSelect = true;

            RepositoryItemTextEdit Text_Edit_1 = gc_GeneralD.RepositoryItems.Add("TextEdit") as RepositoryItemTextEdit;
            Text_Edit_1.MaxLength = 5;
            gv_GeneralD.Columns["GS_Code"].ColumnEdit = Text_Edit_1;

            RepositoryItemTextEdit Text_Edit_2 = gc_GeneralD.RepositoryItems.Add("TextEdit") as RepositoryItemTextEdit;
            Text_Edit_2.MaxLength = 50;
            gv_GeneralD.Columns["GS_Name"].ColumnEdit = Text_Edit_2;

            DbHelp.GridColumn_CheckBox(gv_GeneralD, "Def_Ck");
            RepositoryItemCheckEdit Check_Edit_2 = gv_GeneralD.Columns["Def_Ck"].ColumnEdit as RepositoryItemCheckEdit;
            Check_Edit_2.EditValueChanged += Default_Change;
        }

        #region 버튼 이벤트
        

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regGeneral");
                sp.AddParam("Kind", "S");
                ret = DbHelp.Proc_Search(sp);
                ds = ret.ReturnDataSet;

                DataTable Master = DbHelp.Fill_Table(ds.Tables[0]);

                gc_GeneralH.DataSource = Master;
                gc_GeneralH.RefreshDataSource();
                gv_GeneralH.BestFitColumns();
            }
            catch (Exception)
            {

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            gv_GeneralD.AddNewRow();
            gv_GeneralD.UpdateCurrentRow();
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int handle = gv_GeneralD.FocusedRowHandle;
                if (handle >= 0)
                {
                    string m_code = gv_GeneralH.GetDataRow(gv_GeneralH.FocusedRowHandle)["GM_Code"].ToString();
                    string s_code = gv_GeneralD.GetDataRow(handle)["GS_Code"].ToString();
                    
                    if(string.IsNullOrWhiteSpace(s_code))
                    {
                        old_count--;
                        gv_GeneralD.DeleteRow(handle);
                        gv_GeneralD.UpdateCurrentRow();
                    }
                    else
                    {
                        SqlParam sp = new SqlParam("sp_regGeneral");
                        sp.AddParam("Kind", "D");
                        sp.AddParam("GM_Code", m_code);
                        sp.AddParam("GS_Code", s_code);

                        ret = DbHelp.Proc_Save(sp);
                        old_count--;
                        if (ret.ReturnChk != 0)
                        {
                            XtraMessageBox.Show(ret.ReturnMessage);
                            return;
                        }

                        btn_Delete.sCHK = "Y";

                        int now_focus = gv_GeneralH.FocusedRowHandle;

                        btnSelect_Click(null, null);
                        gv_GeneralH.FocusedRowHandle = now_focus;

                        Search_Detail();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable add_table = (gc_GeneralD.DataSource as DataTable).Clone();
                DataTable modified_table = (gc_GeneralD.DataSource as DataTable).Clone();
                string m_code = gv_GeneralH.GetFocusedRowCellValue("GM_Code").ToString();

                for (int i = 0; i < gv_GeneralD.RowCount; i++)
                {
                    DataRow row = gv_GeneralD.GetDataRow(i);
                    if (row.RowState == DataRowState.Added)
                    {
                        if (!string.IsNullOrWhiteSpace(row["GS_Code"].ToString().Replace(" ", "")))
                            add_table.ImportRow(row);
                    }
                    else if (row.RowState == DataRowState.Modified)
                        modified_table.ImportRow(gv_GeneralD.GetDataRow(i));
                }

                foreach (DataRow row in add_table.Rows)
                {
                    SqlParam sp = new SqlParam("sp_regGeneral");
                    sp.AddParam("Kind", "I");
                    sp.AddParam("GM_Code", m_code);
                    sp.AddParam("GS_Code", row["GS_Code"].ToString());
                    sp.AddParam("GS_Name", row["GS_Name"].ToString());
                    sp.AddParam("Use_Ck", row["Use_Ck"].ToString());
                    sp.AddParam("Def_Ck", row["Def_Ck"].ToString());
                    for(int i = 5; i < gv_GeneralD.Columns.Count; i++)
                    {
                        if (i == 5)
                        {
                            if (gv_GeneralD.Columns["Param_Ck"].Visible)
                                sp.AddParam("Param_Ck", row["Param_Ck"].ToString());
                        }
                        else
                        {
                            if(gv_GeneralD.Columns["Param_Ck" + (i - 5).ToString()].Visible)
                                sp.AddParam("Param_Ck" + (i - 5).ToString(), row["Param_Ck" + (i - 5).ToString()].ToString());
                        }
                    }

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return;
                    }
                }

                foreach (DataRow row in modified_table.Rows)
                {
                    SqlParam sp = new SqlParam("sp_regGeneral");
                    sp.AddParam("Kind", "U");
                    sp.AddParam("GM_Code", m_code);
                    sp.AddParam("GS_Code", row["GS_Code"].ToString());
                    sp.AddParam("GS_Name", row["GS_Name"].ToString());
                    sp.AddParam("Use_Ck", row["Use_Ck"].ToString());
                    sp.AddParam("Def_Ck", row["Def_Ck"].ToString());
                    for (int i = 5; i < gv_GeneralD.Columns.Count; i++)
                    {
                        if (i == 5)
                        {
                            if (gv_GeneralD.Columns["Param_Ck"].Visible)
                                sp.AddParam("Param_Ck", row["Param_Ck"].ToString());
                        }
                        else
                        {
                            if (gv_GeneralD.Columns["Param_Ck" + (i - 5).ToString()].Visible)
                                sp.AddParam("Param_Ck" + (i - 5).ToString(), row["Param_Ck" + (i - 5).ToString()].ToString());
                        }
                    }

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return;
                    }
                }
                btn_Save.sCHK = "Y";
                //XtraMessageBox.Show("저장되었습니다.");

                int now_focus = gv_GeneralH.FocusedRowHandle;

                btnSelect_Click(null, null);
                gv_GeneralH.FocusedRowHandle = now_focus;

                Search_Detail();
            }
            catch (Exception)
            {

            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_GeneralH, "표준코드등록");
        }
        
        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        #region 그리드 이벤트
        private void gv_GeneralH_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Search_Detail();
            }
            catch (Exception)
            {

            }
        }

        private void gv_GeneralD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                string m_code = gv_GeneralD.GetFocusedRowCellValue("GM_Code").ToString();

                if(!string.IsNullOrWhiteSpace(m_code))
                {
                    gv_GeneralD.Columns["GS_Code"].OptionsColumn.AllowEdit = false;
                    gv_GeneralD.Columns["GS_Code"].OptionsColumn.ReadOnly = true;
                }
                else
                {
                    gv_GeneralD.Columns["GS_Code"].OptionsColumn.AllowEdit = true;
                    gv_GeneralD.Columns["GS_Code"].OptionsColumn.ReadOnly = false;
                }
            }
            catch (Exception)
            {

            }
        }

        private void gv_GeneralD_RowCountChanged(object sender, EventArgs e)
        {
            if (old_count < gv_GeneralD.RowCount)
            {
                gv_GeneralD.SetRowCellValue(gv_GeneralD.RowCount - 1, "Use_Ck", "Y");
                gv_GeneralD.UpdateCurrentRow();
                old_count = gv_GeneralD.RowCount;
            }
        }

        private void Default_Change(object sender, EventArgs e)
        {
            gv_GeneralD.Columns["Def_Ck"].ColumnEdit.EditValueChanged -= Default_Change;

            for (int i = 0; i < gv_GeneralD.RowCount; i++)
            {
                if (i != gv_GeneralD.FocusedRowHandle)
                    gv_GeneralD.SetRowCellValue(i, "Def_Ck", "N");
            }

            gv_GeneralD.Columns["Def_Ck"].ColumnEdit.EditValueChanged += Default_Change;
        }
        #endregion

        private void Search_Detail()
        {
            try
            {
                string m_code = gv_GeneralH.GetFocusedRowCellValue("GM_Code").ToString();

                //칼럼명 설정
                DataRow dr_Name = ds.Tables[0].Select("GM_Code = '" + m_code + "'")[0];

                for (int i = 5; i < gv_GeneralD.Columns.Count; i++)
                {
                    if (i == 5)
                    {
                        gv_GeneralD.Columns[i].Caption = dr_Name["Param_Name"].ToString();

                        if (string.IsNullOrWhiteSpace(dr_Name["Param_Name"].ToString()))
                            gv_GeneralD.Columns[i].Visible = false;
                        else
                            gv_GeneralD.Columns[i].VisibleIndex = i + 1; //gv_GeneralD.Columns[i].Visible = true;
                    }
                    else
                    {
                        gv_GeneralD.Columns[i].Caption = dr_Name["Param_Name" + (i - 5).ToString()].ToString();

                        if (string.IsNullOrWhiteSpace(dr_Name["Param_Name" + (i - 5).ToString()].ToString()))
                            gv_GeneralD.Columns[i].Visible = false;
                        else
                            gv_GeneralD.Columns[i].VisibleIndex = i + 1; //gv_GeneralD.Columns[i].Visible = true;
                    }
                }

                DataRow[] rows = ds.Tables[1].Select("GM_Code = '" + m_code + "'");
                DataTable table = ds.Tables[1].Clone();
                if (rows.Count() > 0)
                    table = rows.CopyToDataTable();

                old_count = table.Rows.Count;
                gc_GeneralD.DataSource = table;
                gc_GeneralD.RefreshDataSource();
                gv_GeneralD.BestFitColumns();
            }
            catch(Exception)
            {

            }
        }

        private void Search_Param()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regGeneral");
                sp.AddParam("Kind", "S");

                ret = DbHelp.Proc_Search(sp);

                DataTable dt_Param = ret.ReturnDataSet.Tables[0];

                for(int i = 0; i < dt_Param.Columns.Count - 4; i++)
                {
                    if(i > 0)
                        DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "Param_Ck" + i.ToString(), "", "80", false, true, true);
                    else
                        DbHelp.GridSet(gc_GeneralD, gv_GeneralD, "Param_Ck", "", "80", false, true, true);
                }

                gv_GeneralD.UpdateCurrentRow();
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

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
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
    }
}
