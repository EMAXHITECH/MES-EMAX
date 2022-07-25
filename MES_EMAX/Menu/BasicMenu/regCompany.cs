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
using DevExpress.XtraEditors.Controls;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.XtraTab;

namespace MES
{
    public partial class regCompany : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regCompany()
        {
            InitializeComponent();
        }

        private void regCompany_Load(object sender, EventArgs e)
        {
            Grid_Set();
            Search_Data();
        }

        private void Grid_Set()
        {
            DbHelp.GridSet(Grid_Company, View_Company, "Company_Code", "회사코드", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Company_Name", "회사명", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Owner", "대표자", "90", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Short_Name", "줄임상호", "90", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Company_No", "사업자번호", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Office_No", "법인번호", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "UpTai", "업태", "80", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "UpJong", "업종", "80", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Tel_No", "대표전화", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Fax_No", "팩스번호", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "E_Mail", "이메일", "125", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Home_Page", "홈페이지", "100", false, false, true);
            DbHelp.GridSet(Grid_Company, View_Company, "Def_Ck", "주사업장", "80", false, false, true);

            DbHelp.GridColumn_CheckBox(View_Company, "Def_Ck");

            Grid_Company.MouseWheelChk = false;
            Grid_Company.PopMenuChk = false;
            Grid_Company.AddRowYN = false;
        }

        private void View_Company_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(View_Company.GetFocusedRowCellValue("Company_Code").NullString()))
            {
                PopCompanyForm Form = new PopCompanyForm(View_Company.GetFocusedRowCellValue("Company_Code").NullString());
                Form.StartPosition = FormStartPosition.CenterScreen;

                if (Form.ShowDialog() == DialogResult.OK)
                {
                    Search_Data();
                }
            }
        }

        #region 버튼 이벤트
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            PopCompanyForm Form = new PopCompanyForm();
            Form.StartPosition = FormStartPosition.CenterScreen;

            if (Form.ShowDialog() == DialogResult.OK)
            {
                Search_Data();
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void Search_Data()
        {
            try
            {
                SqlParam sp = new SqlParam("sp_Company");
                sp.AddParam("Kind", "S");
                sp.AddParam("Company_Code", "@");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                Grid_Company.DataSource = ret.ReturnDataSet.Tables[0];
                Grid_Company.RefreshDataSource();
                View_Company.BestFitColumns();
            }
            catch (Exception)
            {

            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            SqlParam sp = new SqlParam("sp_Company");
            sp.AddParam("Kind", "D");
            sp.AddParam("Company_Code", View_Company.GetFocusedRowCellValue("Company_Code").NullString());

            ret = DbHelp.Proc_Save(sp);

            if (ret.ReturnChk != 0)
            {
                XtraMessageBox.Show(ret.ReturnMessage);
            }

            btn_Delete.sCHK = "Y";

            Search_Data();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(Grid_Company, this.Name);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }
        #endregion

        #region 이미지
        private void pictureEdit_Sign_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
        }

        private void pictureEdit_Logo_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Image_Help.Image_Right_Click(sender, e);
            }
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
    }
}
