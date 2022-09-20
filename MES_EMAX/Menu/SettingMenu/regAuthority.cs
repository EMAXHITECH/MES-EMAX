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
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace MES
{
    public partial class regAuthority : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regAuthority()
        {
            InitializeComponent();
        }

        private void regAuthority_Load(object sender, EventArgs e)
        {
            Grid_Set();
            LookUp_Set();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_User, View_User, "User_Code", "사원ID", "100", false, false, true);
            DbHelp.GridSet(Grid_User, View_User, "User_Name", "사원명", "100", false, false, true);
            DbHelp.GridSet(Grid_User, View_User, "User_Pos", "직급코드", "100", false, false, false);
            DbHelp.GridSet(Grid_User, View_User, "Pos_Name", "직급", "100", false, false, true);
            DbHelp.GridSet(Grid_User, View_User, "In_Date", "입사일자", "100", false, false, false);
            DbHelp.GridSet(Grid_User, View_User, "Mobile_No", "휴대폰번호", "100", false, false, false);
            DbHelp.GridSet(Grid_User, View_User, "E_Mail", "이메일", "100", false, false, false);

            RepositoryItemTextEdit text_edit = Grid_User.RepositoryItems.Add("TextEdit") as RepositoryItemTextEdit;
            text_edit.Mask.EditMask = "(\\(\\d\\d\\d\\) )?\\d{1,3}-\\d{1,4}-\\d{1,4}";
            text_edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            text_edit.Mask.UseMaskAsDisplayFormat = true;
            text_edit.MaxLength = 13;

            DbHelp.GridSet(Grid_Menu_M, View_Menu_M, "Menu_MCode", "분류명", "100", false, false, true);
            DbHelp.GridSet(Grid_Menu_M, View_Menu_M, "Menu_MName", "분류명", "100", false, false, true);

            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "Menu_SCode", "메뉴코드", "80", false, false, false);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "Menu_SName", "메뉴명", "80", false, false, true);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "Form_Name", "폼명", "80", false, true, false);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "IP_CK", "입력체크", "80", false, true, true);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "PT_CK", "프린트체크", "80", false, true, true);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "SE_CK", "검색체크", "80", false, true, true);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "DT_CK", "삭제체크", "80", false, true, true);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "SE_Level", "권한레벨코드", "80", false, true, false);
            DbHelp.GridSet(Grid_Menu_S, View_Menu_S, "SE_LevelNM", "권한레벨", "80", false, true, true);

            RepositoryItemCheckEdit edit_Chk = new RepositoryItemCheckEdit();
            edit_Chk.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            edit_Chk.ValueChecked = "1";
            edit_Chk.ValueUnchecked = "0";

            View_Menu_S.Columns["IP_CK"].ColumnEdit = edit_Chk;
            View_Menu_S.Columns["PT_CK"].ColumnEdit = edit_Chk;
            View_Menu_S.Columns["SE_CK"].ColumnEdit = edit_Chk;
            View_Menu_S.Columns["DT_CK"].ColumnEdit = edit_Chk;

            

            Grid_User.MouseWheelChk = false;
            Grid_User.PopMenuChk = false;
            View_User.OptionsCustomization.AllowSort = false;
            View_User.OptionsView.ShowAutoFilterRow = false;

            Grid_Menu_M.MouseWheelChk = false;
            Grid_Menu_M.PopMenuChk = false;
            View_Menu_M.OptionsCustomization.AllowSort = false;
            View_Menu_M.OptionsView.ShowAutoFilterRow = false;

            Grid_Menu_S.MouseWheelChk = false;
            Grid_Menu_S.PopMenuChk = false;
            View_Menu_S.OptionsCustomization.AllowSort = false;
            View_Menu_S.OptionsView.ShowAutoFilterRow = false;

        }

        private void LookUp_Set()
        {
            RepositoryItemLookUpEdit edit_look = new RepositoryItemLookUpEdit();
            SqlParam sp = new SqlParam("sp_regAuthority");
            sp.AddParam("Kind", "B");

            if (DbHelp.Proc_Search(sp).ReturnChk == 0)
            {
                DataTable table = DbHelp.Fill_Table(DbHelp.Proc_Search(sp).ReturnDataSet.Tables[0]);

                edit_look.ValueMember = "CODE";
                edit_look.DisplayMember = "NAME";
                edit_look.NullText = "미정";
                edit_look.Columns.Add(new LookUpColumnInfo("NAME", "레벨"));
                edit_look.DataSource = table;
                edit_look.EditValueChanged += new EventHandler(Grid_LookUp_ValueChanged);

                View_Menu_S.Columns["SE_LevelNM"].ColumnEdit = edit_look;
            }
        }

        private void Grid_LookUp_ValueChanged(object sender, EventArgs e)
        {
            string val = Convert.ToString(((ChangingEventArgs)e).NewValue);

            View_Menu_S.SetFocusedRowCellValue("SE_Level", val);
        }


        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            int n1 = View_User.FocusedRowHandle;
            int n2 = View_Menu_M.FocusedRowHandle;

            Save_Data();

            View_User.FocusedRowHandle = n1;
            View_User.UnselectRow(0);
            View_User.SelectRow(n1);

            View_Menu_M.FocusedRowHandle = n2;
            View_Menu_M.UnselectRow(0);
            View_Menu_M.SelectRow(n2);
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(Grid_Menu_S, "권한관리");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        #region 데이터 이벤트
        private void Search_Data(string part = "")
        {
            if (string.IsNullOrWhiteSpace(txt_DeptNM.Text))
                return;

            if (string.IsNullOrWhiteSpace(part) || part == "M")
            {
                SqlParam sp = new SqlParam("sp_regAuthority");
                sp.AddParam("Kind", "S");
                sp.AddParam("Part", "M");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    Grid_Menu_M.DataSource = ret.ReturnDataSet.Tables[0];
                    Grid_Menu_M.RefreshDataSource();
                    View_Menu_M.BestFitColumns();
                }
            }

            if (string.IsNullOrWhiteSpace(part) || part == "L")
            {
                SqlParam sp = new SqlParam("sp_regAuthority");
                sp.AddParam("Kind", "S");
                sp.AddParam("Part", "L");
                sp.AddParam("Dept_Code", txt_DeptCode.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    Grid_User.DataSource = ret.ReturnDataSet.Tables[0];
                    Grid_User.RefreshDataSource();
                    View_User.BestFitColumns();
                }
            }

            else if (part == "R")
            {
                SqlParam sp = new SqlParam("sp_regAuthority");
                sp.AddParam("Kind", "S");
                sp.AddParam("Part", "R");
                sp.AddParam("User_Code", Convert.ToString(View_User.GetFocusedRowCellValue("User_Code")));

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    bool exists = false;

                    foreach (DataTable temp in ds.Tables)
                    {
                        if (temp.TableName == View_User.GetFocusedRowCellValue("User_Code").NullString()) // DataSet에 해당 유저 권한 관리 DataTable 기억여부
                            exists = true;
                    }

                    if (!exists)        // DataSet에 해당 유저 권한 관리 DataTable 존재하지 않을 시
                    {
                        DataTable table = ret.ReturnDataSet.Tables[0].Copy();

                        table.TableName = View_User.GetFocusedRowCellValue("User_Code").NullString();
                        ds.Tables.Add(table);
                    }
                    if ((ret.ReturnDataSet.Tables[0]).Rows.Count == 1)
                    {
                        Grid_Menu_S.DataSource = ret.ReturnDataSet.Tables[0].Clone();
                        Grid_Menu_S.RefreshDataSource();
                    }
                }
            }
                
            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
                return;
            }
        }

        private void Save_Data()
        {
            foreach (DataTable table in ds.Tables)
            {
                DataTable Changed = table.GetChanges(DataRowState.Added | DataRowState.Modified);

                if (Changed != null)
                {
                    DataRow row = DbHelp.Summary_Data_2(Changed, "Menu_SCode", new string[] { "User_Code", "Menu_SCode", "IP_CK", "PT_CK", "SE_CK", "DT_CK", "SE_Level" });

                    SqlParam sp = new SqlParam("sp_regAuthority");

                    sp.AddParam("Kind", "I");
                    sp.AddParam("User_Code", table.TableName);
                    sp.AddParam("Menu_SCode", row["Menu_SCode"].NullString());
                    sp.AddParam("IP_CK", row["IP_CK"].NullString());
                    sp.AddParam("PT_CK", row["PT_CK"].NullString());
                    sp.AddParam("SE_CK", row["SE_CK"].NullString());
                    sp.AddParam("DT_CK", row["DT_CK"].NullString());
                    sp.AddParam("SE_Level", row["SE_Level"].NullString());

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return;
                    }
                }
            }

            btn_Save.sCHK = "Y";
            btn_Select_Click(null, null);
            Search_Data("R");
        }
        #endregion

        #region 버튼 상속
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
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


        protected override void btnCopy()
        {
            btn_Copy.PerformClick();
        }
        #endregion

        #region 그리드 이벤트

        // 사용자별 권한 조회
        private void View_User_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (View_Menu_M.RowCount > 0)
            {
                Search_Data("R"); // 유저 메뉴 권한 조회

                string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                string user_code = View_User.GetFocusedRowCellValue("User_Code").NullString();

                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName == user_code)
                    {
                        if (table.Select("Menu_MCode = '" + m_code + "'").Count() > 0)
                            Grid_Menu_S.DataSource = table.Select("Menu_MCode = '" + m_code + "'").CopyToDataTable();
                        else
                            Grid_Menu_S.DataSource = table.Clone();
                        Grid_Menu_S.RefreshDataSource();
                        View_Menu_S.BestFitColumns();
                    }
                }
            }
        }

        // 사용자의 메뉴별 권한 조회
        private void View_Menu_M_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (View_User.RowCount > 0)
            {
                View_Menu_S.UpdateSummary();

                string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                string user_code = View_User.GetFocusedRowCellValue("User_Code").NullString();
                foreach (DataTable table in ds.Tables)
                {
                    if (table.TableName == user_code)
                    {
                        if (table.Select("Menu_MCode = '" + m_code + "'").Count() > 0)
                            Grid_Menu_S.DataSource = table.Select("Menu_MCode = '" + m_code + "'").CopyToDataTable();
                        else
                            Grid_Menu_S.DataSource = table.Clone();
                        Grid_Menu_S.RefreshDataSource();
                        View_Menu_S.BestFitColumns();
                    }
                }
            }
        }

        // 중분류 메뉴명 컬러 변경
        private void View_Menu_S_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            string f_name = Convert.ToString(View_Menu_S.GetRowCellValue(e.RowHandle, "Form_Name"));
            if (string.IsNullOrWhiteSpace(f_name))
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        // 입력 권한 체크시 중분류 전체 권한 변경 반영
        private void View_Menu_S_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var val = Convert.ToString(e.Value);
            string user_code = View_User.GetFocusedRowCellValue("User_Code").NullString();

            if (e.Column.FieldName.Contains("_CK"))
            {
                string f_name = Convert.ToString(View_Menu_S.GetRowCellValue(e.RowHandle, "Form_Name"));

                if (string.IsNullOrWhiteSpace(f_name))      // 중분류 변경시
                {
                    for (int i = e.RowHandle; i < View_Menu_S.RowCount; i++)
                    {
                        string f_name_2 = Convert.ToString(View_Menu_S.GetRowCellValue(i, "Form_Name"));
                        if (i != e.RowHandle && string.IsNullOrWhiteSpace(f_name_2)) // 그 다음 중분류 직전까지 값 변경 반영
                            break;
                        else
                        {
                            View_Menu_S.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(View_Menu_S_CellValueChanging);
                            View_Menu_S.SetRowCellValue(i, e.Column, val);
                            View_Menu_S.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(View_Menu_S_CellValueChanging);

                            foreach (DataTable table in ds.Tables)
                            {
                                if (table.TableName == user_code)
                                {
                                    string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                                    string s_code = View_Menu_S.GetRowCellValue(i, "Menu_SCode").NullString();
                                    DataRow row = table.Select("Menu_MCode = '" + m_code + "' AND Menu_SCode = '" + s_code + "'")[0];
                                    int n = table.Rows.IndexOf(row);

                                    table.Rows[n][e.Column.FieldName] = val;
                                }
                            }
                        }
                    }
                    if (e.Column.FieldName == "IP_CK")
                    {
                        for (int i = e.RowHandle; i < View_Menu_S.RowCount; i++)
                        {
                            string f_name_2 = Convert.ToString(View_Menu_S.GetRowCellValue(i, "Form_Name"));
                            if (i != e.RowHandle && string.IsNullOrWhiteSpace(f_name_2))
                                break;
                            View_Menu_S.SetRowCellValue(i, "PT_CK", val);
                            View_Menu_S.SetRowCellValue(i, "SE_CK", val);
                            View_Menu_S.SetRowCellValue(i, "DT_CK", val);

                            foreach (DataTable table in ds.Tables)
                            {
                                if (table.TableName == user_code)
                                {
                                    string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                                    string s_code = View_Menu_S.GetRowCellValue(i, "Menu_SCode").NullString();
                                    DataRow row = table.Select("Menu_MCode = '" + m_code + "' AND Menu_SCode = '" + s_code + "'")[0];
                                    int n = table.Rows.IndexOf(row);

                                    table.Rows[n]["PT_CK"] = val;
                                    table.Rows[n]["SE_CK"] = val;
                                    table.Rows[n]["DT_CK"] = val;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (e.Column.FieldName == "IP_CK")
                    {
                        View_Menu_S.SetFocusedRowCellValue("PT_CK", val);
                        View_Menu_S.SetFocusedRowCellValue("SE_CK", val);
                        View_Menu_S.SetFocusedRowCellValue("DT_CK", val);

                        foreach (DataTable table in ds.Tables)
                        {
                            if (table.TableName == user_code)
                            {
                                string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                                string s_code = View_Menu_S.GetFocusedRowCellValue("Menu_SCode").NullString();
                                DataRow row = table.Select("Menu_MCode = '" + m_code + "' AND Menu_SCode = '" + s_code + "'")[0];
                                int n = table.Rows.IndexOf(row);

                                table.Rows[n]["PT_CK"] = val;
                                table.Rows[n]["SE_CK"] = val;
                                table.Rows[n]["DT_CK"] = val;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 부서 이벤트
        private void txt_DeptCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_DeptNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Dept", "sp_Help_Dept", txt_DeptCode.Text, "N");

                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_DeptCode.Text = HelpForm.sRtCode;
                    txt_DeptNM.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        private void txt_DeptCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_DeptCode_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_DeptCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_DeptNM.Text = PopHelpForm.Return_Help("sp_Help_Dept", txt_DeptCode.Text);

            ds = new DataSet();
            btn_Select_Click(null, null);
            Search_Data("R");
        }
        #endregion

        #region 그리드 헤더 클릭 및 연계 이벤트

        // 그리드 입력 헤더 더블 클릭시 All Check / All Uncheck 반영
        private void View_Menu_S_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs mouse = e as DXMouseEventArgs;

            GridHitInfo hInfo = ((GridView)sender).CalcHitInfo(mouse.Location);

            if (hInfo.InColumn && hInfo.Column.FieldName == "IP_CK")
            {
                string val = "0";

                for (int i = 0; i < View_Menu_S.RowCount; i++)
                {
                    if (View_Menu_S.GetRowCellValue(i, "IP_CK").NullString() != "1")        // 입력 체크박스에 하나라도 체크가 안 되어있으면 val = "1"
                    {
                        val = "1";
                    }
                }

                View_Menu_S.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.View_Menu_S_CellValueChanged);
                View_Menu_S.CellValueChanging -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.View_Menu_S_CellValueChanging);

                // DataTable의 컬럼 및 그리드의 모든 체크 박스에 값 반영, 
                for (int i = 0; i < View_Menu_S.RowCount; i++)
                {
                    View_Menu_S.SetRowCellValue(i, "PT_CK", val);
                    View_Menu_S.SetRowCellValue(i, "SE_CK", val);
                    View_Menu_S.SetRowCellValue(i, "DT_CK", val);

                    string user_code = View_User.GetFocusedRowCellValue("User_Code").NullString();
                    string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                    string s_code = View_Menu_S.GetRowCellValue(i, "Menu_SCode").NullString();

                    foreach (DataTable table in ds.Tables)
                    {
                        if (table.TableName == user_code) // 해당 유저의 권한관리 DataTable
                        {
                            DataRow row = table.Select("Menu_MCode = '" + m_code + "' AND Menu_SCode = '" + s_code + "'")[0];
                            int n = table.Rows.IndexOf(row);

                            table.Rows[n]["IP_CK"] = val;
                            table.Rows[n]["PT_CK"] = val;
                            table.Rows[n]["SE_CK"] = val;
                            table.Rows[n]["DT_CK"] = val;
                        }
                    }
                }

                View_Menu_S.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.View_Menu_S_CellValueChanged);
                View_Menu_S.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.View_Menu_S_CellValueChanging);
            }
        }

        // 입력 이외의 헤더 클릭시 DataSet 안의 DataTable에 변경된 값 반영
        private void View_Menu_S_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var val = Convert.ToString(e.Value);
            string user_code = View_User.GetFocusedRowCellValue("User_Code").NullString();

            foreach (DataTable table in ds.Tables)
            {
                if (table.TableName == user_code)
                {
                    string m_code = View_Menu_M.GetFocusedRowCellValue("Menu_MCode").NullString();
                    string s_code = View_Menu_S.GetRowCellValue(e.RowHandle, "Menu_SCode").NullString();

                    DataRow row = table.Select("Menu_MCode = '" + m_code + "' AND Menu_SCode = '" + s_code + "'")[0];
                    int n = table.Rows.IndexOf(row);

                    table.Rows[n][e.Column.FieldName] = val;
                }
            }
        }
        #endregion

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            if (View_User.RowCount > 0)
            {
                PopAuthoriyCopyForm Form = new PopAuthoriyCopyForm();
                Form.sUser_Code = View_User.GetRowCellValue(View_User.FocusedRowHandle, "User_Code").ToString();
                Form.sUser_Name = View_User.GetRowCellValue(View_User.FocusedRowHandle, "User_Name").ToString();
                Form.StartPosition = FormStartPosition.CenterScreen;
                if (Form.ShowDialog() == DialogResult.OK)
                {
                    btn_Copy.sCHK = "Y";
                    ds = new DataSet();
                    btn_Select_Click(null, null);
                    Search_Data("R");
                }
            }
        }
    }
}
