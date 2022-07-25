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
    public partial class regCustome : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regCustome()
        {
            InitializeComponent();
        }

        private void regCustome_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(gc_Cust, gv_Cust, "Custom_Code", "거래처코드", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Custom_Part", "거래처구분 코드", "100", false, false, false);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Custom_Part_Name", "거래처구분", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Custom_Name", "거래처명", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Short_Name", "줄임상호", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Company_No", "사업자번호", "120", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Office_No", "법인번호", "120", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "UpTai", "업태", "80", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "UpJong", "업종", "80", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Tel_No", "전화번호", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Fax_No", "팩스번호", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "E_Mail", "이메일", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Home_Page", "홈페이지", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Pay_CustomNM", "결제처", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Pay_Date", "결제예정일", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "P_Unit", "화폐단위 코드", "80", false, false, false);
            DbHelp.GridSet(gc_Cust, gv_Cust, "P_Unit_Name", "화폐단위", "80", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Pay_CodeNM", "결제방식", "100", false, false, false);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Pay_Part", "결제조건 코드", "80", false, false, false);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Pay_Part_Name", "결제조건", "80", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Vat_CodeNM", "과세구분", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Country_CodeNM", "국가", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Use_Ck", "사용유무", "80", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Bill_Addr1", "주소1", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Bill_Addr2", "주소2", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Reg_Date", "등록일자", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Reg_User", "등록자 코드", "100", false, false, false);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Reg_User_Name", "등록자", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Up_Date", "수정일자", "100", false, false, true);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Up_User", "수정자 코드", "100", false, false, false);
            DbHelp.GridSet(gc_Cust, gv_Cust, "Up_User_Name", "수정자", "100", false, false, true);
            gc_Cust.PopMenuChk = false;

            RepositoryItemCheckEdit Check_Edit = gc_Cust.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            Check_Edit.ValueChecked = "Y";
            Check_Edit.ValueGrayed = "N";
            Check_Edit.ValueUnchecked = "N";
            Check_Edit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;

            gv_Cust.Columns["Use_Ck"].ColumnEdit = Check_Edit;
            gv_Cust.OptionsSelection.MultiSelect = true;


            // 사업자 번호 마스크
            RepositoryItemTextEdit B_Text_Edit = gc_Cust.RepositoryItems.Add("TextEdit") as RepositoryItemTextEdit;
            B_Text_Edit.Mask.EditMask = "(\\(\\d\\d\\d\\) )?\\d{1,3}-\\d{1,2}-\\d{1,5}";
            B_Text_Edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            B_Text_Edit.Mask.UseMaskAsDisplayFormat = true;
            B_Text_Edit.MaxLength = 12;

            RepositoryItemTextEdit C_Text_Edit = gc_Cust.RepositoryItems.Add("TextEdit") as RepositoryItemTextEdit;
            C_Text_Edit.Mask.EditMask = "(\\(\\d\\d\\d\\) )?\\d{1,4} \\d{1,2}-\\d{1,6} \\d{1,1}";
            C_Text_Edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            C_Text_Edit.Mask.UseMaskAsDisplayFormat = true;
            C_Text_Edit.MaxLength = 16;

            gv_Cust.Columns["Company_No"].ColumnEdit = B_Text_Edit;
            gv_Cust.Columns["Office_No"].ColumnEdit = C_Text_Edit; 
        }

        #region 버튼 이벤트
        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            PopCustomeForm PopForm = new PopCustomeForm();
            PopForm.StartPosition = FormStartPosition.CenterScreen;
            if (PopForm.ShowDialog() == DialogResult.OK)
                btnSelect_Click(null, null);
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_Cust.FocusedRowHandle < 0) return;

                string c_code = gv_Cust.GetFocusedRowCellValue("Custom_Code").ToString();

                SqlParam sp = new SqlParam("sp_regCustome");
                sp.AddParam("Kind", "D");
                sp.AddParam("Custom_Code", c_code);

                ret = DbHelp.Proc_Save(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }
                btn_Delete.sCHK = "Y";
                btnSelect_Click(null, null);
                //XtraMessageBox.Show("삭제되었습니다.");
            }
            catch (Exception Ex)
            {
                XtraMessageBox.Show(Ex.ToString());
                return;
            }
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_Cust, this.Name);
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        private void Search_Data()
        {
            SqlParam sp = new SqlParam("sp_regCustome");
            sp.AddParam("Kind", "S");
            sp.AddParam("Custom_Code", null);

            ret = DbHelp.Proc_Search(sp);
            ds = ret.ReturnDataSet;

            DataTable Table = DbHelp.Fill_Table(ds.Tables[0]);
            gc_Cust.DataSource = Table;
            gc_Cust.RefreshDataSource();
            //gv_Cust.BestFitColumns();
        }

        private void gc_Cust_DoubleClick(object sender, EventArgs e)
        {
            if (gv_Cust.FocusedRowHandle < 0 || gv_Cust.RowCount < 1)
                return;

            PopCustomeForm PopForm = new PopCustomeForm();
            PopForm.Cust_Code = gv_Cust.GetFocusedRowCellValue("Custom_Code").ToString();
            PopForm.StartPosition = FormStartPosition.CenterScreen;
            if (PopForm.ShowDialog() == DialogResult.OK)
                btnSelect_Click(null, null);
        }
    }
}
