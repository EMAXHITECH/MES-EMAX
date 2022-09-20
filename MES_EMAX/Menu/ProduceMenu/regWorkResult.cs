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
    public partial class regWorkResult : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();

        private string sWIdx_No = "", sWork_Sort = "";

        public regWorkResult()
        {
            InitializeComponent();
        }

        private void regWorkResult_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();

            dt_ResultDate.Focus();

            txt_CompanyCode.Text = Comp_Def();

            btn_Close.sUpdate = "N";
            btn_Insert.sUpdate = "N";
        }

        private void Grid_Set()
        {
            //작업자
            gc_User.AddRowYN = true;
            gc_User.PopMenuChk = true;
            gc_User.MouseWheelChk = true;

            gv_User.OptionsView.ShowAutoFilterRow = false;
            gv_User.OptionsCustomization.AllowSort = false;

            DbHelp.GridSet(gc_User, gv_User, "User_Name", "작업자", "100", false, true, true);
            DbHelp.GridSet(gc_User, gv_User, "User_Code", "작업자코드", "100", false, false, true);

            DbHelp.GridColumn_Help(gv_User, "User_Name", "Y");
            RepositoryItemButtonEdit btn_User = (RepositoryItemButtonEdit)gv_User.Columns["User_Name"].ColumnEdit;
            btn_User.Buttons[0].Click += new EventHandler(User_Help);
            gv_User.Columns["User_Name"].ColumnEdit = btn_User;

            gc_User.DeleteRowEventHandler += new EventHandler(User_Delete);

        }

        #region 내부 함수
        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWorkResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");
                sp.AddParam("Result_No", txt_ResultNo.Text);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                DataTable dt_User = ret.ReturnDataSet.Tables[1];

                if(ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ret.ReturnDataSet.Tables[0].Rows[0];

                    sWIdx_No = dr["WIdx_No"].ToString();
                    sWork_Sort = dr["Work_Sort"].ToString();

                    dt_ResultDate.Text = dr["Result_Date"].ToString();
                    txt_CompanyCode.Text = dr["Company_Code"].ToString();
                    txt_WorkSheet.Text = dr["Key_No"].ToString();
                    txt_Item_Code.Text = dr["Item_Code"].ToString();
                    txt_Item_Name.Text = dr["Item_Name"].ToString();
                    txt_Ssize.Text = dr["SSize"].ToString();

                    txt_ProcessCode.Text = dr["Process_Code"].ToString();
                    txt_CustomCode.Text = dr["Custom_Code"].ToString();

                    dt_STime.Text = dr["S_Time"].ToString();
                    dt_ETime.Text = dr["E_Time"].ToString();

                    txt_GoodQty.EditValueChanged -= txt_GoodQty_EditValueChanged;
                    txt_BadQty.EditValueChanged -= txt_BadQty_EditValueChanged;
                    txt_WorkQty.Text = dr["Work_Qty"].NumString();
                    txt_Qty.Text = dr["Qty"].NumString();
                    txt_GoodQty.Text = dr["Good_Qty"].NumString();
                    txt_BadQty.Text = dr["Bad_Qty"].NumString();

                    txt_GoodQty.EditValueChanged += txt_GoodQty_EditValueChanged;
                    txt_BadQty.EditValueChanged += txt_BadQty_EditValueChanged;

                    txt_RegUser.Text = dr["Reg_User"].ToString();
                    txt_RegDate.Text = dr["Reg_Date"].ToString();
                    txt_UpUser.Text = dr["Up_User"].ToString();
                    txt_UpDate.Text = dr["Up_Date"].ToString();
                }
                else
                {
                    txt_GoodQty.EditValueChanged -= txt_GoodQty_EditValueChanged;
                    txt_BadQty.EditValueChanged -= txt_BadQty_EditValueChanged;
                    DbHelp.Clear_Panel(panel_H);
                    DbHelp.Clear_Panel(panel_M);
                    txt_GoodQty.EditValueChanged += txt_GoodQty_EditValueChanged;
                    txt_BadQty.EditValueChanged += txt_BadQty_EditValueChanged;

                    dt_STime.Text = "";
                    dt_ETime.Text = "";
                    sWIdx_No = "";
                    sWork_Sort = "";
                }

                gc_User.DataSource = dt_User;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        private void User_Help(object sender, EventArgs e)
        {
            int iRow = gv_User.FocusedRowHandle;

            if (iRow < 0)
                return;

            if (string.IsNullOrWhiteSpace(gv_User.GetRowCellValue(iRow, "User_Code").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("User", "sp_Help_KioUser", "", "Y");
                Help_Form.Set_Value(txt_CustomCode.Text, "", "", "", "");
                Help_Form.sNotReturn = "Y";
                if(Help_Form.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in Help_Form.drReturn)
                    {
                        gv_User.SetRowCellValue(iRow, "User_Code", row["User_Code"]);
                        gv_User.SetRowCellValue(iRow, "User_Name", row["User_Name"]);

                        if (gv_User.RowCount == iRow + 1)
                            gv_User.AddNewRow();

                        iRow++;

                        gv_User.UpdateCurrentRow();
                    }

                    gv_User.DeleteRow(iRow);
                }
            }
        }

        private void Search_WorkSheet()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWorkResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "W");
                sp.AddParam("WIdx_No", sWIdx_No);
                sp.AddParam("Work_Sort", sWork_Sort);
                sp.AddParam("Key_No", txt_WorkSheet.Text);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                DataTable dt_Need;

                if (ret.ReturnDataSet.Tables.Count > 1)
                    dt_Need = ret.ReturnDataSet.Tables[1];
                else
                    dt_Need = null;

                if(ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ret.ReturnDataSet.Tables[0].Rows[0];

                    txt_Item_Code.Text = dr["Item_Code"].ToString();
                    txt_Item_Name.Text = dr["Item_Name"].ToString();
                    txt_Ssize.Text = dr["SSize"].ToString();
                    txt_ProcessCode.Text = dr["Process_Code"].ToString();
                    txt_CustomCode.Text = dr["Custom_Code"].ToString();
                    txt_WorkQty.Text = dr["Work_Qty"].ToString();
                }
                else
                {
                    sWIdx_No = "";
                    sWork_Sort = "";
                    txt_Item_Code.Text = "";
                    txt_Item_Name.Text = "";
                    txt_Ssize.Text = "";
                    txt_ProcessCode.Text = "";
                    txt_CustomCode.Text = "";
                    txt_WorkQty.Text = "";
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private string Comp_Def()
        {
            string sComp_Code = "";

            try
            {
                SqlParam sp = new SqlParam("sp_regWorkResult");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "CD");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return sComp_Code;
                }

                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    sComp_Code = ret.ReturnDataSet.Tables[0].Rows[0]["Company_Code"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return sComp_Code;
            }

            return sComp_Code;
        } 

        private bool Check_Err()
        {
            if (txt_ResultNo.Enabled)
            {
                if (txt_ResultNo.Text == "")
                {
                    XtraMessageBox.Show("실적번호는 필수 입력값입니다");
                    return false;
                }
            }

            if (dt_ResultDate == null)
            {
                XtraMessageBox.Show("실적일자는 필수 입력값입니다");
                return false;
            }

            if (txt_Item_Code.Text == "")
            {
                XtraMessageBox.Show("작지번호는 필수 입력값입니다");
                return false;
            }

            if (dt_STime.Text == "")
            {
                XtraMessageBox.Show("작업시작 시간은 필수 입력값입니다");
                return false;
            }

            if (dt_ETime.Text == "")
            {
                XtraMessageBox.Show("작업종료 시간은 필수 입력값입니다");
                return false;
            }

            return true;
        }
        private void User_Delete(object sender, EventArgs e)
        {
            int iRow = gv_User.FocusedRowHandle;

            try
            {
                SqlParam sp = new SqlParam("sp_regWorkResult");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "U");
                sp.AddParam("Result_No", txt_ResultNo.Text);
                sp.AddParam("User_Code", gv_User.GetRowCellValue(iRow, "User_Code").ToString());

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_User.DeleteRow(iRow);

                gv_User.UpdateCurrentRow();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("Result", "sp_Help_Result", "", "N");
            HelpForm.Set_Value(this.Name, "", "", "", "");
            HelpForm.sLevelYN = "Y";
            HelpForm.sNotReturn = "Y";
            btn_Select.clsWait.CloseWait();
            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                btn_Select.clsWait.ShowWait(this.FindForm());
                txt_ResultNo.Text = HelpForm.sRtCode;
                Search_Data();

                dt_ResultDate.Focus();

                txt_CompanyCode.Text = Comp_Def();

                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;

                btn_Save_Click(null, null);
            }

            txt_ResultNo.Text = "";

            Search_Data();

            dt_ResultDate.Focus();

            txt_CompanyCode.Text = Comp_Def();

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Check_Err())
                return;

            try
            {
                string sUser_Code = "";

                for(int i = 0; i < gv_User.RowCount; i++)
                {
                    if(!string.IsNullOrWhiteSpace(gv_User.GetRowCellValue(i, "User_Code").ToString()))
                    {
                        sUser_Code += gv_User.GetRowCellValue(i, "User_Code").ToString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regWorkResult");
                sp.AddParam("Kind", "I");
                sp.AddParam("Result_No", txt_ResultNo.Text);
                sp.AddParam("Result_Sort", "1");
                sp.AddParam("Result_Date", dt_ResultDate.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("WIdx_No", sWIdx_No);
                sp.AddParam("Work_Sort", sWork_Sort);
                sp.AddParam("Item_Code", txt_Item_Code.Text);
                sp.AddParam("Company_Code", txt_CompanyCode.Text);
                sp.AddParam("Process_Code", txt_ProcessCode.Text);
                sp.AddParam("Custom_Code", txt_CustomCode.Text);

                sp.AddParam("S_Time", dt_STime.DateTime.ToString("yyyy-MM-dd HH:mm"));
                sp.AddParam("E_Time", dt_ETime.DateTime.ToString("yyyy-MM-dd HH:mm"));

                sp.AddParam("Qty", txt_Qty.Text);
                sp.AddParam("Good_Qty", txt_GoodQty.Text);
                sp.AddParam("Bad_Qty", txt_BadQty.Text);

                sp.AddParam("UserCode", sUser_Code);

                sp.AddParam("Reg_User", GlobalValue.sUserID);
                sp.AddParam("Form_Name", this.Name);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                txt_ResultNo.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Result_No"].ToString();

                Search_Data();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }

            btn_Save.sCHK = "Y";
            btn_Close.sUpdate = "N";
            btn_Insert.sUpdate = "N";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWorkResult");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("Result_No", txt_ResultNo.Text);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                txt_ResultNo.Text = "";

                Search_Data();

                dt_ResultDate.Focus();

                txt_CompanyCode.Text = Comp_Def();

                btn_Delete.sCHK = "Y";
                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_User, this.Name);
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

        #region 텍스트 이벤트

        private void txt_CompanyCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_CompanyName.Text = PopHelpForm.Return_Help("sp_Help_Company", txt_CompanyCode.Text);
        }

        private void txt_CompanyCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_CompanyCode_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_CompanyCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CompanyName.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Company", "sp_Help_Company", txt_CompanyCode.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_CompanyCode.Text = HelpForm.sRtCode;
                    txt_CompanyName.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        private void txt_WorkSheet_EditValueChanged(object sender, EventArgs e)
        {
            Search_WorkSheet();
        }

        private void txt_WorkSheet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txt_WorkSheet_Properties_ButtonClick(null, null);
            }
        }

        private void txt_WorkSheet_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Item_Code.Text))
            {
                PopHelpForm Helpform = new PopHelpForm("WorkSheet", "sp_Help_WorkSheet", "", txt_WorkSheet.Text, "N");
                Helpform.sNotReturn = "Y";
                Helpform.sLevelYN = "Y";
                if (Helpform.ShowDialog() == DialogResult.OK)
                {
                    sWIdx_No = Helpform.sRtCode.Split('/')[0];
                    sWork_Sort = Helpform.sRtCode.Split('/')[1];
                    txt_WorkSheet.Text = Helpform.sRtCodeNm;
                    Search_WorkSheet();
                }
            }
        }

        private void txt_ProcessCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_ProcessName.Text = PopHelpForm.Return_Help("sp_Help_General", txt_ProcessCode.Text, "30030");
        }

        private void txt_CustomCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_CustomName.Text = PopHelpForm.Return_Help("sp_Help_Custom_Param", txt_CustomCode.Text);
        }

        
        private void txt_GoodQty_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Item_Code.Text))
            {
                XtraMessageBox.Show("작업지시를 먼저 선택하세요");
                txt_GoodQty.EditValueChanged -= txt_GoodQty_EditValueChanged;
                txt_GoodQty.Text = "";
                txt_GoodQty.EditValueChanged += txt_GoodQty_EditValueChanged;
                return;
            }

            decimal dGood_Qty = decimal.Parse(txt_GoodQty.Text.NumString());
            decimal dWork_Qty = decimal.Parse(txt_WorkQty.Text.NumString());
            decimal dBad_Qty = decimal.Parse(txt_BadQty.Text.NumString());

            if(dGood_Qty > dWork_Qty)
            {
                XtraMessageBox.Show("지시수량보다 많을 수 없습니다.");

                txt_GoodQty.EditValueChanged -= txt_GoodQty_EditValueChanged;
                txt_GoodQty.Text = dWork_Qty.ToString();
                txt_Qty.Text = (dWork_Qty + dBad_Qty).ToString();
                txt_GoodQty.EditValueChanged += txt_GoodQty_EditValueChanged;

                txt_BadQty.Focus();
            }
            else
            {
                txt_Qty.Text = (dGood_Qty + dBad_Qty).ToString();
            }
        }

        private void txt_BadQty_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Item_Code.Text))
            {
                XtraMessageBox.Show("작업지시를 먼저 선택하세요");
                txt_BadQty.EditValueChanged -= txt_BadQty_EditValueChanged;
                txt_BadQty.Text = "";
                txt_BadQty.EditValueChanged += txt_BadQty_EditValueChanged;
                return;
            }

            decimal dGood_Qty = decimal.Parse(txt_GoodQty.Text.NumString());
            decimal dBad_Qty = decimal.Parse(txt_BadQty.Text.NumString());

            txt_Qty.Text = (dGood_Qty + dBad_Qty).ToString();
        }

        private void txt_ResultNo_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ResultNo.Text))
                txt_WorkSheet.Enabled = true;
            else
                txt_WorkSheet.Enabled = false;
        }

        #endregion

        #region 그리드 이벤트
       
        private void gc_User_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if(gv_User.FocusedColumn.FieldName == "User_Name")
                {
                    User_Help(null, null);
                }
            }
        }

        private void gv_User_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "User_Name")
            {
                string sUser_Name = "";
                string sUser_Code = gv_User.GetRowCellValue(e.RowHandle, "User_Code").ToString();

                if ((gc_User.DataSource as DataTable).Select("User_Code = '" + sUser_Code + "'").Length > 0)
                {
                    XtraMessageBox.Show("동일한 작업자가 등록되어 있습니다");
                    gv_User.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_User_CellValueChanged);
                    gv_User.SetRowCellValue(e.RowHandle, "User_Code", "");
                    gv_User.SetRowCellValue(e.RowHandle, "User_Name", "");
                    gv_User.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_User_CellValueChanged);
                    return;
                }

                if (string.IsNullOrWhiteSpace(sUser_Code))
                {
                    sUser_Name = PopHelpForm.Return_Help("sp_Help_KioUser", e.Value.ToString(), "", txt_CustomCode.Text);
                    if (!string.IsNullOrWhiteSpace(sUser_Name))
                    {
                        gv_User.SetRowCellValue(e.RowHandle, "User_Code", e.Value.ToString());
                        gv_User.SetRowCellValue(e.RowHandle, "User_Name", sUser_Name);
                    }
                }
                else
                {
                    sUser_Name = PopHelpForm.Return_Help("sp_Help_KioUser", sUser_Code, "", txt_CustomCode.Text);
                    if(sUser_Name != e.Value.ToString())
                    {
                        gv_User.SetRowCellValue(e.RowHandle, "User_Code", "");
                    }
                }
            }
        }
        #endregion
    }
}
