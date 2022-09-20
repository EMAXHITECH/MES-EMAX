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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace MES
{
    public partial class regSalePlanTr : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        //영업담당자 부서코드
        private string sDeptCode = "";

        private string sSearch = "N";

        //DateEdit 첫 포커스에서 변경시 Changed 함수가 먹혀서 체크
        private string sLoad = "";

        public regSalePlanTr()
        {
            InitializeComponent();
        }

        private void regSalePlanTr_Load(object sender, EventArgs e)
        {
            Grid_Set();

            Search_Grid();

            txt_UserCode.Text = GlobalValue.sUserID;

            ForMat.sBasic_Set(this.Name, txt_OrderNo);

            sLoad = "Y";

            txt_Comp.Text = Comp_Def();

            dt_OrderDate.Focus();

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }

        private void Grid_Set()
        {
            //Quot_S 그리드
            gc_OrderS.AddRowYN = true;
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Sort_No", "순번", "", false, false, false, true);
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Item_Code", "품목코드", "120", false, true, true, true);
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Item_Name", "품목명", "150", false, false, true, true);
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Qty", "Qty", "80", true, true, true, true);
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Q_Unit", "단위", "50", false, false, true, true);
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Delivery", "예상납기일", "110", false, true, true, true);
            DbHelp.GridSet(gc_OrderS, gv_OrderS, "Order_Bigo", "비고", "150", false, true, true, true);

            DbHelp.GridColumn_Help(gv_OrderS, "Item_Code", "Y");
            RepositoryItemButtonEdit button_Help = (RepositoryItemButtonEdit) gv_OrderS.Columns["Item_Code"].ColumnEdit;
            button_Help.Buttons[0].Click += new EventHandler(grid_S_Help);
            gv_OrderS.Columns["Item_Code"].ColumnEdit = button_Help;

            RepositoryItemMemoEdit Memo_Spec = new RepositoryItemMemoEdit();
            gv_OrderS.Columns["Order_Bigo"].ColumnEdit = Memo_Spec;

            DbHelp.GridColumn_Data(gv_OrderS, "Delivery");

            DbHelp.GridColumn_NumSet(gv_OrderS, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            gv_OrderS.OptionsView.RowAutoHeight = true;// MemoEdit 때문에 행 높이 자동 설정
            gv_OrderS.OptionsView.ShowAutoFilterRow = false;

            gc_OrderS.DeleteRowEventHandler += new EventHandler(S_Delete_Row);
        }

        #region 함수
        private void Search_Grid()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regSalePlanTr");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");
                sp.AddParam("Order_No", txt_OrderNo.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt_S = ret.ReturnDataSet.Tables[1];

                    if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = DbHelp.Fill_Table(ret.ReturnDataSet.Tables[0]);
                        DataRow dr = dt.Rows[0];

                        //값 넣기
                        dt_OrderDate.Text = dr["Order_Date"].ToString();

                        //사업장 기본값 가져오는 부분 수정
                        txt_Comp.EditValueChanged -= txt_Comp_EditValueChanged;
                        txt_Comp.Text = dr["Company_Code"].ToString();
                        txt_Comp.EditValueChanged += txt_Comp_EditValueChanged;
                        txt_CompNM.Text = dr["Company_Name"].ToString();

                        txt_UserCode.EditValueChanged -= txt_UserCode_EditValueChanged;
                        txt_UserCode.Text = dr["User_Code"].ToString();
                        txt_UserCode.EditValueChanged += txt_UserCode_EditValueChanged;
                        txt_UserCodeNM.Text = dr["User_Name"].ToString();
                        txt_DeptCode.Text = dr["Dept_Name"].ToString();
                        sDeptCode = dr["Dept_Code"].ToString();
                        
                        memo_Order.Text = dr["Order_Memo"].ToString();

                        txt_Project_Title.Text = dr["Project_Title"].ToString();

                        txt_RegDate.Text = dr["Reg_Date"].ToString();
                        txt_RegUser.Text = dr["Reg_User"].ToString();
                        txt_UpDate.Text = dr["Up_Date"].ToString();
                        txt_UpUser.Text = dr["Up_User"].ToString();
                    }
                    else
                    {
                        DbHelp.Clear_Panel(panel_H);
                        DbHelp.Clear_Panel(panel_M);
                    }

                    gc_OrderS.DataSource = dt_S;

                    btn_Insert.sUpdate = "N";
                    btn_Close.sUpdate = "N";
                }
                else
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
            }
            catch (Exception ex)
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
                SqlParam sp = new SqlParam("sp_regSalePlanTr");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "CD");

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return sComp_Code;
                }

                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                    sComp_Code = ret.ReturnDataSet.Tables[0].Rows[0]["Company_Code"].ToString();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return sComp_Code;
            }

            return sComp_Code;
        }

        private bool Check_Err()
        {
            if (txt_OrderNo.Enabled)
            {
                if (txt_OrderNo.Text == "")
                {
                    XtraMessageBox.Show("계획번호는 필수 입력값입니다");
                    return false;
                }
            }

            if (dt_OrderDate == null)
            {
                XtraMessageBox.Show("계획일자는 필수 입력값입니다");
                return false;
            }

            if (txt_UserCodeNM.Text == "")
            {
                XtraMessageBox.Show("담당자는 필수 입력값입니다");
                return false;
            }

            if (txt_CompNM.Text == "")
            {
                XtraMessageBox.Show("사업장은 필수 입력값입니다");
                return false;
            }

            return true;
        }

        #endregion

        #region 컨트롤 Help

        //영업 담당자
        private void txt_UserCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_UserCodeNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("User", "sp_Help_User", txt_UserCode.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_UserCode.Text = HelpForm.sRtCode;
                    txt_UserCodeNM.Text = HelpForm.sRtCodeNm;
                    User_Search();
                }
            }
        }

        private void txt_UserCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_UserCodeNM.Text = PopHelpForm.Return_Help("sp_Help_User", txt_UserCode.Text);
            User_Search();
        }

        private void txt_UserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txt_UserCode_Properties_ButtonClick(sender, null);
            }
        }

        //사업장
        private void txt_Comp_EditValueChanged(object sender, EventArgs e)
        {
           txt_CompNM.Text = PopHelpForm.Return_Help("sp_Help_Company", txt_Comp.Text);
        }

        private void txt_Comp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txt_Comp_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_Comp_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CompNM.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Company", "sp_Help_Company", txt_Comp.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_Comp.Text = HelpForm.sRtCode;
                    txt_CompNM.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        #endregion

        #region 세부 사항 조회
        //영업담당자 부서 조회
        private void User_Search()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regSalePlanTr");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "U");
                sp.AddParam("User_Code", txt_UserCode.Text);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt = ret.ReturnDataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt = DbHelp.Fill_Table(dt);
                        DataRow dr = dt.Rows[0];

                        txt_DeptCode.Text = dr["Dept_Name"].ToString();
                        sDeptCode = dr["Dept_Code"].ToString();
                    }
                    else
                    {
                        txt_DeptCode.Text = "";
                        sDeptCode = "";
                    }
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
        #endregion

        #region 버튼 이벤트
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if(btn_Insert.Result_Update == DialogResult.Yes)
            {
                if (!Check_Err())
                    return;
                btn_Save_Click(null, null);
            }

            DbHelp.Clear_Panel(panel_M);
            DbHelp.Clear_Panel(panel_H);

            gc_OrderS.DataSource = null;

            txt_OrderNo.Text = "";

            Search_Grid();

            txt_UserCode.Text = GlobalValue.sUserID;

            txt_Comp.Text = Comp_Def();

            dt_OrderDate.Focus();

            btn_Insert.sUpdate = "N";
            btn_Close.sUpdate = "N";
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!Check_Err())
                return;

            try
            {
                string sItemCode = "", sDelivery = "", sQty = "", sBigo = "";

                for (int i = 0; i < gv_OrderS.RowCount; i++)
                {
                    if(!string.IsNullOrWhiteSpace(gv_OrderS.GetRowCellValue(i, "Item_Name").ToString()))
                    {
                        sItemCode += gv_OrderS.GetRowCellValue(i, "Item_Code").ToString() + "_/";
                        sQty += gv_OrderS.GetRowCellValue(i, "Qty").NumString() + "_/";
                        sBigo += gv_OrderS.GetRowCellValue(i, "Order_Bigo").ToString() + "_/";
                        sDelivery += gv_OrderS.GetRowCellValue(i, "Delivery").ToString() == "" ? "_/" : DateTime.Parse(gv_OrderS.GetRowCellValue(i, "Delivery").ToString()).ToString("yyyyMMdd") + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regSalePlanTr");
                sp.AddParam("Kind", "I");
                sp.AddParam("Order_No", txt_OrderNo.Text);
                sp.AddParam("Order_Date", dt_OrderDate.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("Company_Code", txt_Comp.Text);
                sp.AddParam("User_Code", txt_UserCode.Text);
                sp.AddParam("Dept_Code", sDeptCode);
                sp.AddParam("Order_Memo", memo_Order.Text);
                sp.AddParam("Reg_User", GlobalValue.sUserID);
                sp.AddParam("Form_Name", this.Name);
                sp.AddParam("Project_Title", txt_Project_Title.Text);
                //S
                sp.AddParam("ItemCode", sItemCode);
                sp.AddParam("Qty", sQty);
                sp.AddParam("OrderBigo", sBigo);
                sp.AddParam("Delivery", sDelivery);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                txt_OrderNo.Text = ret.ReturnDataSet.Tables[0].Rows[0]["Order_No"].ToString();

                Search_Grid();

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
            if (string.IsNullOrWhiteSpace(txt_OrderNo.Text))
                return;

            try
            {
                SqlParam sp = new SqlParam("sp_regSalePlanTr");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "H");
                sp.AddParam("Order_No", txt_OrderNo.Text);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_OrderS.DataSource = null;

                txt_OrderNo.Text = "";

                Search_Grid();

                txt_Comp.Text = Comp_Def();

                txt_UserCode.Text = GlobalValue.sUserID;

                btn_Delete.sCHK = "Y";
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            PopHelpForm HelpForm = new PopHelpForm("SalePlan", "sp_Help_OrderTr", "", "N");
            HelpForm.Set_Value(this.Name, "", "", "", "");
            HelpForm.sLevelYN = "Y";
            HelpForm.sNotReturn = "Y";
            btn_Select.clsWait.CloseWait();
            if (HelpForm.ShowDialog() == DialogResult.OK)
            {
                txt_OrderNo.Text = HelpForm.sRtCode;

                btn_Select.clsWait.ShowWait(this.FindForm());
                Search_Grid();
                btn_Select.clsWait.CloseWait();

                sSearch = "N";

                btn_Insert.sUpdate = "N";
                btn_Close.sUpdate = "N";
            }
        }

        //프린트 출력
        private void btn_Print_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_OrderS, this.Name);
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

        #region 그리드 Help
        private void grid_S_Help(object sender, EventArgs e)
        {
            int iRow = gv_OrderS.FocusedRowHandle;

            if (iRow < 0)
                return;

            if(string.IsNullOrWhiteSpace(gv_OrderS.GetRowCellValue(iRow, "Item_Name").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("Item", "sp_Help_Item_Param", gv_OrderS.GetRowCellValue(iRow, "Item_Code").ToString(), "Y");
                Help_Form.sNotReturn = "Y";
                if(Help_Form.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in Help_Form.drReturn)
                    {
                        gv_OrderS.SetRowCellValue(iRow, "Item_Code", row["Item_Code"].ToString());
                        if (!string.IsNullOrWhiteSpace(gv_OrderS.GetRowCellValue(iRow, "Item_Code").ToString()))
                        {
                            gv_OrderS.SetRowCellValue(iRow, "Item_Name", row["Item_Name"].ToString());
                            gv_OrderS.SetRowCellValue(iRow, "Qty", null);
                            gv_OrderS.SetRowCellValue(iRow, "Q_Unit", row["Q_Unit"].ToString());
                            gv_OrderS.UpdateCurrentRow();
                        }

                        if (iRow + 1 == gv_OrderS.RowCount)
                            gv_OrderS.AddNewRow();

                        iRow++;

                        gv_OrderS.UpdateCurrentRow();
                    }

                    gv_OrderS.DeleteRow(iRow);
                }
            }
        }

        private void gv_OrderS_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "Item_Code")
            {
                if (!string.IsNullOrWhiteSpace(e.Value.ToString()) && (gc_OrderS.DataSource as DataTable).Select("Item_Code = '" + e.Value.ToString() + "'").Length > 0)
                {
                    XtraMessageBox.Show("동일한 품목이 이미 등록이 되어 있습니다");
                    gv_OrderS.SetRowCellValue(e.RowHandle, "Item_Code", "");
                    return;
                }

                DataRow Row_Item = PopHelpForm.Return_Help_Row("sp_Help_Item_Param", gv_OrderS.GetRowCellValue(e.RowHandle, "Item_Code").ToString());

                gv_OrderS.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_OrderS_CellValueChanged);
                if(Row_Item != null)
                {
                    gv_OrderS.SetRowCellValue(e.RowHandle, "Item_Code", Row_Item["Item_Code"].ToString());
                    gv_OrderS.SetRowCellValue(e.RowHandle, "Item_Name", Row_Item["Item_Name"].ToString());
                    gv_OrderS.SetRowCellValue(e.RowHandle, "Qty", "");

                    gv_OrderS.UpdateCurrentRow();
                }
                else
                {
                    gv_OrderS.SetRowCellValue(e.RowHandle, "Item_Name", "");
                    gv_OrderS.SetRowCellValue(e.RowHandle, "Qty", "");

                    gv_OrderS.UpdateCurrentRow();
                }
                gv_OrderS.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_OrderS_CellValueChanged);
            }
            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }
        private void gc_OrderS_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gv_OrderS.FocusedColumn == gv_OrderS.Columns["Item_Code"])
                {
                    grid_S_Help(null, null);
                }
            }
        }

        #endregion

        #region 상속 함수(단축키)
        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnSave()
        {
            btn_Save.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        protected override void btnPrint()
        {
            btn_Print.PerformClick();
        }

        public override void Search_Key(string sKey_No)
        {
            txt_OrderNo.Text = sKey_No;
            Search_Grid();

            base.Search_Key(sKey_No);
        }

        protected override void Control_TextChange(object sender, EventArgs e)
        {
            base.Control_TextChange(sender, e);

            if (sLoad == "Y")
            {
                sLoad = "N";
                return;
            }
            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }


        #endregion

        #region 그리드 행 삭제
        private void S_Delete_Row(object sender, EventArgs e)
        {
            int iRow = gv_OrderS.GetFocusedDataSourceRowIndex();

            try
            {
                SqlParam sp = new SqlParam("sp_regSalePlanTr");
                sp.AddParam("Kind", "D");
                sp.AddParam("Delete_D", "S");
                sp.AddParam("Order_No", txt_OrderNo.Text);
                sp.AddParam("Item_Code", gv_OrderS.GetRowCellValue(iRow, "Item_Code").ToString());
                sp.AddParam("Reg_User", GlobalValue.sUserID);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_OrderS.DeleteRow(iRow);
                gv_OrderS.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion
    }
}
