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

namespace MES
{
    public partial class regWork : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();

        public regWork()
        {
            InitializeComponent();
        }

        private void regWork_Load(object sender, EventArgs e)
        {
            Grid_Set();
            dt_WorkDate.DateTime = DateTime.Today;

            txt_CompCode.Text = Comp_Def();

            Search_Data();
            dt_WorkDate.Focus();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Work, gv_Work, "Check", "선택", "50", false, true, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "WIdx_No", "WIdx_No", "", false, false, false);
            DbHelp.GridSet(gc_Work, gv_Work, "Key_No", "작지번호", "120", false, false, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Work_Date", "작업일자", "110", false, true, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "RIdx_No", "계획번호(영업/주문)", "120", false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Item_Code", "품목코드", "120", false, false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Item_Name", "품목명", "120", false, false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "SSize", "규격", "150", false, false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Process_Code", "공정코드", "100", false, false, false);
            DbHelp.GridSet(gc_Work, gv_Work, "Process_Name", "공정명", "100", false, false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Plan_Qty", "계획수량", "80", false, false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Po_Qty", "지시수량", "80", false, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "User_Name", "작업자", "100", false, true, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "User_Code", "작업자코드", "100", false, false, false, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Dept_Name", "부서", "100", false, false, false, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Custom_Name", "작업처", "100", false, true, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Custom_Code", "작업처코드", "100", false, false, false, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Delivery", "작업종료일", "110", false, true, true, true);
            DbHelp.GridSet(gc_Work, gv_Work, "Po_Memo", "비고", "100", false, true, true);

            DbHelp.GridColumn_CheckBox(gv_Work, "Check");

