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
    public partial class regUser : BaseReg
    {
        ReturnStruct ret = new ReturnStruct();
        DataSet ds = new DataSet();

        public regUser()
        {
            InitializeComponent();
        }

        private void regUser_Load(object sender, EventArgs e)
        {
            DbHelp.GridSet(gc_User, gv_User, "User_Code", "사원코드", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "User_Name", "사원명", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Dept_Name", "부서명", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "User_PosNM", "직책", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "In_Date", "입사일자", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Tel_No", "전화번호", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Fax_No", "팩스번호", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Mobile_No", "핸드폰번호", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "E_Mail", "이메일", "", false, false, true);
            DbHelp.GridSet(gc_User, gv_User, "Use_Ck", "사용유무", "", false, false, true);

            DbHelp.GridColumn_CheckBox(gv_User, "Use_Ck");

            gc_User.DeleteRowEventHandler += btnDelete_Click; // 우클릭 삭제 이벤트 등록

            gc_User.PopMenuChk = false;

            btnSelect_Click(null, null);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            PopUserForm PopForm = new PopUserForm();
            PopForm.StartPosition = FormStartPosition.CenterScreen;
            if(PopForm.ShowDialog() == DialogResult.OK)
                this.btnSelect_Click(null, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParam sp = new SqlParam("sp_regUser");
                sp.AddParam("Kind", "S");
                sp.AddParam("Use_Ck", "Y");

                ret = DbHelp.Proc_Search(sp);

                if (ret.ReturnChk != 0)
                {
                    XtraMessageBox.Show(ret.ReturnMessage);
                    return;
                }

                gc_User.DataSource = ret.ReturnDataSet.Tables[0];
                gv_User.BestFitColumns();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        private void gv_User_DoubleClick(object sender, EventArgs e)
        {
            string sUser_Code = gv_User.GetRowCellValue(gv_User.GetSelectedRows()[0], "User_Code").ToString();

            PopUserForm PopForm = new PopUserForm(sUser_Code);
            PopForm.StartPosition = FormStartPosition.CenterScreen;
            if (PopForm.ShowDialog() == DialogResult.OK)
                this.btnSelect_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < gv_User.RowCount; i++)
            {
                if (gv_User.IsRowSelected(i))
                {
                    try
                    {
                        SqlParam sp = new SqlParam("sp_regUser");
                        sp.AddParam("Kind", "D");
                        sp.AddParam("User_Code", gv_User.GetRowCellValue(i, "User_Code").ToString());

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
                }
            }

            btnSelect_Click(null, null);
        }

        protected override void btnInsert()
        {
            btn_Insert.PerformClick();
        }

        protected override void btnSelect()
        {
            btn_Select.PerformClick();
        }

        protected override void btnDelete()
        {
            btn_Delete.PerformClick();
        }

        protected override void btnClose()
        {
            btn_Close.PerformClick();
        }

        protected override void btnExcel()
        {
            btn_Excel.PerformClick();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            MainForm.MainTab.TabPages.Remove(MainForm.MainTab.SelectedTabPage);
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            FileIF.Excel_Down(gc_User, "사원정보");
        }
    }
}
