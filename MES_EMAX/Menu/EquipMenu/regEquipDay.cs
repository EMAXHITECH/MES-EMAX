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
    public partial class regEquipDay : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regEquipDay()
        {
            InitializeComponent();
        }

        private void regEquipDay_Load(object sender, EventArgs e)
        {
            dt_Equip.Text = DateTime.Now.ToString("yyyy-MM-dd");

            Grid_Set();

            Search();

            dt_Equip.Focus();
        }

        private void Grid_Set()
        {
            gc_Equip.AddRowYN = true;
            DbHelp.GridSet(gc_Equip, gv_Equip, "Equip_Code", "설비코드", "130", false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Equip_Name", "설비명", "100", false, false, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Start_Ck", "가동시작", "80", false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Start_Date", "시작시간", "100", false, false, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "End_Ck", "가동완료", "80", false, true, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "End_Date", "완료시간", "100", false, false, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Tot_Date", "총가동시간(분)", "100", false, false, true);
            DbHelp.GridSet(gc_Equip, gv_Equip, "Equip_Memo", "비고", "150", false, true, true);

            DbHelp.GridColumn_Help(gv_Equip, "Equip_Code", "Y");
            RepositoryItemButtonEdit button_Help_M1 = (RepositoryItemButtonEdit)gv_Equip.Columns["Equip_Code"].ColumnEdit;
            button_Help_M1.Buttons[0].Click += new EventHandler(grid_Equip_Help);
            gv_Equip.Columns["Equip_Code"].ColumnEdit = button_Help_M1;

            DbHelp.GridColumn_CheckBox(gv_Equip, "Start_Ck");
            DbHelp.GridColumn_CheckBox(gv_Equip, "End_Ck");

            gv_Equip.OptionsView.ShowAutoFilterRow = false;

            gc_Equip.DeleteRowEventHandler += new EventHandler(Delete_D);
        }
      
        #region 함수

        //구매처 Help 함수
        private void grid_Equip_Help(object sender, EventArgs e)
        {
            int iRow = gv_Equip.GetFocusedDataSourceRowIndex();

            PopHelpForm HelpForm = new PopHelpForm("Equip", "sp_Help_Equip", gv_Equip.GetRowCellValue(iRow, "Equip_Code").ToString(), "Y");
            HelpForm.sNotReturn = "Y";
            if(HelpForm.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow row in HelpForm.drReturn)
                {
                    gv_Equip.SetRowCellValue(iRow, "Equip_Code", row["Equip_Code"].ToString());
                    if (!string.IsNullOrWhiteSpace(gv_Equip.GetRowCellValue(iRow, "Equip_Code").ToString()))
                    {
                        gv_Equip.SetRowCellValue(iRow, "Equip_Name", row["Equip_Name"].ToString());
                        gv_Equip.UpdateCurrentRow();
                    }

                    if (iRow + 1 == gv_Equip.RowCount)
                        gv_Equip.AddNewRow();

                    iRow++;

                    gv_Equip.UpdateCurrentRow();
                }

                gv_Equip.DeleteRow(iRow);
            }
        }

        //그리드 품목 조회
        private void Search()
        {
            if(dt_Equip.Text == "")
            {
                XtraMessageBox.Show("가동일자를 입력해주세요");
                return;
            }

            try
            {
                SqlParam sp = new SqlParam("sp_regEquipDay");
                sp.AddParam("Kind", "S");
                sp.AddParam("Date", dt_Equip.DateTime.ToString("yyyyMMdd"));

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Equip.DataSource = ret.ReturnDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

      
        //삭제 D
        private void Delete_D(object sender, EventArgs e)
        {
            int iRow = gv_Equip.GetFocusedDataSourceRowIndex();

            try
            {
                SqlParam sp = new SqlParam("sp_regEquipDay");
                sp.AddParam("Kind", "D");
                sp.AddParam("Date", dt_Equip.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("Equip_Code", gv_Equip.GetRowCellValue(iRow, "Equip_Code").ToString());
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Search();
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
        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            gv_Equip.AddNewRow();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (gv_Equip.RowCount < 1)
                return;

            if(dt_Equip.Text == "")
            {
                XtraMessageBox.Show("가동일자는 필수 입력값입니다");
                return;
            }

            try
            {
                string sEquipCode = "", sStart_Ck = "", sEnd_Ck = "", sEquipMemo = "";
                
                for(int i = 0; i < gv_Equip.RowCount; i++)
                {
                    if (gv_Equip.GetDataRow(i).RowState != DataRowState.Unchanged)
                    {
                        if (!string.IsNullOrWhiteSpace(gv_Equip.GetRowCellValue(i, "Equip_Code").ToString()))
                        {
                            sEquipCode += gv_Equip.GetRowCellValue(i, "Equip_Code").ToString() + "_/";
                            sStart_Ck += gv_Equip.GetRowCellValue(i, "Start_Ck").ToString() + "_/";
                            sEnd_Ck += gv_Equip.GetRowCellValue(i, "End_Ck").ToString() + "_/";
                            sEquipMemo += gv_Equip.GetRowCellValue(i, "Equip_Memo").ToString() + "_/";
                        }
                    }
                }

                SqlParam sp = new SqlParam("sp_regEquipDay");
                sp.AddParam("Kind", "I");
                sp.AddParam("Date", dt_Equip.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("EquipCode", sEquipCode);
                sp.AddParam("StartCk", sStart_Ck);
                sp.AddParam("EndCk", sEnd_Ck);
                sp.AddParam("EquipMemo", sEquipMemo);
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Save(sp);
                
                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

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
            try
            {
                Delete_D(null, null);

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
            FileIF.Excel_Down(gc_Equip, "설비가동 일지등록");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (btn_Close.Result_Update == DialogResult.Yes)
            {
                btn_Save_Click(null, null);
            }

            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        #region 그리드 이벤트

        private void gv_Equip_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "Equip_Code")
            {
                string sEquipName = PopHelpForm.Return_Help("sp_Help_Equip", e.Value.ToString());
                if (!string.IsNullOrWhiteSpace(sEquipName))
                {
                    gv_Equip.SetRowCellValue(e.RowHandle, "Equip_Name", sEquipName);
                }
            }
        }

        private void gc_Equip_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if (gv_Equip.FocusedColumn.FieldName == "Equip_Code")
                {
                    grid_Equip_Help(sender, null);
                }
            }
        }

        #endregion
    }
}
