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
    public partial class regMenu : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regMenu()
        {
            InitializeComponent();
        }

        private void regMenu_Load(object sender, EventArgs e)
        {
            Grid_Set();

            btn_Select_Click(null, null);
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Menu_S, gv_Menu_S, "Org_Code", "메뉴코드", "125", false, true, false, true);
            DbHelp.GridSet(gc_Menu_S, gv_Menu_S, "Menu_SCode", "메뉴코드", "125", false, true, true, true);
            DbHelp.GridSet(gc_Menu_S, gv_Menu_S, "Menu_SName", "메뉴명", "120", false, true, true, true);
            DbHelp.GridSet(gc_Menu_S, gv_Menu_S, "Sort_No", "정렬", "120", true, true, true, true);
            DbHelp.GridSet(gc_Menu_S, gv_Menu_S, "Form_Name", "Form_Name", "150", false, true, true, true);
            DbHelp.GridSet(gc_Menu_S, gv_Menu_S, "Use_Ck", "사용유무", "120", false, true, true, true);

            DbHelp.GridColumn_CheckBox(gv_Menu_S, "Use_Ck");

            gc_Menu_S.DeleteRowEventHandler += Delete_Row;

            gc_Menu_S.PopMenuChk = true;
            gc_Menu_S.MouseWheelChk = true;
            gv_Menu_S.OptionsView.ShowAutoFilterRow = false;
        }

        private void txt_MenuM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.txt_MenuM_Properties_ButtonClick(null, null);
            }
        }

        private void txt_MenuM_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           if(txt_MenuM_NM.Text == "")
            {
                PopHelpForm helpForm = new PopHelpForm("Menu", "sp_Help_MenuM", txt_MenuM.Text);

                if(helpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_MenuM.Text = helpForm.sRtCode;
                    txt_MenuM_NM.Text = helpForm.sRtCodeNm;

                    btn_Select_Click(null, null);
                }
            }
        }

        private void txt_MenuM_EditValueChanged(object sender, EventArgs e)
        {
            txt_MenuM_NM.Text = PopHelpForm.Return_Help("sp_Help_MenuM", txt_MenuM.Text);

            Search_Data();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regMenu");
                sp.AddParam("Kind", "S");
                sp.AddParam("Menu_MCode", txt_MenuM.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    gc_Menu_S.DataSource = ret.ReturnDataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (txt_MenuM_NM.Text == "")
                return;

            gv_Menu_S.AddNewRow();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_MenuM_NM.Text == "")
                return;

            try
            {
                for(int i = 0; i < gv_Menu_S.RowCount; i++)
                {
                    if(gv_Menu_S.GetDataRow(i).RowState == DataRowState.Added || gv_Menu_S.GetDataRow(i).RowState == DataRowState.Modified)
                    {
                        if (gv_Menu_S.GetRowCellValue(i, "Org_Code").NullString() == "0" || gv_Menu_S.GetRowCellValue(i, "Menu_SCode").NullString() == "0")
                            continue;

                        SqlParam sp = new SqlParam("sp_regMenu");
                        sp.AddParam("Kind", "I");
                        sp.AddParam("Menu_MCode", txt_MenuM.Text);
                        sp.AddParam("Org_Code", gv_Menu_S.GetRowCellValue(i, "Org_Code").ToString());
                        sp.AddParam("Menu_SCode", gv_Menu_S.GetRowCellValue(i, "Menu_SCode").ToString());
                        sp.AddParam("Menu_SName", gv_Menu_S.GetRowCellValue(i, "Menu_SName").ToString());
                        sp.AddParam("Sort_No", gv_Menu_S.GetRowCellValue(i, "Sort_No").ToString());
                        sp.AddParam("Form_Name", gv_Menu_S.GetRowCellValue(i, "Form_Name").ToString());
                        sp.AddParam("UseCk", gv_Menu_S.GetRowCellValue(i, "Use_Ck").ToString());

                        ret = DbHelp.Proc_Save(sp);
                        
                        if(ret.ReturnChk != 0)
                        {
                            XtraMessageBox.Show(ret.ReturnMessage);
                            return;
                        }
                    }
                }

                Search_Data();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Delete_Data();
        }

        private void Delete_Row(object sender, EventArgs e)
        {
            Delete_Data();
        }

        private void Delete_Data()
        {
            if (gv_Menu_S.RowCount < 1)
                return;

            try
            {
                foreach (int Index in gv_Menu_S.GetSelectedRows())
                {
                    SqlParam sp = new SqlParam("sp_regMenu");
                    sp.AddParam("Kind", "D");
                    sp.AddParam("Menu_MCode", txt_MenuM.Text);
                    sp.AddParam("Org_Code", gv_Menu_S.GetRowCellValue(Index, "Org_Code").ToString());

                    ret = DbHelp.Proc_Save(sp);

                    if (ret.ReturnChk != 0)
                    {
                        XtraMessageBox.Show(ret.ReturnMessage);
                        return;
                    }
                }

                Search_Data();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_Menu_S, "메뉴설정");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
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

        private void gv_Menu_S_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            string f_name = Convert.ToString(gv_Menu_S.GetRowCellValue(e.RowHandle, "Form_Name"));
            if (string.IsNullOrWhiteSpace(f_name))
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }
    }
}