            DbHelp.GridColumn_NumSet(gv_Work, "Plan_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_Work, "Po_Qty", ForMat.SetDecimal(this.Name, "Qty1"));

            DbHelp.GridColumn_Data(gv_Work, "Work_Date");
            DbHelp.GridColumn_Data(gv_Work, "Delivery");

            DbHelp.GridColumn_Help(gv_Work, "RIdx_No", "Y");
            DbHelp.GridColumn_Help(gv_Work, "Custom_Name", "Y");
            DbHelp.GridColumn_Help(gv_Work, "User_Name", "Y");

            RepositoryItemButtonEdit btn_edit = (RepositoryItemButtonEdit)gv_Work.Columns["RIdx_No"].ColumnEdit;
            btn_edit.Buttons[0].Click += new EventHandler(Plan_Help);
            gv_Work.Columns["RIdx_No"].ColumnEdit = btn_edit;

            RepositoryItemButtonEdit btn_edit_Custom = (RepositoryItemButtonEdit)gv_Work.Columns["Custom_Name"].ColumnEdit;
            btn_edit_Custom.Buttons[0].Click += new EventHandler(Custom_Help);
            gv_Work.Columns["Custom_Name"].ColumnEdit = btn_edit_Custom;

            RepositoryItemButtonEdit btn_edit_User = (RepositoryItemButtonEdit)gv_Work.Columns["User_Name"].ColumnEdit;
            btn_edit_User.Buttons[0].Click += new EventHandler(User_Help);
            gv_Work.Columns["User_Name"].ColumnEdit = btn_edit_User;

            gc_Work.AddRowYN = true;
            gc_Work.PopMenuChk = true;
            gc_Work.MouseWheelChk = true;

            gv_Work.OptionsView.ShowAutoFilterRow = false;

            gv_Work.OptionsCustomization.AllowSort = false; 

            //BOM 정보 그리드
            gc_BOM.AddRowYN = false;
            gc_BOM.PopMenuChk = false;
            gc_BOM.MouseWheelChk = false;

            gv_BOM.OptionsView.ShowAutoFilterRow = false;
            gv_BOM.OptionsCustomization.AllowSort = false;

            DbHelp.GridSet(gc_BOM, gv_BOM, "Sort_No", "No", "", false, false, false, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "SItem_Code", "품목코드", "120", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Item_Name", "품목명", "150", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "SSize", "규격", "150", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Item_BPart", "품목구분", "80", false, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Qty", "수량", "80", true, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Loss_Per", "Loss", "80", true, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Real_Qty", "실소요량", "80", true, false, true, true);
            DbHelp.GridSet(gc_BOM, gv_BOM, "Bom_Bigo", "비고", "150", false, false, true, true);

            DbHelp.GridColumn_NumSet(gv_BOM, "Qty", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_BOM, "Loss_Per", ForMat.SetDecimal(this.Name, "Qty1"));
            DbHelp.GridColumn_NumSet(gv_BOM, "Real_Qty", ForMat.SetDecimal(this.Name, "Qty1"));
        }

        #region 버튼 이벤트
        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (gv_Work.RowCount < 0)
                return;

            try
            {
                string sWIdx_No = "", sRIdx_No = "", sWorkDate = "", sPo_Qty = "", sUserCode = "", sCustomCode = "", sDelivery = "", sPoMemo = "", sItemCode = "", sProcessCode = "";

                for (int i = 0; i < gv_Work.RowCount; i++)
                {
                    if (gv_Work.GetRowCellValue(i, "Check").ToString() == "Y")
                    {
                        if(string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(i, "Work_Date").ToString()))
                        {
                            XtraMessageBox.Show("작업일자는 필수 입력값입니다.");
                            return;
                        }

                        sWIdx_No += gv_Work.GetRowCellValue(i, "WIdx_No").NumString() + "_/";
                        sRIdx_No += gv_Work.GetRowCellValue(i, "RIdx_No").ToString() + "_/";
                        sWorkDate += DateTime.Parse(gv_Work.GetRowCellValue(i, "Work_Date").ToString()).ToString("yyyyMMdd") + "_/";
                        sPo_Qty += gv_Work.GetRowCellValue(i, "Po_Qty").NumString() + "_/";
                        sUserCode += gv_Work.GetRowCellValue(i, "User_Code").ToString() + "_/";
                        sCustomCode += gv_Work.GetRowCellValue(i, "Custom_Code").ToString() + "_/";
                        sItemCode += gv_Work.GetRowCellValue(i, "Item_Code").ToString() + "_/";
                        sProcessCode += gv_Work.GetRowCellValue(i, "Process_Code").ToString() + "_/";
                        sDelivery += gv_Work.GetRowCellValue(i, "Delivery").ToString() == "" ? "_/" : DateTime.Parse(gv_Work.GetRowCellValue(i, "Delivery").ToString()).ToString("yyyyMMdd") + "_/";
                        sPoMemo += gv_Work.GetRowCellValue(i, "Po_Memo").ToString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regWork");
                sp.AddParam("Kind", "I");
                sp.AddParam("Company_Code", txt_CompCode.Text);
                sp.AddParam("WIdxNo", sWIdx_No);
                sp.AddParam("RIdxNo", sRIdx_No);
                sp.AddParam("WorkDate", sWorkDate);
                sp.AddParam("PoQty", sPo_Qty);
                sp.AddParam("UserCode", sUserCode);
                sp.AddParam("CustomCode", sCustomCode);
                sp.AddParam("Delivery", sDelivery);
                sp.AddParam("PoMemo", sPoMemo);
                sp.AddParam("ItemCode", sItemCode);
                sp.AddParam("ProcessCode", sProcessCode);
                sp.AddParam("Reg_User", GlobalValue.sUserID);
                sp.AddParam("Form_Name", this.Name);

                ret = DbHelp.Proc_Save(sp);

                if(ret.ReturnChk != 0)
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
           
            btn_Save.sCHK = "Y";
            Search_Data();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (gv_Work.RowCount < 0)
                return;

            try
            {
                string sWIdx_No = "";

                for (int i = 0; i < gv_Work.RowCount; i++)
                {
                    if (gv_Work.GetRowCellValue(i, "Check").ToString() == "Y")
                    {
                        sWIdx_No += gv_Work.GetRowCellValue(i, "WIdx_No").NumString() + "_/";
                    }
                }

                SqlParam sp = new SqlParam("sp_regWork");
                sp.AddParam("Kind", "D");
                sp.AddParam("WIdxNo", sWIdx_No);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
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

            btn_Delete.sCHK = "Y";
            Search_Data();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            //if (btn_Insert.Result_Update == DialogResult.Yes)
            //{
            //    btn_Save_Click(null, null);
            //    Search_Data();
            //}

            //btn_Insert.sUpdate = "N";
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_Work, "작업지시등록");
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

        #region 헬프(그리드, 텍스트)

        private void Plan_Help(object sender, EventArgs e)
        {
            int iRow = gv_Work.FocusedRowHandle;

            if (string.IsNullOrWhiteSpace(txt_CompName.Text))
            {
                XtraMessageBox.Show("사업장 정보를 먼저 입력하세요");
                return;
            }

            if(string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(iRow, "Item_Code").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("SheetPlan", "sp_Help_SheetPlan", gv_Work.GetRowCellValue(iRow, "RIdx_No").ToString(), "Y");
                Help_Form.Set_Value(this.Name, "", "", "", "");
                Help_Form.sNotReturn = "Y";
                Help_Form.sLevelYN = "Y";
                if(Help_Form.ShowDialog() == DialogResult.OK)
                {
                    foreach(DataRow row in Help_Form.drReturn)
                    {
                        gv_Work.SetRowCellValue(iRow, "RIdx_No", row["RIdx_No"].ToString());

                        if (iRow + 1 == gv_Work.RowCount)
                            gv_Work.AddNewRow();

                        iRow++;

                        gv_Work.UpdateCurrentRow();
                    }

                    gv_Work.DeleteRow(iRow);
                }
            }
        }

        private void Custom_Help(object sender, EventArgs e)
        {
            int iRow = gv_Work.FocusedRowHandle;

            if (string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(iRow, "Custom_Code").ToString()))
            {
                PopHelpForm Help_Form = new PopHelpForm("Custom", "sp_Help_Custom_Param", gv_Work.GetRowCellValue(iRow, "Custom_Name").ToString());
                Help_Form.Set_Value("생산", "500", "", "", "");
                if (Help_Form.ShowDialog() == DialogResult.OK)
                {
                    gv_Work.SetRowCellValue(iRow, "Custom_Code", Help_Form.sRtCode);
                    if (!string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(iRow, "Custom_Code").ToString()))
                    {
                        gv_Work.SetRowCellValue(iRow, "Custom_Name", Help_Form.sRtCodeNm);
                    }
                }
            }
        }

        private void User_Help(object sender, EventArgs e)
        {
            int iRow = gv_Work.FocusedRowHandle;

            if (string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(iRow, "User_Code").ToString()))
            {
                PopHelpForm HelpForm = new PopHelpForm("User", "sp_Help_User", gv_Work.GetRowCellValue(iRow, "User_Name").ToString());
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    gv_Work.SetRowCellValue(iRow, "User_Code", HelpForm.sRtCode);
                    gv_Work.SetRowCellValue(iRow, "User_Name", HelpForm.sRtCodeNm);
                    User_Search(iRow);
                }
            }
        }

        private void txt_CompCode_EditValueChanged(object sender, EventArgs e)
        {
            txt_CompName.Text = PopHelpForm.Return_Help("sp_Help_Company", txt_CompCode.Text);
        }

        private void txt_CompCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_CompCode_Properties_ButtonClick(sender, null);
            }
        }

        private void txt_CompCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CompName.Text))
            {
                PopHelpForm HelpForm = new PopHelpForm("Company", "sp_Help_Company", txt_CompCode.Text, "N");
                if (HelpForm.ShowDialog() == DialogResult.OK)
                {
                    txt_CompCode.Text = HelpForm.sRtCode;
                    txt_CompName.Text = HelpForm.sRtCodeNm;
                }
            }
        }

        private void dt_WorkDate_EditValueChanged(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void dt_TWorkDate_EditValueChanged(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void txt_CompName_EditValueChanged(object sender, EventArgs e)
        {
            Search_Data();
        }

        #endregion

        #region 그리드 이벤트


        private void gv_Work_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Custom_Name")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_Custom_Param", e.Value.ToString(), "", "생산");
                if (dr_Help == null)
                {
                    gv_Work.SetRowCellValue(e.RowHandle, "Custom_Code", "");
                }
                else
                {
                    gv_Work.SetRowCellValue(e.RowHandle, "Custom_Code", dr_Help["Custom_Code"]);
                    gv_Work.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                    gv_Work.SetRowCellValue(e.RowHandle, "Custom_Name", dr_Help["Short_Name"]);
                    gv_Work.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                }

                //string sCustom_Code = gv_Work.GetRowCellValue(e.RowHandle, "Custom_Code").NullString();
                //string sCustom_Name = "";

                //if (string.IsNullOrWhiteSpace(sCustom_Code))
                //{
                //    sCustom_Name = PopHelpForm.Return_Help("sp_Help_Custom_Param", e.Value.ToString(), "", "생산");
                //    if (!string.IsNullOrWhiteSpace(sCustom_Name))
                //    {
                //        gv_Work.SetRowCellValue(e.RowHandle, "Custom_Code", e.Value.ToString());
                //        gv_Work.SetRowCellValue(e.RowHandle, "Custom_Name", sCustom_Name);
                //    }
                //}
                //else
                //{
                //    sCustom_Name = PopHelpForm.Return_Help("sp_Help_Custom_Param", sCustom_Code, "", "생산");
                //    if (sCustom_Name != e.Value.ToString())
                //    {
                //        gv_Work.SetRowCellValue(e.RowHandle, "Custom_Code", "");
                //    }
                //}
            }
            else if(e.Column.FieldName == "User_Name")
            {
                DataRow dr_Help = PopHelpForm.Return_Help_Row("sp_Help_User", e.Value.ToString());
                if (dr_Help == null)
                {
                    gv_Work.SetRowCellValue(e.RowHandle, "User_Code", "");
                }
                else
                {
                    gv_Work.SetRowCellValue(e.RowHandle, "User_Code", dr_Help["User_Code"]);
                    gv_Work.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                    gv_Work.SetRowCellValue(e.RowHandle, "User_Name", dr_Help["User_Name"]);
                    gv_Work.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                }
            }
            else if(e.Column.FieldName == "RIdx_No")
            {
                if(!string.IsNullOrWhiteSpace(e.Value.ToString()) && (gc_Work.DataSource as DataTable).Select("RIdx_No = '" + e.Value.ToString() + "'").Length > 0)
                {
                    XtraMessageBox.Show("동일한 계획이 이미 등록이 되어 있습니다");
                    gv_Work.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                    gv_Work.SetRowCellValue(e.RowHandle, "RIdx_No", "");
                    gv_Work.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                    return;
                }

                Search_Rout(e.Value.ToString(), e.RowHandle);
            }

            if(e.Column.FieldName != "Check")
            {
                gv_Work.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                gv_Work.SetRowCellValue(e.RowHandle, "Check", "Y");
                gv_Work.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
            }

            btn_Insert.sUpdate = "Y";
            btn_Close.sUpdate = "Y";
        }

        private void gc_Work_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if(gv_Work.FocusedColumn.FieldName == "Custom_Name")
                {
                    Custom_Help(null, null);
                }
                else if(gv_Work.FocusedColumn.FieldName == "RIdx_No")
                {
                    Plan_Help(null, null);
                }
                else if(gv_Work.FocusedColumn.FieldName == "User_Name")
                {
                    User_Help(null, null);
                }
            }
        }

        private void gc_Work_NewRowAdd(object sender, InitNewRowEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(e.RowHandle, "User_Name").ToString()))
            {
                gv_Work.SetRowCellValue(e.RowHandle, "Work_Date", dt_WorkDate.DateTime);
                gv_Work.SetRowCellValue(e.RowHandle, "User_Name", GlobalValue.sUserID);
            }
        }

        private void gv_Work_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(gv_Work.GetRowCellValue(e.RowHandle, "User_Name").ToString()))
            {
                gv_Work.SetRowCellValue(e.RowHandle, "Work_Date", dt_WorkDate.DateTime);
                gv_Work.SetRowCellValue(e.RowHandle, "User_Name", GlobalValue.sUserID);
            }
        }


        private void gv_Work_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;

            string sItem_Code = gv_Work.GetRowCellValue(e.FocusedRowHandle, "Item_Code").ToString();

            Search_Bom(sItem_Code);
        }

        #endregion

        #region 내부 함수
        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWork");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "H");
                sp.AddParam("FDATE", dt_WorkDate.DateTime.ToString("yyyyMMdd"));
                sp.AddParam("Company_Code", txt_CompCode.Text);
                sp.AddParam("Form_Name", this.Name);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_Work.DataSource = ret.ReturnDataSet.Tables[0];

                gv_Work.BestFitColumns();

                if(gv_Work.RowCount > 0)
                {
                    Search_Bom(gv_Work.GetRowCellValue(0, "Item_Code").ToString());
                }
                else
                {
                    gc_BOM.DataSource = null;
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
                SqlParam sp = new SqlParam("sp_regWork");
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

        private void User_Search(int iRow)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWork");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "U");
                sp.AddParam("User_Code", gv_Work.GetRowCellValue(iRow, "User_Code").ToString());

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk == 0)
                {
                    DataTable dt = ret.ReturnDataSet.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt = DbHelp.Fill_Table(dt);
                        DataRow dr = dt.Rows[0];

                        gv_Work.SetRowCellValue(iRow, "Dept_Name", dr["Dept_Name"].ToString());
                    }
                    else
                    {
                        gv_Work.SetRowCellValue(iRow, "Dept_Name", "");
                    }
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

        private void Search_Rout(string sRIdx_No, int iRow)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWork");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SR");
                sp.AddParam("RIdx_No", sRIdx_No);

                ret = DbHelp.Proc_Search(sp);

                if(ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gv_Work.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);
                if (ret.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ret.ReturnDataSet.Tables[0].Rows[0];

                    gv_Work.SetRowCellValue(iRow, "Item_Code", row["Item_Code"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Item_Name", row["Item_Name"].ToString());
                    gv_Work.SetRowCellValue(iRow, "SSize", row["SSize"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Process_Code", row["Process_Code"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Process_Name", row["Process_Name"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Custom_Name", row["Custom_Name"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Custom_Code", row["Custom_Code"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Delivery", row["Delivery"].ToString());
                    gv_Work.SetRowCellValue(iRow, "Plan_Qty", row["Plan_Qty"].NumString());
                    gv_Work.SetRowCellValue(iRow, "Po_Qty", row["Plan_Qty"].NumString());

                    Search_Bom(row["Item_Code"].ToString());
                }
                else
                {
                    gv_Work.SetRowCellValue(iRow, "Item_Code", "");
                    gv_Work.SetRowCellValue(iRow, "Item_Name", "");
                    gv_Work.SetRowCellValue(iRow, "SSize", "");
                    gv_Work.SetRowCellValue(iRow, "Process_Code", "");
                    gv_Work.SetRowCellValue(iRow, "Process_Name", "");
                    gv_Work.SetRowCellValue(iRow, "Custom_Name", "");
                    gv_Work.SetRowCellValue(iRow, "Custom_Code", "");
                    gv_Work.SetRowCellValue(iRow, "Delivery", "");
                    gv_Work.SetRowCellValue(iRow, "Plan_Qty", null);
                    gv_Work.SetRowCellValue(iRow, "Po_Qty", null);

                    gc_BOM.DataSource = null;
                }
                gv_Work.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gv_Work_CellValueChanged);

                gv_Work.UpdateCurrentRow();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void Search_Bom(string sItem_Code)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regWork");
                sp.AddParam("Kind", "S");
                sp.AddParam("Search_D", "SB");
                sp.AddParam("Item_Code", sItem_Code);

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_BOM.DataSource = ret.ReturnDataSet.Tables[0];

                gv_BOM.BestFitColumns();
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
